using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Controllers;

//[Authorize]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
[Route("[controller]")]
public class ConnectorController : ControllerBase
{
    internal static HttpItem DefaultTestSample { get; set; } = new()
    {
        Name = "Send Sample Text Message",
        Description = "Sample text",
        Request = new()
        {
            Variables = new List<HttpVariable>()
            {
                new()
                {
                    Location = VariablePosition.Uri,
                    Key = "Version",
                    Value = "v15.0"
                },
                new()
                {
                    Location = VariablePosition.Uri,
                    Key = "Phone-Number-ID",
                    Value = "113037821608895"
                },
                new()
                {
                    Location = VariablePosition.Header,
                    Key = "User-Access-Token",
                    Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                },
                new()
                {
                    Location = VariablePosition.Body,
                    Key = "Recipient-Phone-Number",
                    Value = "31618395668"
                }
            },

            Method = "POST",
            Url = new()
            {
                Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                Protocol = "https",
                Host = new string[]
                    {
                            "graph",
                            "facebook",
                            "com"
                    },
                Path = new string[]
                    {
                        "{{Version}}",
                        "{{Phone-Number-ID}}",
                        "messages"
                    }
            },
            Header = new HttpHeader[]
            {
                new()
                {
                    Key = "Authorization",
                    Value = "Bearer {{User-Access-Token}}",
                    Type = "text"
                },
                new()
                {
                    Key = "Content-Type",
                    Value = "application/json",
                    Type = "text"
                }
            },
            Body = new()
            {
                Mode = "raw",
                Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                Options = new()
                {
                    Raw = new()
                    {
                        Language = "json"
                    }
                }
            }
        }
    };

    internal static HttpItem DefaultTestSample2 { get; set; } = new()
    {
        Name = "Send Sample Text Message",
        Description = "Sample text 2",
        Request = new()
        {
            Variables = new List<HttpVariable>()
            {
                new()
                {
                    Location = VariablePosition.Uri,
                    Key = "Version",
                    Value = "v15.0"
                },
                new()
                {
                    Location = VariablePosition.Uri,
                    Key = "Phone-Number-ID",
                    Value = "113037821608895"
                },
                new()
                {
                    Location = VariablePosition.Header,
                    Key = "User-Access-Token",
                    Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                },
                new()
                {
                    Location = VariablePosition.Body,
                    Key = "Recipient-Phone-Number",
                    Value = "31618395668"
                }
            },

            Method = "POST",
            Url = new()
            {
                Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                Protocol = "https",
                Host = new string[]
                    {
                            "graph",
                            "facebook",
                            "com"
                    },
                Path = new string[]
                    {
                        "{{Version}}",
                        "{{Phone-Number-ID}}",
                        "messages"
                    }
            },
            Header = new HttpHeader[]
            {
                new()
                {
                    Key = "Authorization",
                    Value = "Bearer {{User-Access-Token}}",
                    Type = "text"
                },
                new()
                {
                    Key = "Content-Type",
                    Value = "application/json",
                    Type = "text"
                }
            },
            Body = new()
            {
                Mode = "raw",
                Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                Options = new()
                {
                    Raw = new()
                    {
                        Language = "json"
                    }
                }
            }
        }
    };

    [HttpGet("all")]
    public IEnumerable<Connector> GetAll()
    {
        var colors = new List<string>() { "gray", "red", "green", "blue", "orange" };
        var inputs = new List<ConnectorVariable>()
        {
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            }
        }.ToArray();
        return Enumerable.Range(1, 5).Select(index => new Connector
        {
            Id = Guid.NewGuid(),
            Name = $"Connector: {index}",
            Description = $"Description explain the connector on index: {index}",
            Color = colors[index % 4],
            Variables = inputs
        });
    }

    [HttpGet("{id}")]
    public Connector GetById(string id)
    {
        var inputs = new List<ConnectorVariable>()
        {
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            }
        }.ToArray();
        return new Connector
        {
            Id = Guid.NewGuid(),
            Name = $"Test connector",
            Description = $"Description explain the connector",
            Color = "orange",
            Variables = inputs,
            Pipeline = new List<HttpItem> { DefaultTestSample, DefaultTestSample2 }
        };
    }

    [HttpPost("{id}")]
    public Task<Guid?> CreateById(string id, Connector payload)
    {
        var inputs = new List<ConnectorVariable>()
        {
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            }
        }.ToArray();
        return new Connector
        {
            Id = Guid.NewGuid(),
            Name = $"Test connector",
            Description = $"Description explain the connector",
            Color = "orange",
            Variables = inputs,
            Pipeline = new List<HttpItem> { DefaultTestSample, DefaultTestSample2 }
        };
    }

    [HttpPut("{id}")]
    public Task<Guid?> UpdateById(string id, Connector payload)
    {
        var inputs = new List<ConnectorVariable>()
        {
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            }
        }.ToArray();
        return new Connector
        {
            Id = Guid.NewGuid(),
            Name = $"Test connector",
            Description = $"Description explain the connector",
            Color = "orange",
            Variables = inputs,
            Pipeline = new List<HttpItem> { DefaultTestSample, DefaultTestSample2 }
        };
    }

    [HttpDelete("{id}")]
    public Task<Guid?> DeleteById(string id, Connector payload)
    {
        var inputs = new List<ConnectorVariable>()
        {
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            },
            new ConnectorVariable()
            {
                Key = $"Key 1",
                Value = $"Value 1"
            }
        }.ToArray();
        return new Connector
        {
            Id = Guid.NewGuid(),
            Name = $"Test connector",
            Description = $"Description explain the connector",
            Color = "orange",
            Variables = inputs,
            Pipeline = new List<HttpItem> { DefaultTestSample, DefaultTestSample2 }
        };
    }

}
