namespace APIWizard.Models.Interfaces
{
    internal interface IWizardSchema
    {
        internal HttpRequestMessage? BuildRequest(string pathName, object? inputData, string method = null, string server = null);
    }
}
