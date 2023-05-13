using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mindr.Api.Swagger;

public class FieldsSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var fields = context.Type.GetFields();

        if (fields == null) return;
        if (fields.Length == 0) return;

        foreach (var field in fields)
        {
            schema.Properties[field.Name] = new OpenApiSchema
            {
                // this should be mapped to an OpenApiSchema type
                Type = field.FieldType.Name
            };
        }
    }
}