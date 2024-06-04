using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

var token = HttpContext.Session.GetString("Jwt");
if (string.IsNullOrEmpty(token))
{
    return RedirectToAction("Login", "Account");
}

Console.WriteLine("JWT Token: " + token); // Ghi log token để kiểm tra
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);