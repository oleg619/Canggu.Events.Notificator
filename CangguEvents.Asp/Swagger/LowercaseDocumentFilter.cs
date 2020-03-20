using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CangguEvents.Asp.Swagger
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = swaggerDoc.Paths
                .Where(pair => pair.Key.ToLower() != pair.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var (key, pathItem) in newPaths)
            {
                swaggerDoc.Paths.Add(key.ToLower(), pathItem);
                swaggerDoc.Paths.Remove(key);
            }
        }
    }
}