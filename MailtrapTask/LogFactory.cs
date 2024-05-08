using MailtrapEmailSender.EmailParameters;

internal class LogFactory
{
    internal static ILogger GetCurrentClassLogger()
    {
        return new ConsoleLogger();
    }

    public class ConsoleLogger : ILogger
    {
        public void Error(string message)
        {
            Console.WriteLine($"\tERROR: {message}");
        }

        public void Info(string message)
        {
            Console.WriteLine($"\tINFO: {message}");
        }
    }
}