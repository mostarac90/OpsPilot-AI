var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpsPilot API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Health check endpoint
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();