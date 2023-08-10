using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage AddInputData(this HttpRequestMessage httpRequestMessage, object? inputData = null, string? contentType = null)
        {
            if (inputData == null)
            {
                return httpRequestMessage;
            }

            try
            {
                HttpContent httpContent = GetHttpContent(inputData, contentType);
                httpRequestMessage.Content = httpContent;
            }
            catch (JsonSerializationException jsonEx)
            {
                throw new APIClientException(ExceptionMessages.SerializationError, jsonEx);
            }
            catch (Exception ex)
            {
                throw new APIClientException(ExceptionMessages.UnexpectedError, ex);
            }

            return httpRequestMessage;
        }


        private static HttpContent GetHttpContent(object inputData, string? contentType)
        {
            if (inputData is IDictionary inputDict)
            {
                if (string.Equals(contentType, ContentTypes.MultipartFormData, StringComparison.OrdinalIgnoreCase))
                {
                    var formData = new MultipartFormDataContent();

                    foreach (DictionaryEntry formEntry in inputDict)
                    {
                        if (formEntry.Value is Stream stream)
                        {
                            var fileContent = new StreamContent(stream);
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentTypes.MultipartFormData);
                            formData.Add(fileContent, HttpClientDefaults.MultipartFormDataName, formEntry.Key.ToString() ?? string.Empty);
                        }
                        else
                        {
                            formData.Add(new StringContent(formEntry.Value?.ToString() ?? string.Empty), formEntry.Key.ToString() ?? string.Empty);
                        }
                    }

                    return formData;
                }
                if (string.Equals(contentType, ContentTypes.FormUrlEncoded, StringComparison.OrdinalIgnoreCase))
                {
                    var formData = new FormUrlEncodedContent(HttpRequestUtils.ConvertToEnumerable<string, string>(inputDict));

                    return formData;
                }
                else
                {
                    throw new InvalidOperationException(ExceptionMessages.ContentTypeNotSupported);
                }
            }
            else
            {
                var jsonInput = JsonConvert.SerializeObject(inputData);
                return new StringContent(jsonInput, Encoding.UTF8, contentType ?? ContentTypes.ApplicationJson);
            }
        }


    }
}
