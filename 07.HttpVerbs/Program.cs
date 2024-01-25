using System.Text.RegularExpressions;

List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    // 2e752824-1657-4c7f-844b-6ec2e168e99c
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    if (path == "/api/users" && request.Method == "GET")
    {
        await GetAllPeople(response);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        // get id from request
        string? id = path.Value?.Split("/")[3];
        await GetPerson(id, response);
    }
    else if (path == "/api/users" && request.Method == "POST")
    {
        await CreatePerson(response, request);
    }
    else if (path == "/api/users" && request.Method == "PUT")
    {
        await UpdatePerson(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

// Bütün istifadəçilərin əldə olunması
async Task GetAllPeople(HttpResponse response)
{
    await response.WriteAsJsonAsync(users);
}
// id-yə uyğun olan istifadəçilərin əldə olunması
async Task GetPerson(string? id, HttpResponse response)
{
    // id-yə uyğun olan istifadəçinin əldə olunması
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // əgər istifadəçi tapılsa, onu göndər
    if (user != null)
        await response.WriteAsJsonAsync(user);
    // əgər tapılmasırsa, status kodu və xəta mesajı göndər
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "İtifadəçi aşkar edilməsi" });
    }
}

async Task DeletePerson(string? id, HttpResponse response)
{
    // id-yə uyğun istifadəçi tapırıq
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // əgər istifadəçi taplısa, onu silirik
    if (user != null)
    {
        users.Remove(user);
        await response.WriteAsJsonAsync(user);
    }
    // əgər tapılmasa, xəta göndəririk
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "İtifadəçi aşkar edilməsi" });
    }
}

async Task CreatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // istifadəçinin məlumatlarını əldə edirik
        var user = await request.ReadFromJsonAsync<Person>();
        if (user != null)
        {
            // Yeni bir id yaradırıq
            user.Id = Guid.NewGuid().ToString();
            // istifadəçini əlavə edirik
            users.Add(user);
            await response.WriteAsJsonAsync(user);
        }
        else
        {
            throw new Exception("Yanlış məlumat");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Yanlış məlumat" });
    }
}

async Task UpdatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // istifadəçi haqqında məlumat əldə edirik
        Person? userData = await request.ReadFromJsonAsync<Person>();
        if (userData != null)
        {
            // istifadəçinin id-sinə uyğun olan istifadəçini tapırıq
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // əgər istifadəçi aşkar edilsə, onu yeniləyirik və geriyə göndəririk
            if (user != null)
            {
                user.Age = userData.Age;
                user.Name = userData.Name;
                await response.WriteAsJsonAsync(user);
            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "İstifadəçi aşkar edilmədi" });
            }
        }
        else
        {
            throw new Exception("Yanlış məlumat");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Yanlış məlumat" });
    }
}
public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}