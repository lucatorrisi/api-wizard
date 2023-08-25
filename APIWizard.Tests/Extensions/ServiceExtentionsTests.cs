using NUnit.Framework;
using Moq;
using APIWizard.Extensions;
using APIWizard.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace APIWizard.Tests
{
    [TestFixture]
    public class ServiceExtensionsTests
    {
        [Test]
        public void AddAPIWizardClient_ValidArguments_AddsSingleton()
        {
            // Arrange
            var services = new ServiceCollection();
            var mockApiClient = new Mock<IAPIClient>();

            // Act
            services.AddAPIWizardClient(mockApiClient.Object);
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(sp => sp.GetService(typeof(IAPIClient))).Returns(mockApiClient.Object);

            // Assert
            var resolvedApiClient = serviceProvider.Object.GetService(typeof(IAPIClient)) as IAPIClient;
            Assert.IsNotNull(resolvedApiClient);
            Assert.AreSame(mockApiClient.Object, resolvedApiClient);
        }

        [Test]
        public void AddAPIWizardClient_NullServices_ThrowsArgumentNullException()
        {
            // Arrange
            IServiceCollection services = null;
            var mockApiClient = new Mock<IAPIClient>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ServiceExtensions.AddAPIWizardClient(services, mockApiClient.Object));
        }

        [Test]
        public void AddAPIWizardClient_NullAPIClient_ThrowsArgumentNullException()
        {
            // Arrange
            var services = new ServiceCollection();
            IAPIClient apiClient = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => services.AddAPIWizardClient(apiClient));
        }
    }
}
