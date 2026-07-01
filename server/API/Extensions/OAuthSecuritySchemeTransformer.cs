using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace API.Extensions;

public class OAuthSecuritySchemeTransformer(IConfiguration config) : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken ct)
    {
        var tenantId = config["AzureAd:TenantId"];
        var authority = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0";
        var clientId = config["AzureAd:ClientId"];

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["OAuth2"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{authority}/authorize"),
                    TokenUrl = new Uri($"{authority}/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        [$"api://{clientId}/access_as_user"] = "Access API"
                    }
                }
            }
        };

        foreach (var operation in document.Paths.Values.SelectMany(p => p.Operations.Values))
        {
            operation.Security ??= new List<OpenApiSecurityRequirement>();
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("OAuth2", document)] = []
            });
        }

        return Task.CompletedTask;
    }
}