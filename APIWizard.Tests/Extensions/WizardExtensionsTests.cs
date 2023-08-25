using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using APIWizard.Constants;
using APIWizard.Enums;
using APIWizard.Exceptions;
using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Utils;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace APIWizard.Tests
{
    [TestFixture]
    public class WizardExtensionsTests
    {
        [Test]
        public void AddInputDataV2_InputDataNotNull_ModifiesRequest()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();
            var inputData = new { Name = "John", Age = 30 };
            var contentType = ContentTypes.ApplicationJson;
            var parameter = new Models.V2.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V2.Parameter[]
            {
                parameter
            };
            var isBodyRequired = true;

            // Act
            var modifiedRequest = httpRequestMessage.AddInputData(inputData, contentType, parameters, isBodyRequired);

            // Assert
            Assert.AreSame(httpRequestMessage, modifiedRequest);
            // Additional assertions can be added here
        }

        [Test]
        public void AddInputDataV2_InputDataNull_ReturnsOriginalRequest()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();
            object inputData = null;
            var contentType = ContentTypes.ApplicationJson;
            var parameter = new Models.V2.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V2.Parameter[]
            {
                parameter
            };
            var isBodyRequired = true;

            // Act
            var modifiedRequest = httpRequestMessage.AddInputData(inputData, contentType, parameters, isBodyRequired);

            // Assert
            Assert.AreSame(httpRequestMessage, modifiedRequest);
        }

        [Test]
        public void AddInputDataV3_InputDataNotNull_ModifiesRequest()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();
            var inputData = new { Name = "John", Age = 30 };
            var contentType = ContentTypes.ApplicationJson;
            var parameter = new Models.V3.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V3.Parameter[]
            {
                parameter
            };
            var isBodyRequired = true;

            // Act
            var modifiedRequest = httpRequestMessage.AddInputData(inputData, contentType, parameters, isBodyRequired);

            // Assert
            Assert.AreSame(httpRequestMessage, modifiedRequest);
            // Additional assertions can be added here
        }

        [Test]
        public void AddInputDataV3_InputDataNull_ReturnsOriginalRequest()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();
            object inputData = null;
            var contentType = ContentTypes.ApplicationJson;
            var parameter = new Models.V3.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V3.Parameter[]
            {
                parameter
            };
            var isBodyRequired = true;

            // Act
            var modifiedRequest = httpRequestMessage.AddInputData(inputData, contentType, parameters, isBodyRequired);

            // Assert
            Assert.AreSame(httpRequestMessage, modifiedRequest);
        }

        [Test]
        public void SetCookie_ValidCookieCollection_SetsCookieHeaders()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();
            var cookieCollection = new CookieCollection
            {
                new Cookie("sessionId", "12345"),
                new Cookie("userId", "9876")
            };

            // Act
            httpRequestMessage.SetCookie(cookieCollection);

            // Assert
            Assert.IsTrue(httpRequestMessage.Headers.Contains(HttpClientDefaults.Cookie));
            // Additional assertions can be added here
        }

        [Test]
        public void NormalizeAndAddInputToRequestV2_ValidParameters_ModifiesRequest()
        {
            // Arrange
            var request = new HttpRequestMessage();
            var parameter = new Models.V2.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V2.Parameter[]
            {
                parameter
            };
            var inputData = new Dictionary<string, object> { { "Name", "John" } };
            var contentType = ContentTypes.ApplicationJson;
            var isBodyRequired = true;

            // Act
            WizardExtensions.NormalizeAndAddInputToRequest(request, parameters, inputData, contentType, isBodyRequired);

            // Assert
            // Add assertions to verify the modifications in the request
        }

        [Test]
        public void NormalizeAndAddInputToRequestV3_ValidParameters_ModifiesRequest()
        {
            // Arrange
            var request = new HttpRequestMessage();
            var parameter = new Models.V3.Parameter();
            parameter.Name = "Name";
            parameter.In = ParameterType.Header;
            var parameters = new Models.V3.Parameter[]
            {
                parameter
            };
            var inputData = new Dictionary<string, object> { { "Name", "John" } };
            var contentType = ContentTypes.ApplicationJson;
            var isBodyRequired = true;

            // Act
            WizardExtensions.NormalizeAndAddInputToRequest(request, parameters, inputData, contentType, isBodyRequired);

            // Assert
            // Add assertions to verify the modifications in the request
        }

        [Test]
        public void CreateStringContent_ValidInputData_CreatesStringContent()
        {
            // Arrange
            var inputData = new { Name = "John" };
            var contentType = ContentTypes.ApplicationJson;

            // Act
            var content = WizardExtensions.CreateStringContent(inputData, contentType);

            // Assert
            Assert.IsNotNull(content);
            Assert.AreEqual(contentType, content.Headers.ContentType.MediaType);
            // Additional assertions can be added here
        }

        [Test]
        public void CreateMultipartFormDataContent_ValidFormData_CreatesMultipartFormDataContent()
        {
            // Arrange
            var formDataValues = new List<(string Key, object? Value)>
            {
                ("Name", "John"),
                ("File", new MemoryStream(Encoding.UTF8.GetBytes("FileContents")))
            };

            // Act
            var formData = WizardExtensions.CreateMultipartFormDataContent(formDataValues);

            // Assert
            Assert.IsNotNull(formData);
            // Additional assertions can be added here
        }

        [Test]
        public void GetValueFromInputData_ValidInputData_ReturnsValue()
        {
            // Arrange
            var inputData = new Dictionary<string, object>
            {
                { "Name", "John" },
                { "Age", 30 }
            };
            var parameterName = "Name";

            // Act
            var value = WizardExtensions.GetValueFromInputData(inputData, parameterName);

            // Assert
            Assert.AreEqual("John", value);
        }

        [Test]
        public void GetValueFromInputData_InvalidParameter_ReturnsNull()
        {
            // Arrange
            var inputData = new { Name = "John" };
            var parameterName = "Age";

            // Act
            var value = WizardExtensions.GetValueFromInputData(inputData, parameterName);

            // Assert
            Assert.IsNull(value);
        }

        [Test]
        public void CreateFormUrlEncodedContent_ValidFormData_CreatesFormUrlEncodedContent()
        {
            // Arrange
            var formDataValues = new List<(string Key, object? Value)>
            {
                ("Name", "John"),
                ("Age", 30)
            };

            // Act
            var formData = WizardExtensions.CreateFormUrlEncodedContent(formDataValues);

            // Assert
            Assert.IsNotNull(formData);
            Assert.AreEqual(ContentTypes.FormUrlEncoded, formData.Headers.ContentType.MediaType);

            var formContent = formData.ReadAsStringAsync().Result;
            Assert.IsTrue(formContent.Contains("Name=John"));
            Assert.IsTrue(formContent.Contains("Age=30"));
        }
    }
}
