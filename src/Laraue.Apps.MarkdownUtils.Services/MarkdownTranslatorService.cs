using Laraue.Apps.MarkdownUtils.Contracts;

namespace Laraue.Apps.MarkdownUtils.Services;

public interface IMarkdownTranslatorService
{
    public MarkdownTranslateResponse Translate(MarkdownTranslateRequest request);
}

public class MarkdownTranslatorService : IMarkdownTranslatorService
{
    public MarkdownTranslateResponse Translate(MarkdownTranslateRequest request)
    {
        throw new NotImplementedException();
    }
}