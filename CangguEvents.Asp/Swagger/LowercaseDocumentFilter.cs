using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CangguEvents.Asp.Swagger
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = new Dictionary<string, OpenApiPathItem>();
            var removeKeys = new List<string>();
            foreach (var (key, pathItem) in swaggerDoc.Paths)
            {
                var newKey = key.ToLower();
                if (newKey != key)
                {
                    removeKeys.Add(key);
                    newPaths.Add(newKey, pathItem);
                }
            }

            foreach (var (key, pathItem) in newPaths)
            {
                swaggerDoc.Paths.Add(key, pathItem);
            }

            foreach (var key in removeKeys)
            {
                swaggerDoc.Paths.Remove(key);
            }
        }
    }
}