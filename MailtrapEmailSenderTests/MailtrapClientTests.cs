using Moq;
using RestSharp;
using System.Net;
using AutoFixture;

namespace MailtrapEmailSender.EmailParameters.Tests
{
    [TestClass]
    public class MailtrapClientTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInit() 
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task SendEmailAsyncTest_ReturnsOk()
        {
            var logger = new Mock<ILogger>();
            var restClient = new Mock<IMailtrapRestClient>();

            restClient.Setup(c => c.PostAsync(It.IsAny<RestRequest>()))
                .Returns(Task.FromResult(new RestResponse() 
                { 
                    StatusCode = HttpStatusCode.OK
                }));

            var client = new MailtrapClient("token", logger.Object, restClient.Object);
            var input = _fixture.Create<EmailParameters>();

            var result = await client.SendEmailAsync(input);

            Assert.IsTrue(result, "Error with sending email");

            logger.Verify(l => l.Info("Email sent successfully."), Times.Once);
            logger.Verify(l => l.Error(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task SendEmailAsyncTest_ReturnsError()
        {
            var logger = new Mock<ILogger>();
            var restClient = new Mock<IMailtrapRestClient>();

            restClient.Setup(c => c.PostAsync(It.IsAny<RestRequest>()))
                .Returns(Task.FromResult(new RestResponse() 
                { 
                    StatusCode = HttpStatusCode.BadRequest 
                }));

            var client = new MailtrapClient("token", logger.Object, restClient.Object);
            var input = _fixture.Create<EmailParameters>();

            var result = await client.SendEmailAsync(input);

            Assert.IsFalse(result);

            logger.Verify(l => l.Info("Email sent successfully."), Times.Never);
            logger.Verify(l => l.Error(It.IsAny<string>()), Times.Once);
        }
    }
}