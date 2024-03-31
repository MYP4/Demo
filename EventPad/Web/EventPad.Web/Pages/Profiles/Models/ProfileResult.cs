using System.Text.Json.Serialization;

namespace EventPad.Web.Pages.Profiles.Models;

public class ProfileResult
{
    public bool IsSuccessful { get; set; }
    [JsonPropertyName("fieldErrors")] public List<ErrorInfo> Errors { get; set; }
}

public class ErrorInfo
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}