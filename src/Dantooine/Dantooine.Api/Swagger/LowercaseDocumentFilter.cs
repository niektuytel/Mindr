﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mindr.Api.Swagger;


/// <summary>
/// This class orginally was created by https://gist.github.com/smaglio81 and modified version used in this project you can find here
/// https://gist.github.com/rafalkasa/01d5e3b265e5aa075678e0adfd54e23f
/// </summary>
public class LowercaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths;

        //	generate the new keys
        var newPaths = new Dictionary<string, OpenApiPathItem>();
        var removeKeys = new List<string>();
        foreach (var path in paths)
        {
            var newKey = path.Key.ToLower();
            if (newKey != path.Key)
            {
                removeKeys.Add(path.Key);
                newPaths.Add(newKey, path.Value);
            }
        }

        //	add the new keys
        foreach (var path in newPaths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }

        //	remove the old keys
        foreach (var key in removeKeys)
        {
            swaggerDoc.Paths.Remove(key);
        }
    }
}