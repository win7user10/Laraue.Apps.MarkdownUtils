using Laraue.Apps.MarkdownUtils.Contracts;
using Laraue.Interpreter.Common;
using ITranspiler = Laraue.Interpreter.Markdown.IMarkdownTranspiler;

namespace Laraue.Apps.MarkdownUtils.Services;

public interface IMarkdownTranspilerService
{
    public MarkdownTranspileResponse Transpile(MarkdownTranspileRequest request);
}

public class MarkdownTranspilerService(ITranspiler transpiler) : IMarkdownTranspilerService
{
    public MarkdownTranspileResponse Transpile(MarkdownTranspileRequest request)
    {
        try
        {
            var result = transpiler.ToHtml(request.Content);
            return new MarkdownTranspileResponse
            {
                Headers = result.Headers
                    .Select(h => new MarkdownHeader
                    {
                        PropertyName = h.PropertyName,
                        Value = h.Value,
                    })
                    .ToArray(),
                HtmlContent = result.HtmlContent,
                Error = null,
            };
        }
        catch (CompileException e)
        {
            return new MarkdownTranspileResponse
            {
                Headers = [],
                HtmlContent = null,
                Error = e.Message,
            };
        }
    }
}