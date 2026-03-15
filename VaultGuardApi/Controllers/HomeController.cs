using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace VaultGuardApi.Controllers;

[ApiController]
[Route("")]
public sealed class HomeController(IWebHostEnvironment env) : ControllerBase
{
    private readonly IWebHostEnvironment _env = env;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<User> users = [
            new() { Name = "Kleant", Surname = "Bajraktari", Age = 19 },
            new() { Name = "Test", Surname = "User", Age = 20 }
        ];

        var csvContent = string.Join("\n", users.Select(u => $"{u.Name},{u.Surname},{u.Age}"));
    
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent));

        var blobUri = new Uri("https://kleostorage.blob.core.windows.net");
        var blobServiceClient = new BlobServiceClient(blobUri, new DefaultAzureCredential());
        var containerClient = blobServiceClient.GetBlobContainerClient("blobs");
    
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient($"users-{Guid.NewGuid()}.csv");
        await blobClient.UploadAsync(stream, overwrite: true);

        return Ok(users);
    }
}