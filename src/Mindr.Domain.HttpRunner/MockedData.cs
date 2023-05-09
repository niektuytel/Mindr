using Mindr.Domain.HttpRunner.Enums;
using Mindr.Domain.HttpRunner.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Domain.HttpRunner
{
    public static class MockedData
    {
        public static HttpItem GetHttpItem1()
        {
            return new HttpItem()
            {
                Name = "ProcessConnectorEventAsync Sample Text Message",
                Description = "Sample text",
                Request = new HttpRequest()
                {
                    Variables = new List<HttpVariable>()
                    {
                        new HttpVariable()
                        {
                            Location = VariablePosition.Uri,
                            Key = "Version",
                            Value = "v15.0"
                        },
                        new HttpVariable()
                        {
                            Location = VariablePosition.Uri,
                            Key = "Phone-Number-ID",
                            Value = "113037821608895"
                        },
                        new HttpVariable()
                        {
                            Location = VariablePosition.Header,
                            Key = "User-Access-Token",
                            Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                        },
                        new HttpVariable()
                        {
                            Location = VariablePosition.Body,
                            Key = "Recipient-Phone-Number",
                            Value = "31618395668"
                        }
                    },

                    Method = "POST",
                    Url = new HttpRequestUrl()
                    {
                        Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                        Protocol = "https",
                        Hosts = new string[]
                            {
                                    "graph",
                                    "facebook",
                                    "com"
                            },
                        Paths = new string[]
                            {
                                "{{Version}}",
                                "{{Phone-Number-ID}}",
                                "messages"
                            }
                    },
                    Header = new HttpHeader[]
                    {
                        new HttpHeader()
                        {
                            Key = "Authorization",
                            Value = "Bearer {{User-Access-Token}}",
                            Type = "text"
                        },
                        new HttpHeader()
                        {
                            Key = "Content-Key",
                            Value = "application/json",
                            Type = "text"
                        }
                    },
                    Body = new HttpBody()
                    {
                        Mode = "raw",
                        Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                        Options = new HttpBodyOption()
                        {
                            Raw = new HttpBodyOptionRaw()
                            {
                                Language = "json"
                            }
                        }
                    }
                }
            };
        }

        public static HttpItem GetHttpItem2()
        {
            return new HttpItem()
            {
                Name = "ProcessConnectorEventAsync Sample Text Message",
                Description = "Sample text 2",
                Request = new HttpRequest()
                {
                    Variables = new List<HttpVariable>()
                        {
                            new HttpVariable()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new HttpVariable()
                            {
                                Location = VariablePosition.Uri,
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new HttpVariable()
                            {
                                Location = VariablePosition.Header,
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBAExyTHn1XOJN9SCkZAyLkkwvfgAF34gDtIIIgF5VEn4iihUsHSSgbICtzLGhZBMfpwOZA1f0KzZA7DcmKWIW1nnsyOoWJgFknicQI0OvfrbW4c31rABm9RKR8Bq3EckyUYROWeX1iSipaPEJdlC6LHH5I9ILHMzC4ZAcUaZBshmtZBNyHQO8yRZBZCSToD0GG4wPZC7TDt46he"
                            },
                            new HttpVariable()
                            {
                                Location = VariablePosition.Body,
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },

                    Method = "POST",
                    Url = new HttpRequestUrl()
                    {
                        Raw = "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
                        Protocol = "https",
                        Hosts = new string[]
                            {
                                        "graph",
                                        "facebook",
                                        "com"
                            },
                        Paths = new string[]
                            {
                                    "{{Version}}",
                                    "{{Phone-Number-ID}}",
                                    "messages"
                            }
                    },
                    Header = new HttpHeader[]
                    {
                            new HttpHeader()
                            {
                                Key = "Authorization",
                                Value = "Bearer {{User-Access-Token}}",
                                Type = "text"
                            },
                            new HttpHeader()
                            {
                                Key = "Content-Key",
                                Value = "application/json",
                                Type = "text"
                            }
                    },
                    Body = new HttpBody()
                    {
                        Mode = "raw",
                        Raw = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
                        Options = new HttpBodyOption()
                        {
                            Raw = new HttpBodyOptionRaw()
                            {
                                Language = "json"
                            }
                        }
                    }
                }
            };
        }

        public static IEnumerable<HttpItem> GetPipeline()
        {
            return new List<HttpItem>() { GetHttpItem1(), GetHttpItem2() };
        }
    }
}
