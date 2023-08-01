namespace APIWizard.Models.Interfaces
{
    internal interface IWizardSchema
    {
        internal HttpRequestMessage? BuildRequest(string pathName, object? requestBody, string method = null, string server = null);
    }
}
