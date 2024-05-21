using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;

namespace Manero_Backoffice.Services;

public class ImageService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> UploadImageAsync(IBrowserFile file)
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:BlobApi");

            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream(maxAllowedSize: 104857600);
            using var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "file", file.Name);

            var response = await Http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent.Trim('"');
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        return null!;
    }
}
