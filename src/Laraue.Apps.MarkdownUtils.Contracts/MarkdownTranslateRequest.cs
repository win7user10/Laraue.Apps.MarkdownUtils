using System.ComponentModel.DataAnnotations;

namespace Laraue.Apps.MarkdownUtils.Contracts;

public class MarkdownTranslateRequest
{
    [MaxLength(Constraints.MaxTranslationInput, ErrorMessage = "Free version allow to translate no more than 10 000 symbols")]
    public required string Content { get; init; }
    
    [MinLength(2)]
    [MaxLength(2)]
    public required string From { get; init; }
    
    [MinLength(2)]
    [MaxLength(2)]
    public required string To { get; init; }
}