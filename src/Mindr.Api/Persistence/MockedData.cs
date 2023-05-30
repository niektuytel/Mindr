using Mindr.Domain.Enums;
using Mindr.Domain.Models.DTO.Connector;

using Mindr.Domain.HttpRunner.Enums;
using Mindr.Domain.HttpRunner.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Mindr.Domain.Models.DTO.Personal;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Mindr.Api.Persistence
{
    public static class MockedData
    {
        public const string UserId = "79c0ff3d-32aa-445b-b9e5-330799cb03c1";// test@test.com
        public const string UserId2 = "410786db-7682-45e5-9099-686c21626d9c";// tuytelniek@gmail.com (3th parties, gmail/google login)
        
        public static PersonalCalendar GetPersonalCalendar()
        {
            return new PersonalCalendar()
            {
                Id = Guid.Parse("129a67ce-8f3a-45ae-929d-460e19b5cd06"),
                UserId = UserId,
                CalendarId = "a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301@group.calendar.google.com",
                CredentialId = Guid.Parse("49fa5022-66dc-45b6-bf27-e512e41a905f"),
                From = CalendarFrom.Google
            };
        }

        public static PersonalCredential GetPersonalCredential() 
        {
            return new PersonalCredential(Guid.Parse("49fa5022-66dc-45b6-bf27-e512e41a905f"), UserId, new PersonalCredentialDTO()
            {
                Target = CredentialTarget.GoogleCalendar,
                AccessToken = "ya29.a0AWY7CklqaLHr3A6x_du7-JrtifzPBVTMAapV6zjTEJgPZWiGcdbAPoGYs9m8h4dXC5tM5eAZPMys2ooPrs-EYUd25wXKPS8uLg3TdSpmKKWLPH0YghddBj60ZxbhUGYGfxMytqFMMJt0f71oa12g4I7m1WAraCgYKATQSARESFQG1tDrpZu-pcnhWAKCbDk_1Gv31CA0163",
                ExpiresIn = 3599,
                RefreshToken = "1//09f8d-mKU1D2RCgYIARAAGAkSNwF-L9IrZ_GsMEG0Z-UsAuFJvLVh7y1bW0jr83HIOOfybWJ6OmL1M74lOpjH1_BQzjAtdzPWCu4",
                Scope = "https://www.googleapis.com/auth/calendar",
                TokenType = "Bearer"
            }); 
        }

        public static Connector GetConnector1()
        {
            return new Connector()
            {
                Id = Guid.Parse("c98d9b51-cf20-4938-b7cb-76e8743f673c"),
                CreatedBy = UserId,
                Color = "orange",
                IsPublic = true,
                Name = "Send Whatsapp Text Message 1",
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
                Pipeline = Domain.HttpRunner.MockedData.GetPipeline1().ToList()
            };
        }
        
        public static Connector GetConnector2()
        {
            return new Connector()
            {
                Id = Guid.Parse("60994748-0cf3-452b-bbbc-44930e8fb052"),
                CreatedBy = UserId,
                Color = "blue",
                IsPublic = true,
                Name = "Send WhatsApp Sample Text Message (2)",
                Description = "Some description explain the product (2)",
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
                Pipeline = Domain.HttpRunner.MockedData.GetPipeline2().ToList()
            };
        }

        public static ConnectorEvent GetConnectorEvent1()
        {
            var connector1 = GetConnector1();
            var event1 = new ConnectorEvent(UserId, "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", connector1);
            var events1 = new List<ConnectorEventVariable>()
            {
                new ConnectorEventVariable()
                {
                    Key = Domain.Enums.EventType.OnDateTime,
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
            var event2 = new ConnectorEvent(UserId, "AQMkADAwATMwMAItNTllZC1hMzFlLTAwAi0wMAoARgAAA2qB3dgu8NBIiZJXcEtOu1YHAK-kNuNXZP9CkLYI4D7saB4AAAIBDQAAAK-kNuNXZP9CkLYI4D7saB4AAAKbMAAAAA==", connector2);
            var events2 = new List<ConnectorEventVariable>()
            {
                new ConnectorEventVariable()
                {
                    Key = Domain.Enums.EventType.OnDateTime,
                    Value = DateTime.Now.AddDays(1).ToLongDateString()
                }
            };
            event2.EventParameters = events2;
            event2.ConnectorColor = connector2.Color;

            return event2;
        }
    }
}
