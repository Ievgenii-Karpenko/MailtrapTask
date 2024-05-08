using MailtrapEmailSender.EmailParameters;
using Microsoft.Extensions.Configuration;

var logger = LogFactory.GetCurrentClassLogger();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string token = configuration["MailtrapSettings:BearerToken"];
string endpoint = configuration["MailtrapSettings:Endpoint"];

var mailtrapClient = new MailtrapClient(token, logger, endpoint);

var senderName = "Mailtrap Test";
var senderEmail = "mailtrap@demomailtrap.com";
var recipientName = "Jane Smith";
var recipientEmail = "gmen.karpenko@gmail.com";
var subject = "Test Email";
var text = "Hello, this is a test email.";

var input = new EmailParameters
{
    From = new Recipient { Name = senderName, Email = senderEmail },
    To = new List<Recipient> { new Recipient { Name = recipientName, Email = recipientEmail } },
    Subject = subject,
    Text = text,
    Category = "cccc"
};

await mailtrapClient.SendEmailAsync(input);
