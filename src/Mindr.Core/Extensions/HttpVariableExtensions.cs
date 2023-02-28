using Mindr.Core.Enums;
using Mindr.Core.Models.HttpCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.Core.Extensions
{
    public static class HttpVariableExtensions
    {
        public const string PrefixOpen = "{{";
        public const string PrefixClose = "}}";

        public static IEnumerable<HttpVariable> GetVariables(this HttpRequest request)
        {
            if (request.Variables != null) return request.Variables;
            var variables = new List<HttpVariable>();

            variables.AddRange(request.Url.Raw.GetVariables(VariablePosition.Uri));
            variables.AddRange(request.Header.GetVariables(VariablePosition.Header));
            variables.AddRange(request.Body.Raw.GetVariables(VariablePosition.Body));

            return variables;
        }

        public static IEnumerable<HttpVariable> GetVariables(this HttpHeader[] lines, VariablePosition location)
        {
            var variables = new List<HttpVariable>();
            foreach (var line in lines)
            {
                variables.AddRange(
                    GetVariables(line.Value, location)
                );
            }

            return variables;
        }

        public static IEnumerable<HttpVariable> GetVariables(this string value, VariablePosition location)
        {
            var variables = new List<HttpVariable>();
            if (string.IsNullOrEmpty(value)) return variables;

            var items = value.Split(PrefixOpen)[1..];
            if (items.Any())
            {
                foreach (var item in items)
                {
                    variables.Add(
                        new HttpVariable()
                        {
                            Location = location,
                            Key = item.Split(PrefixClose)[0],
                            Value = ""
                        }
                    );
                }
            }

            return variables;
        }


        public static HttpRequest SetVariables(this HttpRequest request)
        {
            if (request.Variables == null) return request;

            request.Url.Raw = request.Variables.SetVariables(request.Url.Raw, VariablePosition.Uri);
            request.Header = request.Variables.SetVariables(request.Header, VariablePosition.Header);
            request.Body.Raw = request.Variables.SetVariables(request.Body.Raw, VariablePosition.Body);

            return request;
        }
        
        public static HttpHeader[] SetVariables(this IEnumerable<HttpVariable> variables, HttpHeader[] lines, VariablePosition location)
        {
            var output = lines;
            if (variables != null)
            {
                foreach (var line in output)
                {
                    line.Value = variables.SetVariables(line.Value, location);
                }
            }

            return output;
        }

        public static string SetVariables(this IEnumerable<HttpVariable> variables, string value, VariablePosition location)
        {
            var output = value;
            if (variables != null)
            {
                foreach (var item in variables.Where(item => item.Location == location))
                {
                    output = output.Replace($"{PrefixOpen}{item.Key}{PrefixClose}", item.Value);
                }
            }

            return output;
        }

    }
}
