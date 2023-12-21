using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Trackster.API.Helper.AuthenticationAuthorization
{
    public class AuthorizationSwaggerHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "authentication-token",
                In = ParameterLocation.Header,
                Description = "upisati token preuzet iz autentikacijacontrollera"
            });
        }
    }
}
