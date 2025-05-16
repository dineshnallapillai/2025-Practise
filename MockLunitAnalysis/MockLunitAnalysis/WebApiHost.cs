using System.Text.Json;

namespace MockLunitAnalysis
{
    public class WebApiHost
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WebApiHost> _logger;

        public WebApiHost(
            IConfiguration configuration,
            ILogger<WebApiHost> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RunWebApiAsync(CancellationToken cancellationToken)
        {
            bool mockLicenseEnabled = _configuration.GetValue("MockLunitLicense", false);
            var urls = new List<string> { "http://localhost:10401" };

            if (mockLicenseEnabled)
            {
                _logger.LogInformation("MockLicense is enabled. Port 1948 will be used for mock license requests.");
                urls.Add("http://localhost:1948");
            }

            var builder = WebApplication.CreateBuilder();
            builder.WebHost.UseUrls(urls.ToArray());
            builder.Services.AddLogging();
            builder.Services.AddSingleton(_configuration);

            var app = builder.Build();
            int waitTime = _configuration.GetValue<int>("LunitAnalysisWaitime");

            app.Use(async (context, next) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<WebApiHost>>();
                logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
                await next.Invoke();
                logger.LogInformation($"Response: {context.Response.StatusCode}");
            });

            app.MapPost("/api/v1/inference", async (HttpContext context) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<WebApiHost>>();
                var contentRootPath = app.Environment.ContentRootPath;

                try
                {
                    foreach (var header in context.Request.Headers)
                        logger.LogInformation($"Header: {header.Key} = {header.Value}");

                    if (!context.Request.HasFormContentType)
                        return Results.BadRequest("Invalid content type. Expected 'multipart/form-data'.");

                    var form = await context.Request.ReadFormAsync();
                    if (!form.TryGetValue("parameters", out var parametersJson))
                        return Results.BadRequest("Missing 'parameters' field in the form data.");

                    logger.LogInformation($"Received parameters: {parametersJson}");
                    var parameters = JsonSerializer.Deserialize<JsonElement>(parametersJson!);

                    if (!parameters.TryGetProperty("creation", out var creation))
                        return Results.BadRequest("Missing 'creation' property in 'parameters'.");

                    bool scReport = creation.GetProperty("sc_report").GetBoolean();
                    bool scMap = creation.GetProperty("sc_map").GetBoolean();

                    string fileName = scReport && scMap ? "response" :
                                      scReport ? "scReport0.dcm" :
                                      scMap ? "scMap0.dcm" :
                                      string.Empty;

                    if (string.IsNullOrWhiteSpace(fileName))
                        return Results.BadRequest("Invalid request. No valid file selected for download.");

                    var filePath = Path.Combine(contentRootPath, "Data", fileName);
                    if (!File.Exists(filePath))
                        return Results.NotFound($"DICOM file '{fileName}' not found.");

                    logger.LogInformation($"Delaying response for {waitTime} seconds...");
                    await Task.Delay(TimeSpan.FromSeconds(waitTime));

                    var fileBytes = await File.ReadAllBytesAsync(filePath);
                    return Results.File(fileBytes, "application/dicom", fileName);
                }
                catch (JsonException ex)
                {
                    logger.LogError(ex, "Error parsing 'parameters' JSON.");
                    return Results.BadRequest("Invalid JSON in the 'parameters' field.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred.");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/api/v1/label/1", async (HttpContext context) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<WebApiHost>>();
                var contentRootPath = app.Environment.ContentRootPath;
                var fileName = "label.png";
                var filePath = Path.Combine(contentRootPath, "Data", fileName);

                if (!File.Exists(filePath))
                {
                    logger.LogWarning($"Image file '{fileName}' not found.");
                    return Results.NotFound($"Image file '{fileName}' not found.");
                }

                logger.LogInformation($"Returning image file '{fileName}'.");
                var fileBytes = await File.ReadAllBytesAsync(filePath);
                return Results.File(fileBytes, "image/png", fileName);
            });

            if (mockLicenseEnabled)
            {
                app.MapGet("/api/keys", (HttpContext context) =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<WebApiHost>>();
                    var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

                    var response = new[]
                    {
                        new
                        {
                            id = "44157676413252866",
                            product = "Lunit INSIGHT CXR 3",
                            product_id = 13003,
                            is_cloned = false,
                            is_usable = configuration.GetValue("LicenseMock:is_usable", true),
                            is_expired = configuration.GetValue("LicenseMock:is_expired", false),
                            current_usage_count = configuration.GetValue("LicenseMock:current_usage_count", 1185),
                            limit_usage_count = configuration.GetValue("LicenseMock:limit_usage_count", 2500),
                            margin_usage_count = 0,
                            license_type = "expiration",
                            license_exp_time = 1945987199,
                            expiration_date = configuration.GetValue("LicenseMock:expiration_date", "2031-09-01"),
                            activation_date = "2024-05-28",
                            status_code = 0
                        }
                    };

                    logger.LogInformation("Returning mock license data for /api/keys");
                    return Results.Json(response);
                });
            }

            await app.RunAsync(cancellationToken);
        }
    }
}
