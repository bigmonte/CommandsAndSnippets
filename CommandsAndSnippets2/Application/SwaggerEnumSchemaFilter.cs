using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeLiturgy.Startup.Application
{
    public class SwaggerEnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply Swagger OpenApi schema
        /// </summary>
        /// <param name="model">OpenApiSchema model</param>
        /// <param name="context">Schema filter context</param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(n => model.Enum.Add(new OpenApiString(n)));
            }
        }
    }
}

