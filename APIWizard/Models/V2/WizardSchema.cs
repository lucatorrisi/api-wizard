﻿using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Models.V2
{
    internal class WizardSchema : WizardSchemaBase, IWizardSchema
    {
        [JsonProperty("host")]
        internal string Host { get; set; }
        [JsonProperty("basePath")]
        internal string BasePath { get; set; }
        [JsonProperty("schemes")]
        internal string[] Schemes { get; set; }
        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, PathDetail?>>? Paths { get; set; }

        public HttpRequestMessage? BuildRequest(string pathName, object? requestBody, string method = null, string server = null)
        {
            HttpRequestMessage request = null;
            PathDetail? pathDetail = null;

            if (pathName == null)
                throw new ArgumentNullException("pathName");
            if (Paths == null)
                throw new InvalidOperationException("Error");

            if (Paths.TryGetValue(pathName, out var path))
            {
                if (path == null)
                    throw new InvalidOperationException("Error");

                if (method != null && path.TryGetValue(method, out pathDetail))
                {
                    if (pathDetail == null)
                        throw new InvalidOperationException("Error");
                }
                else
                {
                    method = path.FirstOrDefault().Key;
                    pathDetail = path.FirstOrDefault().Value;
                }

                request = new HttpRequestMessage(
                    HttpRequestUtils.ConvertToHttpMethod(method),
                    GetUri(pathName)).AddRequestBody(requestBody, pathDetail?.GetContentType()
                    );
            }

            return request;
        }

        internal override Uri GetUri(string route)
        {
            return new(HttpRequestUtils.CombineUri(Host, BasePath, route, Schemes));
        }

        internal override Uri GetUri(string server, string route)
        {
            return GetUri(route);
        }
    }
}
