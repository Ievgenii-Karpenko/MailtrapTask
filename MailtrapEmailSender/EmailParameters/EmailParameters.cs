using System.Text.Json.Serialization;

namespace MailtrapEmailSender.EmailParameters;

public class EmailParameters
{
    public List<Recipient> To { get; set; }
    public List<Recipient> Cc { get; set; }
    public List<Recipient> Bcc { get; set; }
    public Recipient From { get; set; }
    public List<Attachment> Attachments { get; set; }

    [JsonPropertyName("custom_variables")]
    public Dictionary<string, string> CustomVariables { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public string Category { get; set; }
}
