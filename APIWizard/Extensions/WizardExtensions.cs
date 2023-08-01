﻿using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using APIWizard.Constants;
using APIWizard.Exceptions;
using Newtonsoft.Json;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage AddRequestBody(this HttpRequestMessage httpRequestMessage, object? requestBody, string? contentType)
        {
            if (requestBody != null)
            {
                try
                {
                    HttpContent httpContent = GetHttpContent(requestBody, contentType);
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
            }

            return httpRequestMessage;
        }

        private static HttpContent GetHttpContent(object requestBody, string? contentType)
        {
            if (requestBody is IDictionary requestBodyDictionary)
            {
                var formData = new MultipartFormDataContent();

                foreach (DictionaryEntry formEntry in requestBodyDictionary)
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
            else
            {
                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                return new StringContent(jsonRequest, Encoding.UTF8, contentType ?? ContentTypes.ApplicationJson);
            }
        }

    }
}
