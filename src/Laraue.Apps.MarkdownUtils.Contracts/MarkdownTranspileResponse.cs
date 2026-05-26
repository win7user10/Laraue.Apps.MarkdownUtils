namespace Laraue.Apps.MarkdownUtils.Contracts;

public class MarkdownTranspileResponse
{ 
    public required string? HtmlContent { get; init; }
    public required MarkdownHeader[] Headers { get; init; }
    public required string? Error { get; init; }
}

public record MarkdownHeader
{
    public required string PropertyName { get; set; }
    public required object? Value { get; set; }
}