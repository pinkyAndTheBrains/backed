using cBankWebApi.Providers;
using NUnit.Framework;

namespace cBankWebApi.Tests.IntegrationTests
{
    [TestFixture]
    public class IntegrationTest
    {
        [TestCase]
        public void GetsToken()
        {
            var repo = new YaasPersistentRepo();
            repo.GetData();
        }
    }
}
