using RestSharp;

namespace MailtrapEmailSender.EmailParameters
{
    internal class MailtrapRestClient : IMailtrapRestClient
    {
        private RestClient _restClient;

        public MailtrapRestClient(RestClient restClient)
        {
            _restClient = restClient;
        }

        public void AddDefaultHeader(string name, string value)
        {
            _restClient.AddDefaultHeader(name, value);
        }

        public async Task<RestResponse> PostAsync(RestRequest request)
        {
            return await _restClient.PostAsync(request);
        }
    }
}