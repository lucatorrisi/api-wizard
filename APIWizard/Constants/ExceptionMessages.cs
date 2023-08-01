using System;

namespace APIWizard.Constants
{
    public class ExceptionMessages
    {
        public const string InvalidFilePath = "Invalid file path";
        public const string ConfigurationFileNotFound = "Configuration file not found";
        public const string InvalidOpenAPIVersionNone = "OpenAPIVersion cannot be None";
        public const string InvalidOpenAPIVersion = "Invalid OpenAPIVersion";
        public const string InvalidConfiguration = "Invalid configuration";
        public const string SchemaIsNull = "Schema is null";
        public const string InvalidJson = "JSON string is null or empty";
        public const string InvalidConfigurationFileFormat = "Invalid configuration file format";
        public const string ServiceCollectionNull = "The services collection cannot be null";
        public const string APIClientNull = "The API client instance cannot be null";
        public const string ErrorBuildingRequest = "Error building the request message";
        public const string ErrorSendingRequest = "Error sending the HTTP request";
        public const string DeserializationError = "Error deserializing the response JSON";
        public const string SerializationError = "Unable to serialize the request body to JSON";
        public const string UnexpectedError = "An unexpected error occurred";
    }
}
