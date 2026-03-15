using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace VaultGuardApi.Controllers;

[ApiController]
[Route("")]
public sealed class HomeController(IWebHostEnvironment env) : ControllerBase
{
    private readonly IWebHostEnvironment _env = env;

    public async Task<IActionResult>  Get()
    {
        List<User> users = [
        new()
        {
            Name = "Kleant",
            Surname = "Bajraktari",
            Age = 18
        },
        
        new()
        {
            Name = "Test",
            Surname = "User",
            Age = 20
        },
        new()
        {
            Name = "Test2",
            Surname = "User2",
            Age = 25
        }
        ];
        string filePath = Path.Combine(_env.WebRootPath, "content.txt");
        var blobUri = new Uri("https://kleostorage.blob.core.windows.net");
        var blobServiceClient = new BlobServiceClient(blobUri,new DefaultAzureCredential());
        var containerClient = blobServiceClient.GetBlobContainerClient("blobs");
        var blobClient = containerClient.GetBlobClient($"file-{Guid.NewGuid()}");
        await blobClient.UploadAsync(filePath,overwrite:true);
        return Ok(users);
    }
}