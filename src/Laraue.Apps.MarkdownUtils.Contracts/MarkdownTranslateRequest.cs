using System.ComponentModel.DataAnnotations;

namespace Laraue.Apps.MarkdownUtils.Contracts;

public class MarkdownTranslateRequest
{
    [MaxLength(50000)]
    public required string Content { get; init; }
    
    [MinLength(2)]
    [MaxLength(2)]
    public required string From { get; init; }
    
    [MinLength(2)]
    [MaxLength(2)]
    public required string To { get; init; }
}