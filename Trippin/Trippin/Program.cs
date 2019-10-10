class Program
{
    private static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient() { BaseAddress = new System.Uri("https://services.odata.org/TripPinRESTierService/(S(5yramqng23p4qsvfuy0ekoym))/)") };
    static async System.Threading.Tasks.Task Main()
    {
        foreach (var user in System.Text.Json.JsonSerializer.Deserialize<User[]>(await System.IO.File.ReadAllTextAsync("users.json")))
            if (!(await client.GetAsync("People('" + user.UserName + "')")).IsSuccessStatusCode) // only create user if user doesn't exist
                await client.PostAsync("People", new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(new { user.UserName, user.FirstName, user.LastName, Emails = new[] { user.Email }, AddressInfo = new[] { new { user.Address, City = new { Name = user.CityName, CountryRegion = user.Country, Region = "unknown" } } } }), System.Text.Encoding.UTF8, "application/json"));
    }
    public class User { public string UserName, FirstName, LastName, Email, Address, CityName, Country; }
}
