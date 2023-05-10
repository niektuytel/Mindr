using Mindr.Domain.Enums;
using Mindr.Domain.Models.DTO.Connector;

using Mindr.Domain.HttpRunner.Enums;
using Mindr.Domain.HttpRunner.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mindr.Api.Persistence
{
    public static class MockedData
    {
        public static Connector GetConnector1()
        {
            return new Connector()
            {
                Id = Guid.Parse("c98d9b51-cf20-4938-b7cb-76e8743f673c"),
                CreatedBy = "79c0ff3d-32aa-445b-b9e5-330799cb03c1",
                Color = "orange",
                IsPublic = true,
                Name = "Send Whatsapp Text Message",
                Description = "Some description explain the product",
                Variables = new ConnectorVariable[]
                        {
                            new()
                            {
                                IsPublic = false,
                                Name = "Authentication Token",
                                Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                                Key = "User-Access-Token",
                                Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                            },
                            new()
                            {
                                IsPublic = false,
                                Name = "Api version",
                                Description = "Api Version",
                                Key = "Version",
                                Value = "v15.0"
                            },
                            new()
                            {
                                IsPublic = true,
                                Name = "Sender Phone id",
                                Description = "The phone number id of the sender",
                                Key = "Phone-Number-ID",
                                Value = "113037821608895"
                            },
                            new()
                            {
                                IsPublic = true,
                                Name = "Receiver Phone number id",
                                Description = "The phone number id of the receiver",
                                Key = "Recipient-Phone-Number",
                                Value = "31618395668"
                            }
                        },
                Pipeline = Domain.HttpRunner.MockedData.GetPipeline()
            };
        }
        public static Connector GetConnector2()
        {
            return new Connector()
            {
                Id = Guid.Parse("60994748-0cf3-452b-bbbc-44930e8fb052"),
                CreatedBy = "79c0ff3d-32aa-445b-b9e5-330799cb03c1",
                Color = "blue",
                IsPublic = true,
                Name = "Send WhatsApp Sample Text Message",
                Description = "Some description explain the product",
                Variables = new ConnectorVariable[]
                        {
                        new()
                        {
                            IsPublic = false,
                            Name = "Authentication Token",
                            Description = "Token needed to login with see: https://developers.facebook.com/apps/824837035437555/whatsapp-business/wa-dev-console/?business_id=656542846083352",
                            Key = "User-Access-Token",
                            Value = "EAALuL1ZAZBrfMBABkpP9ztBGqgTZCG7FWEljDQFUhOPgyV2O6lwhIPsWVWjBZB9IRK0cfFQNxkQ2eL6eAWKIGNUYAx1ygElRU8ffzksmYGEggb0ZBy0Jiq1JKhqWzZCD3mYONhv4S0HZAwwaZBhOmohl1UaauBl4u1iRyUP00gMEMXIThX6qoWAgpQmNDGj62WPFEFx4IS6GAvTmAyQF0wdx"
                        },
                        new()
                        {
                            IsPublic = false,
                            Name = "Api version",
                            Description = "Api Version",
                            Key = "Version",
                            Value = "v15.0"
                        },
                        new()
                        {
                            IsPublic = false,
                            Name = "Sender Phone id",
                            Description = "The phone number id of the sender",
                            Key = "Phone-Number-ID",
                            Value = "113037821608895"
                        },
                        new()
                        {
                            IsPublic = true,
                            Name = "Receiver Phone number id",
                            Description = "The phone number id of the receiver",
                            Key = "Recipient-Phone-Number",
                            Value = "31618395668"
                        },
                        new()
                        {
                            IsPublic = true,
                            Name = "Sending message",
                            Description = "Message that will been sended",
                            Key = "Text-Body-String",
                            Value = "unser inputed content"
                        }
                    },
                Pipeline = Domain.HttpRunner.MockedData.GetPipeline()
            };
        }

        public static ConnectorEvent GetConnectorEvent1()
        {
            var connector1 = GetConnector1();
            var event1 = new ConnectorEvent("79c0ff3d-32aa-445b-b9e5-330799cb03c1", "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", connector1);
            var events1 = new List<ConnectorEventVariable>()
            {
                new ConnectorEventVariable()
                {
                    Key = EventType.OnDateTime,
                    Value = DateTime.Now.AddDays(1).ToLongDateString()
                }
            };
            event1.EventParameters = events1;
            event1.ConnectorColor = connector1.Color;

            return event1;
        }

        public static ConnectorEvent GetConnectorEvent2()
        {
            var connector2 = GetConnector2();
            var event2 = new ConnectorEvent("79c0ff3d-32aa-445b-b9e5-330799cb03c1", "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", connector2);
            var events2 = new List<ConnectorEventVariable>()
            {
                new ConnectorEventVariable()
                {
                    Key = EventType.OnDateTime,
                    Value = DateTime.Now.AddDays(1).ToLongDateString()
                }
            };
            event2.EventParameters = events2;
            event2.ConnectorColor = connector2.Color;

            return event2;
        }
    }
}
