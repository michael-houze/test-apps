using SendGrid;
using SendGrid.Helpers.Mail;

// pull api key from launchSettings
var apiKey = Environment.GetEnvironmentVariable("API_KEY");
var client = new SendGridClient(apiKey);

// build message
var from = new EmailAddress("from@email.com", "From Name");
var subject = "Test Email Subject Line";
var to = new EmailAddress("to@email.com", "To Name");
var plainTextContent = "The text/plain content of the email body.";
var htmlContent = "The text/html content of the email body.";

// send message
var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
var response = await client.SendEmailAsync(message);

// print response
if (response != null)
{
    Console.WriteLine($"SendGrid Response StatusCode: {response.StatusCode}");
    Console.WriteLine($"SendGrid Response Body: {response.Body}");
}
else
{
    Console.WriteLine("SendGrid response was null");
}