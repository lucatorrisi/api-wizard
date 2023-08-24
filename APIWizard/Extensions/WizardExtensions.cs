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
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage AddInputData(
            this HttpRequestMessage httpRequestMessage,
            object? inputData = null,
            string? contentType = null,
            ParameterBase[]? parameters = null,
            bool isBodyRequired = false)
        {
            if (inputData == null)
            {
                return httpRequestMessage;
            }

            try
            {
                NormalizeAndAddInputToRequest(httpRequestMessage, parameters, inputData, contentType, isBodyRequired);
            }
            catch (JsonSerializationException ex)
            {
                throw new APIClientException(ExceptionMessages.SerializationError, ex);
            }
            catch (Exception ex)
            {
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

        private static void NormalizeAndAddInputToRequest(HttpRequestMessage request, ParameterBase[]? parameters, object inputData, string contentType, bool isBodyRequired)
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
                        ProcessBodyParameter(request, inputData, contentType, isBodyRequired);
                        break;

                    case ParameterType.FormData:
                        ProcessFormDataParameter(request, parameterGroup, inputData, contentType);
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

        private static void ProcessBodyParameter(HttpRequestMessage request, object inputData, string contentType, bool isBodyRequired)
        {
            var parameterValue = GetValueFromInputData(inputData, HttpClientDefaults.Body);
            if (isBodyRequired)
            {
                ValidationUtils.ParameterNotNull(parameterValue, nameof(HttpClientDefaults.Body), ExceptionMessages.MissingRequiredParameter);
            }

            var content = CreateStringContent(parameterValue ?? inputData, contentType);
            request.Content = content;
        }

        private static void ProcessFormDataParameter(HttpRequestMessage request, IEnumerable<ParameterBase> parameterGroup, object inputData, string contentType)
        {
            var formDataValues = new List<(string Key, object? Value)>();
            foreach (var parameter in parameterGroup)
            {
                var value = GetValueFromInputData(inputData, parameter.Name);
                if (parameter.Required)
                {
                    ValidationUtils.ParameterNotNull(value, parameter.Name, ExceptionMessages.MissingRequiredParameter);
                }
                formDataValues.Add((parameter.Name, value));
            }

            HttpContent formData;
            if (string.Equals(contentType, ContentTypes.MultipartFormData, StringComparison.OrdinalIgnoreCase))
            {
                formData = CreateMultipartFormDataContent(formDataValues);
            }
            else if (string.Equals(contentType, ContentTypes.FormUrlEncoded, StringComparison.OrdinalIgnoreCase))
            {
                formData = CreateFormUrlEncodedContent(formDataValues);
            }
            else
            {
                throw new APIClientException(ExceptionMessages.ContentTypeNotSupported);
            }

            request.Content = formData;
        }

        private static void ReplacePathParameters(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            var originalUri = request.RequestUri?.ToString();
            if (originalUri != null && !originalUri.ContainsCurlyBraces())
            {
                originalUri = originalUri.ToLowerInvariant();
            }

            foreach (var parameter in parameters)
            {
                string parameterName = "{" + parameter.Name + "}";
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameterName));
                if (parameter.Required)
                {
                    ValidationUtils.ParameterNotNullOrEmpty(parameterValue, parameter.Name, ExceptionMessages.MissingRequiredParameter);
                }

                originalUri = originalUri.Replace(parameterName, Uri.EscapeDataString(parameterValue ?? string.Empty));
            }

            request.RequestUri = new Uri(originalUri ?? string.Empty);
        }

        private static void AddQueryParameters(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            var uriBuilder = new UriBuilder(request.RequestUri ?? new Uri(HttpClientDefaults.BlankUri));
            var query = uriBuilder.Query;

            foreach (var parameter in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameter.Name));
                if (parameter.Required)
                {
                    ValidationUtils.ParameterNotNullOrEmpty(parameterValue, parameter.Name, ExceptionMessages.MissingRequiredParameter);
                }

                query += string.IsNullOrEmpty(query) ? $"{parameter.Name}={parameterValue}" : $"&{parameter.Name}={parameterValue}";
            }

            uriBuilder.Query = query;
            request.RequestUri = uriBuilder.Uri;
        }

        private static void AddHeaders(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData)
        {
            foreach (var parameter in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameter.Name));
                if (parameter.Required)
                {
                    ValidationUtils.ParameterNotNullOrEmpty(parameterValue, parameter.Name, ExceptionMessages.MissingRequiredParameter);
                }

                request.Headers.Add(parameter.Name, parameterValue);
            }
        }

        private static void AddCookies(HttpRequestMessage request, IEnumerable<ParameterBase> parameters, object inputData, CookieCollection cookieContainer)
        {
            foreach (var parameter in parameters)
            {
                var parameterValue = Convert.ToString(GetValueFromInputData(inputData, parameter.Name));
                if (parameter.Required)
                {
                    ValidationUtils.ParameterNotNullOrEmpty(parameterValue, parameter.Name, ExceptionMessages.MissingRequiredParameter);
                }

                cookieContainer.Add(new Cookie(parameter.Name, parameterValue));
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
