using cBankWebApi.Models;
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
    public class YaasPersistentRepo
    {
        private readonly Uri _tokenUri;

        private string GetAuthToken()
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

        private WebRequest GetWebRequest(string url, string authToken, string method)
        {
            var wr = WebRequest.Create(url);
            wr.Method = method;
            wr.Headers["Authorization"] = $"Bearer {authToken}";
            wr.ContentType = "application/json";

            return wr;
        }

        private string GetRequestUrl(string tableName)
        {
            var reqUrl = "https://api.yaas.io/hybris/document/v1/beacons/beacons.ios/data/" + tableName;
            return reqUrl;
        }

        public T PostData<T>(string table, string data)
        {
            var reqUrl = GetRequestUrl(table);
            byte[] buf = Encoding.UTF8.GetBytes(data);

            var wr = GetWebRequest(reqUrl, GetAuthToken(), "POST");
            wr.ContentLength = buf.Length;
            wr.GetRequestStream().Write(buf, 0, buf.Length);
            var HttpWebResponse = (HttpWebResponse)wr.GetResponse();

            var encoding = UTF8Encoding.UTF8;
            T postDataResponse = default(T);
            using (var reader = new System.IO.StreamReader(HttpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                postDataResponse = JsonConvert.DeserializeObject<T>(responseText);
            }

            return postDataResponse;
        }

        public void PutData(string table, string data)
        {
            var reqUrl = GetRequestUrl(table);
            byte[] buf = Encoding.UTF8.GetBytes(data);

            var wr = GetWebRequest(reqUrl, GetAuthToken(), "PUT");
            wr.ContentLength = buf.Length;
            wr.GetRequestStream().Write(buf, 0, buf.Length);
            var HttpWebResponse = (HttpWebResponse)wr.GetResponse();

            var encoding = UTF8Encoding.UTF8;
            PostDataResponse postDataResponse = null;
            using (var reader = new System.IO.StreamReader(HttpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                postDataResponse = JsonConvert.DeserializeObject<PostDataResponse>(responseText);
            }
        }

        public T GetData<T>(string table, string data)
        {
            var reqUrl = GetRequestUrl(table + data);
         
            var wr = GetWebRequest(reqUrl, GetAuthToken(), "GET");
            var HttpWebResponse = (HttpWebResponse)wr.GetResponse();

            var encoding = UTF8Encoding.UTF8;
            T postDataResponse = default(T);
            using (var reader = new System.IO.StreamReader(HttpWebResponse.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                postDataResponse = JsonConvert.DeserializeObject<T>(responseText);
            }

            return postDataResponse;
        }

        public YaasPersistentRepo()
        {
            _tokenUri = new Uri("https://api.yaas.io/hybris/oauth2/v1/token");
        }
    }
}