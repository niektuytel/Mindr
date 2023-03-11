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
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected static HttpItem HttpItem1 = new()
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
    protected static HttpItem HttpItem2 = new()
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
    protected static Connector Connector1 = new()
    {
        Id = Guid.Parse("c98d9b51-cf20-4938-b7cb-76e8743f673c"),
        Color = "orange",
        Name = "Send Whatsapp Text Message",
        Description = "Some description explain the product",
        Variables = new ConnectorVariable[]
            {
                new()
                {
                    Name = "Authentication Token",
                    Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                    Key = "User-Access-Token",
                    Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                },
                new()
                {
                    Name = "Api version",
                    Description = "Api Version",
                    Key = "Version",
                    Value = "v15.0"
                },
                new()
                {
                    Name = "Sender Phone id",
                    Description = "The phone number id of the sender",
                    Key = "Phone-Number-ID",
                    Value = "113037821608895"
                },
                new()
                {
                    Name = "Receiver Phone number id",
                    Description = "The phone number id of the receiver",
                    Key = "Recipient-Phone-Number",
                    Value = "31618395668"
                }
            },
        Pipeline = new List<HttpItem>() { HttpItem1, HttpItem2 }
    };
    protected static Connector Connector2 = new()
    {
        Id = Guid.Parse("60994748-0cf3-452b-bbbc-44930e8fb052"),
        Color = "blue",
        Name = "Send WhatsApp Sample Text Message",
        Description = "Some description explain the product",
        Variables = new ConnectorVariable[]
            {
                new()
                {
                    Name = "Authentication Token",
                    Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                    Key = "User-Access-Token",
                    Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                },
                new()
                {
                    Name = "Api version",
                    Description = "Api Version",
                    Key = "Version",
                    Value = "v15.0"
                },
                new()
                {
                    Name = "Sender Phone id",
                    Description = "The phone number id of the sender",
                    Key = "Phone-Number-ID",
                    Value = "113037821608895"
                },
                new()
                {
                    Name = "Receiver Phone number id",
                    Description = "The phone number id of the receiver",
                    Key = "Recipient-Phone-Number",
                    Value = "31618395668"
                },
                new()
                {
                    Name = "Sending message",
                    Description = "Message that will been sended",
                    Key = "Text-Body-String",
                    Value = "unser inputed content"
                }
            },
        Pipeline = new List<HttpItem>() { HttpItem1, HttpItem2 }
    };
    protected IEnumerable<Connector> Items = new List<Connector>
    { Connector1, Connector2 };
    protected List<ConnectorHook> ItemHooks { get; set; } = new List<ConnectorHook>()
    {
        new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", Connector1),//Test 1
        new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMQAAAA==", Connector2),//Test 2
        new ConnectorHook(Guid.Parse("2cf632fd-c055-4ecf-abcc-6d9c29e919ec"), "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMQAAAA==", Connector1)//Test 2
    };


}
