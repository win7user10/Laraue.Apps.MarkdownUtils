namespace Laraue.Apps.MarkdownUtils.Services;

public record MarkdownTranslatorOptions
{
    public required string Model { get; set; }
    public required bool Thinking { get; set; }
}