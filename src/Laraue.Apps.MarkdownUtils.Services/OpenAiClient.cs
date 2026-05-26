using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Laraue.Apps.MarkdownUtils.Services;

public interface IOpenAiClient
{
    Task<ChatCompletionResponse> ChatCompletion(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default);
}

public class OpenAiClient(HttpClient client) : IOpenAiClient
{
    public async Task<ChatCompletionResponse> ChatCompletion(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync(
            "/chat/completions",
            request,
            cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken);
        return content!;
    }
}

public record ChatCompletionRequest
{
    [JsonPropertyName("model")]
    public required string Model { get; init; }

    [JsonPropertyName("messages")]
    public required List<Message> Messages { get; init; }

    [JsonPropertyName("thinking")]
    public required Thinking Thinking { get; init; }

    [JsonPropertyName("max_tokens")]
    public required int MaxTokens { get; init; }

    [JsonPropertyName("stream")]
    public required bool Stream { get; init; }
}

public record Message
{
    [JsonPropertyName("role")]
    public required string Role { get; init; }

    [JsonPropertyName("content")]
    public required string Content { get; init; }
}

public record Thinking
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

public record ChatCompletionResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("model")]
    public required string Model { get; init; }

    [JsonPropertyName("choices")]
    public required List<Choice> Choices { get; init; }

    [JsonPropertyName("usage")]
    public required Usage Usage { get; init; }
}

public record Choice
{
    [JsonPropertyName("index")]
    public required int Index { get; init; }

    [JsonPropertyName("message")]
    public required Message Message { get; init; }

    [JsonPropertyName("finish_reason")]
    public required string FinishReason { get; init; }
}

public record Usage
{
    [JsonPropertyName("prompt_tokens")]
    public required int PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    public required int CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public required int TotalTokens { get; init; }

    [JsonPropertyName("prompt_cache_hit_tokens")]
    public required int PromptCacheHitTokens { get; init; }

    [JsonPropertyName("prompt_cache_miss_tokens")]
    public required int PromptCacheMissTokens { get; init; }
}