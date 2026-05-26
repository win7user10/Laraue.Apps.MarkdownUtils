using System.ComponentModel.DataAnnotations;

namespace Laraue.Apps.MarkdownUtils.Contracts;

public class MarkdownTranspileRequest
{
    [MaxLength(100000)]
    public required string Content { get; init; }
}