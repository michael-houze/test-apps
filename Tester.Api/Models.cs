using Newtonsoft.Json;

namespace Tester.Api;

public class LoginPayload
{
    public string Email { get; set; }
        
    public string Password { get; set; }

    public LoginPayload(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class TokenResponse
{
    public string Token { get; set; }
        
    public string RefreshToken { get; set; }
        
    public DateTimeOffset Expiry { get; set; }
}

public class Patient
{
    public string Id { get; set; }
        
    public string MedicalRecordNumber { get; set; }
        
    public string UniversalRecordNumber { get; set; }
        
    public string FirstName { get; set; }
        
    public string LastName { get; set; }
        
    public string MiddleName { get; set; }
}