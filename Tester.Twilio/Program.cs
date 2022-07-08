using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// pull secrets from launchSettings
var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
var fromNumber = Environment.GetEnvironmentVariable("TWILIO_FROM_NUMBER");

TwilioClient.Init(accountSid, authToken);

// build and send message
var message = MessageResource.Create(
    body: "I am not superstitious, but I am a little stitious.",
    from: new PhoneNumber(fromNumber),
    to: new PhoneNumber("+15558675309")
);

// print response
if (message != null)
{
    Console.WriteLine($"Twilio Response StatusCode: {message.Status}");
    Console.WriteLine($"Twilio Response Body: {message.Body}");
}
else
{
    Console.WriteLine("Twilio response was null");
}