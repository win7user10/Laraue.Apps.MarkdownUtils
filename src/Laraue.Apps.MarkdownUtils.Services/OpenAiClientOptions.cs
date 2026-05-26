namespace Laraue.Apps.MarkdownUtils.Services;

public record OpenAiClientOptions
{
    public required string Token { get; set; }
    public required string BaseUrl { get; set; }
}