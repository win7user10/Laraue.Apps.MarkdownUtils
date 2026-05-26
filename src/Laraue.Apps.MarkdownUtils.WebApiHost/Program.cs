using Laraue.Apps.MarkdownUtils.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IMarkdownTranspilerService, MarkdownTranspilerService>();
builder.Services.AddSingleton<IMarkdownTranslatorService, MarkdownTranslatorService>();
builder.Services.AddSingleton<Laraue.Interpreter.Markdown.IMarkdownTranspiler, Laraue.Interpreter.Markdown.MarkdownTranspiler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Markdown Utils API")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
    });
}

var origins = builder
    .Configuration
    .GetRequiredSection("Cors:Hosts")
    .Get<string[]>() ?? throw new InvalidOperationException();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.WithOrigins(origins)
        .AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.MapHealthChecks("/_health");
app.MapControllers();
app.Run();