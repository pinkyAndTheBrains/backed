using cBankWebApi.Models;
using cBankWebApi.Providers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace cBankWebApi.Providers
{
    internal class YaasPersistentRepo<T> : IRepositoryReadWrite<T>
    {
        private readonly Uri _tokenUri;
        private readonly Uri _dataUri;
        private readonly string _repositoryName;
        private string _authToken;

        public YaasPersistentRepo()
        {
            _tokenUri = new Uri("https://api.yaas.io/hybris/oauth2/v1/token");
            _dataUri = new Uri("https://api.yaas.io/hybris/document/v1/beacons/beacons.ios/data/");
            _repositoryName = typeof(T).Name;
            _authToken = GenerateNewToken();
        }

        public T Get(string id)
        {
            var qId = HttpUtility.UrlEncode($"id:{id}");
            return FetchData(typeof(T).Name, $"?q={qId}").FirstOrDefault();
        }

        public void Add(T entity)
        {
            PostData<object>(entity);
        }

        public R Add<R>(T entity)
        {
            return PostData<R>(entity);
        }

        public void Update(T entity)
        {
            PutData(entity);
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        private R PostData<R>(T data)
        {
            var reqUrl = UrlToProcessData();
            var httpWebResponse = RequestHandler(() => GetWebRequest(reqUrl, "POST", data));

            var encoding = UTF8Encoding.UTF8;
            R postDataResponse = default(R);
            using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                try
                {
                    postDataResponse = JsonConvert.DeserializeObject<R>(responseText);
                }
                catch { };
            }

            return postDataResponse;
        }

        private void PutData(T data)
        {
            var reqUrl = UrlToProcessData();
            var httpWebResponse = RequestHandler(() => GetWebRequest(reqUrl, "PUT", data));

            var encoding = UTF8Encoding.UTF8;
            using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
            }
        }

        private IEnumerable<T> FetchData(string table, string data)
        {
            var reqUrl = UrlToRequestData(data);
            var httpWebResponse = RequestHandler(() => GetWebRequest(reqUrl, "GET"));

            var encoding = UTF8Encoding.UTF8;
            IEnumerable<T> postDataResponse = default(IEnumerable<T>);
            using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                postDataResponse = JsonConvert.DeserializeObject<IEnumerable<T>>(responseText);
            }

            return postDataResponse;
        }

        private string GenerateNewToken()
        {
            NameValueCollection postData = new NameValueCollection();
            postData.Add("grant_type", "client_credentials");
            postData.Add("scope", "hybris.tenant=beacons hybris.document_view hybris.document_manage");
            postData.Add("client_id", "q6CoQy2zMRhtspDpCpJg9yJTEwmZUdJF");
            postData.Add("client_secret", "yNrWpsLtCd5L1Ppo");

            TokenReponse tokenResponce = null;
            using (WebClient client = new WebClient())
            {
                var responce = client.UploadValues(_tokenUri, postData);
                var responceString = Encoding.UTF8.GetString(responce);
                tokenResponce = JsonConvert.DeserializeObject<TokenReponse>(responceString);
            }

            return tokenResponce?.access_token;
        }

        private WebRequest GetWebRequest(string url, string method, T content = default(T))
        {
            var wr = WebRequest.Create(url);
            wr.Method = method;
            wr.ContentType = "application/json";
            wr.Headers["Authorization"] = $"Bearer {_authToken}";

            if (content != null)
            {
                var stringData = JsonConvert.SerializeObject(content);
                byte[] buf = Encoding.UTF8.GetBytes(stringData);

                wr.ContentLength = buf.Length;
                wr.GetRequestStream().Write(buf, 0, buf.Length);
            }
            return wr;
        }

        private HttpWebResponse RequestHandler(Func<WebRequest> webRequestGenerate)
        {
            var tryCount = 0;
            var tryCountMax = 2;

            HttpWebResponse httpWebResponse = default(HttpWebResponse);
            while (tryCount < tryCountMax)
            {
                try
                {
                    var wr = webRequestGenerate();
                    httpWebResponse = (HttpWebResponse)wr.GetResponse();
                }
                catch (WebException ex)
                {
                    if ((ex.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _authToken = GenerateNewToken();
                        tryCount++;
                    }
                    if (tryCount == tryCountMax)
                    {
                        throw;
                    }
                    else
                    {
                        continue;
                    }
                }

                break;
            }

            return httpWebResponse;
        }

        private string UrlToProcessData()
        {
            var reqUrl = _dataUri + _repositoryName;
            return reqUrl;
        }

        private string UrlToRequestData(string data)
        {
            return $"{UrlToProcessData()}{data}";
        }
    }
}