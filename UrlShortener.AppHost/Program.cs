using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.UrlShortener>("urlshortener");

apiService.WithCommand(
        "swagger-ui-docs",
        "Swagger UI Documentation",
        executeCommand: async _ =>
        {
            try
            {
                var endpoint = apiService.GetEndpoint("https");

                var url = $"{endpoint.Url}/swagger";

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                return new ExecuteCommandResult { Success = true };
            }
            catch (Exception ex)
            {
                return new ExecuteCommandResult { Success = false, ErrorMessage = ex.ToString() };
            }
        },
        updateState: context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ?
            ResourceCommandState.Enabled : ResourceCommandState.Disabled,
            iconName: "Document",
            iconVariant: IconVariant.Filled
    );



builder.Build().Run();
