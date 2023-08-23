using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using APIWizard.Constants;
using APIWizard.Enums;
using APIWizard.Exceptions;
using APIWizard.Models.Abstracts;
using Newtonsoft.Json;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage AddInputData(this HttpRequestMessage httpRequestMessage, object? inputData = null, string? contentType = null, ParameterBase[]? parameters = null)
        {
            if (inputData == null)
            {
                return httpRequestMessage;
            }

            try
            {
                NormalizeAndAddInputToRequest(httpRequestMessage, parameters, inputData, contentType);
            }
            catch (Exception ex)
            {
                if (ex is JsonSerializationException)
                {
                    throw new APIClientException(ExceptionMessages.SerializationError, ex);
                }
                throw new APIClientException(ExceptionMessages.UnexpectedError, ex);
            }

            return httpRequestMessage;
        }

        private static void SetCookie(this HttpRequestMessage httpRequestMessage, CookieCollection? cookieCollection = null)
        {
            if (cookieCollection == null || cookieCollection.Count == 0)
            {
                return;
            }

            httpRequestMessage.Headers.Remove(HttpClientDefaults.Cookie);

            foreach (Cookie? cookie in cookieCollection)
            {
                if (cookie != null)
                {
                    httpRequestMessage.Headers.Add(HttpClientDefaults.Cookie, cookie.ToString());
                }
            }
        }


        private static void NormalizeAndAddInputToRequest(HttpRequestMessage request, ParameterBase[]? parameters, object inputData, string contentType)
        {
            if (parameters == null)
            {
                return;
            }

            var cookieContainer = new CookieCollection();

            foreach (var parameterGroup in parameters.GroupBy(p => p.In).OrderBy(p => p.Key))
            {
                switch (parameterGroup.Key)
                {
                    case ParameterType.Body:
                        var parameterValue = GetValueFromInputData(inputData, HttpClientDefaults.Body);
                        var content = CreateStringContent(parameterValue ?? inputData, contentType);
                        request.Content = content;
                        break;

                    case ParameterType.FormData:
                        var formDataValues = new List<(string Key, object? Value)>();
                        foreach (var parameter in parameterGroup)
                        {
                            var value = GetValueFromInputData(inputData, parameter.Name);
                            formDataValues.Add((parameter.Name, value));
                        }

                        if (string.Equals(contentType, ContentTypes.MultipartFormData, StringComparison.OrdinalIgnoreCase))
                        {
                            var formData = CreateMultipartFormDataContent(formDataValues);
                            request.Content = formData;
                        }
                        else if (string.Equals(contentType, ContentTypes.FormUrlEncoded, StringComparison.OrdinalIgnoreCase))
                        {
                            var formData = CreateFormUrlEncodedContent(formDataValues);
                            request.Content = formData;
                        }
                        break;

                    case ParameterType.Path:
                        ReplacePathParameters(request, parameterGroup, inputData);
                        break;

                    case ParameterType.Query:
                        AddQueryParameters(request, parameterGroup, inputData);
                        break;

                    case ParameterType.Header:
                        AddHeaders(request, parameterGroup, inputData);
                        break;

                    case ParameterType.Cookie:
                        AddCookies(request, parameterGroup, inputData, cookieContainer);
                        break;
                }
            }

            request.SetCookie(cookieContainer);
        }

        private static void ReplacePathParameters(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            var originalUri = request.RequestUri?.ToString();
            if (originalUri != null && !originalUri.ContainsCurlyBraces())
            {
                originalUri = originalUri.ToLowerInvariant();
            }

            foreach (var parameterBase in parameters)
            {
                string parameterName = "{" + parameterBase.Name + "}";
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameterName));

                originalUri = originalUri?.Replace(parameterName, Uri.EscapeDataString(parameterValue ?? string.Empty));
            }

            request.RequestUri = new Uri(originalUri ?? string.Empty);
        }

        private static void AddQueryParameters(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            foreach (var parameterBase in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameterBase.Name));
                var uriBuilder = new UriBuilder(request.RequestUri ?? new Uri(HttpClientDefaults.BlankUri));
                var query = uriBuilder.Query;
                query += string.IsNullOrEmpty(query) ? $"{parameterBase.Name}={parameterValue}" : $"&{parameterBase.Name}={parameterValue}";
                uriBuilder.Query = query;
                request.RequestUri = uriBuilder.Uri;
            }
        }

        private static void AddHeaders(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            foreach (var parameterBase in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameterBase.Name));
                request.Headers.Add(parameterBase.Name, parameterValue);
            }
        }

        private static void AddCookies(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData, CookieCollection cookieContainer)
        {
            foreach (var parameterBase in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameterBase.Name));
                cookieContainer.Add(new Cookie(parameterBase.Name, parameterValue));
            }
            request.SetCookie(cookieContainer);
        }

        private static StringContent CreateStringContent(object inputData, string contentType)
        {
            var jsonInput = JsonConvert.SerializeObject(inputData);
            return new StringContent(jsonInput, Encoding.UTF8, contentType ?? ContentTypes.ApplicationJson);
        }

        private static MultipartFormDataContent CreateMultipartFormDataContent(List<(string Key, object? Value)> formDataValues)
        {
            var formData = new MultipartFormDataContent();

            foreach (var data in formDataValues)
            {
                if (data.Value is Stream stream)
                {
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentTypes.MultipartFormData);
                    formData.Add(fileContent, data.Key ?? string.Empty, data.Value.ToString() ?? string.Empty);
                }
                else
                {
                    formData.Add(new StringContent(data.Value?.ToString() ?? string.Empty), data.Key ?? string.Empty);
                }
            }

            return formData;
        }

        private static FormUrlEncodedContent CreateFormUrlEncodedContent(List<(string Key, object? Value)> formDataValues)
        {
            var formDataDictionary = formDataValues.ToDictionary(d => d.Key.ToString(), d => d.Value?.ToString());
            return new FormUrlEncodedContent(formDataDictionary);
        }

        private static object? GetValueFromInputData(object inputData, string parameterName)
        {
            if (inputData is IDictionary inputDict && inputDict.Contains(parameterName))
            {
                return inputDict[parameterName];
            }

            return null;
        }
    }
}

