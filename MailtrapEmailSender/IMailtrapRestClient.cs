using RestSharp;

namespace MailtrapEmailSender.EmailParameters
{
    public interface IMailtrapRestClient
    {
        void AddDefaultHeader(string name, string value);

        Task<RestResponse> PostAsync(RestRequest request);
    }
}