using APIWizard.Constants;
using APIWizard.Enums;
using APIWizard.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Moq;

namespace APIWizard.Tests.Utils
{
    [TestFixture]
    public class OpenAPIUtilsTests
    {
        [Test]
        public void GetOpenAPIVersion_FromIConfigurationSection_ReturnsCorrectVersion()
        {
            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(x => x[Common.OpenAPIVersionPropertyName]).Returns(Common.OpenAPIVersionPrefix + "3.0.1");

            var result = OpenAPIUtils.GetOpenAPIVersion(configSectionMock.Object);

            Assert.AreEqual(OpenAPIVersion.V3, result);
        }

        [Test]
        public void GetOpenAPIVersion_FromJsonString_ReturnsCorrectVersion()
        {
            var jsonString = "{\"swagger\": \"2.0\"}";

            var result = OpenAPIUtils.GetOpenAPIVersion(jsonString);

            Assert.AreEqual(OpenAPIVersion.V2, result);
        }

        [Test]
        public void GetOpenAPIVersion_InvalidInput_ReturnsNone()
        {
            var invalidInput = new object();

            var result = OpenAPIUtils.GetOpenAPIVersion(invalidInput);

            Assert.AreEqual(OpenAPIVersion.None, result);
        }
    }
}
