using APIWizard.Utils;

namespace APIWizard.Tests.Utils
{
    [TestFixture]
    public class HttpRequestUtilsTests
    {
        [Test]
        public void ConvertToHttpMethod_ValidMethod_ReturnsHttpMethodInstance()
        {
            // Arrange
            string method = "GET";

            // Act
            HttpMethod result = HttpRequestUtils.ConvertToHttpMethod(method);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(method, result.Method);
        }

        [Test]
        public void ConvertToHttpMethod_NullMethod_ThrowsArgumentNullException()
        {
            // Arrange
            string method = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => HttpRequestUtils.ConvertToHttpMethod(method));
        }

        [Test]
        public void ConvertToHttpMethod_EmptyMethod_ThrowsArgumentException()
        {
            // Arrange
            string method = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => HttpRequestUtils.ConvertToHttpMethod(method));
        }

        [Test]
        public void CombineUri_WithSchemes_CreatesCombinedUri()
        {
            // Arrange
            string host = "example.com";
            string basePath = "api";
            string route = "endpoint";
            ICollection<string> schemes = new List<string> { "https" };

            // Act
            string result = HttpRequestUtils.CombineUri(host, basePath, route, schemes);

            // Assert
            Assert.AreEqual("https://example.com/api/endpoint", result);
        }

        [Test]
        public void CombineUri_WithServerAndRoute_CreatesCombinedUri()
        {
            // Arrange
            string server = "http://localhost:8080";
            string route = "api/endpoint";

            // Act
            string result = HttpRequestUtils.CombineUri(server, route);

            // Assert
            Assert.AreEqual("http://localhost:8080/api/endpoint", result);
        }

        [Test]
        public void CombineUri_NullServer_ThrowsArgumentNullException()
        {
            // Arrange
            string server = null;
            string route = "api/endpoint";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => HttpRequestUtils.CombineUri(server, route));
        }

        [Test]
        public void CombineUri_EmptyRoute_ThrowsArgumentException()
        {
            // Arrange
            string server = "http://localhost:8080";
            string route = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => HttpRequestUtils.CombineUri(server, route));
        }

        [Test]
        public void GetPreferredScheme_HttpsSchemeExists_ReturnsHttpsScheme()
        {
            // Arrange
            ICollection<string> schemes = new List<string> { "http", "https" };

            // Act
            string result = HttpRequestUtils.GetPreferredScheme(schemes);

            // Assert
            Assert.AreEqual("https", result);
        }

        [Test]
        public void GetPreferredScheme_OnlyHttpSchemeExists_ReturnsHttpScheme()
        {
            // Arrange
            ICollection<string> schemes = new List<string> { "http" };

            // Act
            string result = HttpRequestUtils.GetPreferredScheme(schemes);

            // Assert
            Assert.AreEqual("http", result);
        }

        [Test]
        public void GetPreferredScheme_NoSchemes_ReturnsHttpSchemeByDefault()
        {
            // Arrange
            ICollection<string> schemes = new List<string>();

            // Act
            string result = HttpRequestUtils.GetPreferredScheme(schemes);

            // Assert
            Assert.AreEqual("http", result);
        }
    }
}
