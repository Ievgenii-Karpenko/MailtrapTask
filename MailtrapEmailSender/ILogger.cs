﻿namespace MailtrapEmailSender.EmailParameters;

public interface ILogger
{
    void Error(string message);
    void Info(string message);
}