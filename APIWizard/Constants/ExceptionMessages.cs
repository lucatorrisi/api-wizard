namespace APIWizard.Constants
{
    public class ExceptionMessages
    {
        public const string InvalidFilePath = "Invalid file path";
        public const string InvalidPathName = "Path name cannot be null or empty";
        public const string InvalidMethod = "Method cannot be null or empty";
        public const string HttpClientNotInitialized = "HttpClient instance is not initialized.";
        public const string ConfigurationFileNotFound = "Configuration file not found at path:";
        public const string FailedReadConfigurationFile = "Failed to read configuration file:";
        public const string ConfigurationSectionNull = "Configuration section cannot be null";
        public const string FailedRetrieveRemoteConfig = "Failed to retrieve OpenAPI configuration:";
        public const string InvalidOpenAPIVersionNone = "OpenAPIVersion cannot be None";
        public const string InvalidOpenAPIVersion = "Invalid OpenAPIVersion specified";
        public const string InvalidConfiguration = "Invalid configuration";
        public const string SchemaIsNull = "Schema is null";
        public const string InvalidJson = "JSON string is null or empty";
        public const string InvalidOpenAPIConfigurationUrl = "OpenAPI URL string is null or empty";
        public const string InvalidConfigurationFileFormat = "Invalid configuration file format";
        public const string ServiceCollectionNull = "The services collection cannot be null";
        public const string APIClientNull = "The API client instance cannot be null";
        public const string ErrorBuildingRequest = "Error building the request message";
        public const string ErrorSendingRequest = "Error sending the HTTP request";
        public const string DeserializationError = "Error deserializing the response JSON";
        public const string SerializationError = "Unable to serialize the request body to JSON";
        public const string ContentTypeNotSupported = "Content type not supported";
        public const string UnexpectedError = "An unexpected error occurred";
        public const string MissingRequiredParameter = "Missing required parameter: {0}";
        public const string NoPathsFound = "The 'Paths' collection is null";
        public const string PathNotFound = "The specified path name does not exist in the 'Paths' collection";
        public const string MethodNotFound = "The specified method does not exist in the path details";
        public const string NoDefaultMethodFound = "No default method a found for the specified path name";
    }
}
