using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Tester.Api;

// pull secrets from launchSettings
var _baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
var _username = Environment.GetEnvironmentVariable("USERNAME");
var _password = Environment.GetEnvironmentVariable("PASS");

var attempts = 2000;
var endTime = DateTime.UtcNow;
var totalPatients = 0;
var requestTime = DateTime.MinValue;

var client = new HttpClient();

var loginResponse = await client.PostAsync($"{_baseUrl}/account/login",
    new StringContent(JsonConvert.SerializeObject(new LoginPayload(_username, _password)),
        Encoding.UTF8, "application/json"));
        
loginResponse.EnsureSuccessStatusCode();

Console.WriteLine("Login Successful");

var tokenResponse =
    JsonConvert.DeserializeObject<TokenResponse>(await loginResponse.Content.ReadAsStringAsync());

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);

for (var request = 1; request <= attempts; request++)
{
    requestTime = endTime.AddMinutes(-15 * request);

    var getResponse = await client.GetAsync($"{_baseUrl}/patient?dateTime={requestTime:O}");
    getResponse.EnsureSuccessStatusCode();

    var patients = JsonConvert.DeserializeObject<List<Patient>>(await getResponse.Content.ReadAsStringAsync());

    if (patients?.Count > 0)
    {
        Console.WriteLine($"timestamp {requestTime} produced {patients.Count} results");
        totalPatients += patients.Count;
    }
    //else 
    //    Console.WriteLine($"no results for found for {requestTime}");
    
    Thread.Sleep(10);
}

Console.WriteLine($"A total of {totalPatients} records were found between {requestTime} and {endTime}");

Console.WriteLine("Press any key to exit.");
Console.ReadLine();