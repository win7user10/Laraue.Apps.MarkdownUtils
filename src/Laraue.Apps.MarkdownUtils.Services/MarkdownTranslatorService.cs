using Laraue.Apps.MarkdownUtils.Contracts;
using Laraue.Core.Exceptions.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Laraue.Apps.MarkdownUtils.Services;

public interface IMarkdownTranslatorService
{
    public Task<MarkdownTranslateResponse> Translate(MarkdownTranslateRequest request);
}

public class MarkdownTranslatorService(
    IOpenAiClient openAiClient,
    ILogger<MarkdownTranslatorService> logger,
    IOptions<MarkdownTranslatorOptions> options)
    : IMarkdownTranslatorService
{
    private const string SystemPrompt = @"Perform the translation of markdown starting from the next row from {0} to {1}.
Keep original formatting layout";
    
    public async Task<MarkdownTranslateResponse> Translate(MarkdownTranslateRequest request)
    {
        if (request.From == request.To)
            throw new BadRequestException(
                nameof(request.To),
                "Translation could not be performed to the same language as selected in 'From'");
        
        var prompt = string.Format(SystemPrompt, request.From, request.To);
        
        var response = await openAiClient.ChatCompletion(new ChatCompletionRequest
        {
            MaxTokens = Constraints.MaxTranslationOutput,
            Messages =
            [
                new Message { Role = "system", Content = prompt },
                new Message { Role = "user", Content = request.Content },
            ],
            Model = options.Value.Model,
            Stream = false,
            Thinking = new Thinking
            {
                Type = options.Value.Thinking ? "enabled" : "disabled"
            },
        });
        
        logger.LogInformation("Translation completed. Usage: {Usage}", response.Usage);

        return new MarkdownTranslateResponse
        {
            Content = response.Choices[0].Message.Content
        };
    }
}