using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBankWebApi.Controllers;
using cBankWebApi.Models;
using cBankWebApi.Providers;
using cBankWebApi.Providers.Interfaces;
using cBankWebApi.Push;
using Moq;
using NUnit.Framework;

namespace cBankWebApi.Tests.IntegrationTests
{
    [TestFixture]
    public class UserFlow
    {
        [Test]
        public void WhenUserAcceptPayment_MerchantWebPageIsNotified()
        {
            //arrange
            var productCatalogMoq = new Mock<IProductCatalog>();
            var products = new List<Product>()
            {
                new Product() {ProductId = "1", Name = "Name1", Price = 11.11m},
                new Product() {ProductId = "2", Name = "Name2", Price = 22.22m},
            };
            productCatalogMoq.Setup(x => x.GetProductsForCompany(It.IsAny<string>())).Returns(
                products);
            productCatalogMoq.Setup(x => x.GetProduct(It.IsAny<string>(), It.Is<string>(s => s == "1"))).Returns(products[0]);

            var suggestionController = new SuggestionsController(productCatalogMoq.Object);

            var transactionSystemMoq = new Mock<ITransactionSystem>();
            var definedTransactionId = Guid.NewGuid().ToString();
            transactionSystemMoq.Setup(x => x.RegisterTransaction(It.IsAny<Product>())).Returns(definedTransactionId);
            var prepareController = new PaymentPrepareController(transactionSystemMoq.Object, productCatalogMoq.Object);
            var merchantNotifierMoq = new Mock<IMerchantNotifier>();
            var intermediateController = new PaymentFinalController(transactionSystemMoq.Object,
                merchantNotifierMoq.Object);

            var beaconId = "beacon_Id";
            
            //act
            var list = suggestionController.Get(beaconId);
            var transactionId = prepareController.Get(list.First().ProductId);
            intermediateController.Post(new TransactionAuth() {TransactionId = transactionId, AuthCode = "1234"});
            
            //assert
            Assert.AreEqual(definedTransactionId, transactionId);
            productCatalogMoq.Verify(
                x => x.GetProductsForCompany(It.Is<string>(passedBeaconId => passedBeaconId.Equals(beaconId))),
                Times.Once);

            transactionSystemMoq.Verify(
                x => x.RegisterTransaction(It.Is<Product>(product => product.ProductId == "1" && product.Price == 11.11m)),
                Times.Once);
            transactionSystemMoq.Verify(
                x =>
                    x.AuthTransaction(
                        It.Is<TransactionAuth>(
                            auth => auth.TransactionId == definedTransactionId && auth.AuthCode == "1234")), Times.Once);

            merchantNotifierMoq.Verify(x => x.NotifyMerchant(It.IsAny<MerchantNotificationMessage>()), Times.Once);
        }
    }
}
