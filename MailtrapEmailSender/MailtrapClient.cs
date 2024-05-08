using System.Net;
using RestSharp;
using System.Text.Json;

namespace MailtrapEmailSender.EmailParameters;

public class MailtrapClient
{
    private readonly IMailtrapRestClient _client;
    private readonly ILogger _logger;

    public MailtrapClient(string token, ILogger logger, string url = "https://send.api.mailtrap.io/api/send") 
        : this (token, logger, new MailtrapRestClient(new RestClient(url)))
    {
    }

    public MailtrapClient(string token, ILogger logger, IMailtrapRestClient client)
    {
        if (string.IsNullOrEmpty(token) || client is null)
        {
            throw new ArgumentException();
        }

        _logger = logger;
        _client = client;
        _client.AddDefaultHeader("Authorization", $"Bearer {token}");
    }

    public async Task<bool> SendEmailAsync(EmailParameters parameters)
    {
        try
        {
            var content = JsonSerializer.Serialize(parameters);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", content, ParameterType.RequestBody);
            var response = await _client.PostAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger?.Info("Email sent successfully.");
                return true;
            }
            else
            {
                _logger?.Error($"Failed to send email. Status code: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.Error($"Error sending email: {ex.Message}");
            return false;
        }
    }
}
