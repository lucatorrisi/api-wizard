namespace APIWizard.Constants
{
    public static class HttpClientDefaults
    {
        public static TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(2);
        
        public const string HttpsSchema = "https";
        public const string HttpSchema = "http";
        public const string MultipartFormDataName = "file";
        public const string Cookie = "Cookie";
        public const string Body = "body";
        public const string BlankUri = "about:blank";

        public const string GetHttpMethod = "GET";
        public const string PostHttpMethod = "POST";
        public const string PutHttpMethod = "PUT";
        public const string PatchHttpMethod = "PATCH";
        public const string DeleteHttpMethod = "DELETE";
        public const string HeadHttpMethod = "HEAD";
        public const string OptionsHttpMethod = "OPTIONS";
    }
}
