using CardOperationsService.Application.Cards.Queries.GetAllowedCardActions;
using CardOperationsService.Domain.Abstractions;
using CardOperationsService.Infrastructure.Services;

namespace CardOperationsService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Information);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddSingleton<ICardService, InMemoryCardService>();

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<GetAllowedCardActionsQuery>());

        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        
        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Handling: {context.Request.Method} {context.Request.Path}");
            await next();
            logger.LogInformation($"Completed: {context.Request.Method} {context.Request.Path} → {context.Response.StatusCode}");
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Unhandled exception: {context.TraceIdentifier}");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    Message = "An unexpected error occurred.",
                    RequestId = context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(error);
            });
        });

        
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (ArgumentException ex)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogWarning(ex, "ArgumentException: {Message}", ex.Message);

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
        });

        app.UseAuthorization();
        app.MapControllers();

        
        app.MapGet("/", () => {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Health check endpoint called");
            return "Card Operations Service is running!";
        });

        app.Run();
    }
}