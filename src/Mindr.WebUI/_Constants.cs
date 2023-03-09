﻿using Mindr.WebUI.Models;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.WebUI;

internal static class _Constants
{
    public static string Logo = "https://www.lucrasoft.nl//media/xrrbm1vl/lucrasoft-ict-groep-white.png";
    public static List<Page> Pages = new()
    {
        new Page(){
            Name = "Home",
            Icon = @FluentIcons.Home,
            Href = "/",
        },
        new Page(){
            Name = "Agenda",
            Icon = @FluentIcons.CalendarEmpty,
            Href = "/agenda",
        },
        new Page(){
            Name = "Connector",
            Icon = @FluentIcons.CalendarAgenda,
            Href = "/connector",
        },
        new Page(){
            Name = "Connectors",
            Icon = @FluentIcons.CalendarAgenda,
            Href = "/connectors",
        },
        new Page(){
            Name = "ConnectorHooks",
            Icon = @FluentIcons.CalendarAgenda,
            Href = "/connectorhooks",
        }
        //
        //new Page(){
        //    Name = "Collections",
        //    Icon = @FluentIcons.Database,
        //    Href = "/collections",
        //    UseBreadcrumb = false,
        //    Disabled = true
        //}
    };


    public static List<Page> ConnectorPage = new()
    {
        new Page(){
            Name = "Overview",
            Icon = @FluentIcons.Info,
            Href = "/connector",
        },
        new Page(){
            Name = "Pipeline",
            Icon = @FluentIcons.PipelinePlay,
            Href = "/connector/pipeline",
        }
    };

    internal static List<Option<string>> Years = new()
    {
        { new Option<string> { Value = $"{DateTime.Now.Year-1}", Text = $"{DateTime.Now.Year-1}" } },
        { new Option<string> { Value = $"{DateTime.Now.Year}",   Text = $"{DateTime.Now.Year}", Selected = true } },
        { new Option<string> { Value = $"{DateTime.Now.Year+1}", Text = $"{DateTime.Now.Year+1}" } }

    };

    internal static List<Option<string>> Months = new()
    {
        { new Option<string> { Value = "1", Text = "January" } },
        { new Option<string> { Value = "2", Text = "February" } },
        { new Option<string> { Value = "3", Text = "March" } },
        { new Option<string> { Value = "4", Text = "April" } },
        { new Option<string> { Value = "5", Text = "May" } },
        { new Option<string> { Value = "6", Text = "June" } },
        { new Option<string> { Value = "7", Text = "July" } },
        { new Option<string> { Value = "8", Text = "August" } },
        { new Option<string> { Value = "9", Text = "September" } },
        { new Option<string> { Value = "10", Text = "October" } },
        { new Option<string> { Value = "11", Text = "November" } },
        { new Option<string> { Value = "12", Text = "December" } }
    };

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


    internal static string Json = """"
                {
        	"info": {
        		"_postman_id": "33f74b91-fc9c-4840-82f2-189c53c21228",
        		"name": "WhatsApp Cloud API",
        		"description": "Welcome to the WhatsApp Business Platform Cloud API from Meta.\n\nIndividual developers and Business Service Providers (BSPs) can now send and receive messages using a cloud-hosted version of the WhatsApp Business Platform API. Compared to the previous solutions, Cloud API is simpler to use and is a more cost-effective way for businesses to use WhatsApp.\n\nWhether you’re a business or a BSP, Cloud API provides great benefits when compared to WhatsApp On-Premises API.\n\nSo let's get started!\n\n# Installation\n\n## Quick Installation\n\nUse the `Run in Postman` button below or at the top right corner of the documentation to import this collection into your local Postman app.\n\n| | |\n|---|---|\n| [![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/13382743-84d01ff8-4253-4720-b454-af661f36acc2?action=collection%2Ffork&collection-url=entityId%3D13382743-84d01ff8-4253-4720-b454-af661f36acc2%26entityType%3Dcollection%26workspaceId%3Da31742be-ce5c-4b9d-a828-e10ee7f7a5a3)| Click to fork WhatsApp Cloud API into your workspace. |\n\n# Environment\n\nThis collection includes pre-configured environments. To use the pre-configured environment for Cloud API, select **Cloud API** from the environment drop down list box at the top right corner of Postman. This automatically populates the correct environment variables listed in the table below.\n\nTo run requests in the [Get Started](#01e24b28-7896-4aee-946c-210b6945e50b) guide using a custom environment, you need to set the listed variables in the following table to values in your custom environment:\n\n| Name | Description |\n| --- | --- |\n| `Version` | Latest [Graph API version](https://developers.facebook.com/docs/graph-api/). For example: **`v15.0`** |\n| `User-Access-Token` | Your user access token after signing up at [developers.facebook.com](https://developers.facebook.com). |\n| `WABA-ID` | Your WhatsApp Business Account (WABA) ID. |\n| `Phone-Number-ID` | ID for the phone number connected to the WhatsApp Business API. You can get this with a [Get Phone Number ID request](3184f675-d289-46f1-88e5-e2b11549c418). |\n| `Business-ID` | Your Business' ID. Once you have your Phone-Number-ID, make a [Get Business Profile request](#99fd3743-46cf-46c4-95b5-431c6a4eb0b0) to get your Business' ID. |\n| `Recipient-Phone-Number` | Phone number that you want to send a WhatsApp message to. |\n| `Media-ID` | ID for the media to [send a media message](#0a632754-3788-43bf-b785-ac6a73423d5a) or [media template message](#439c926a-8a6c-4972-ab2c-d99297716da9) to your customers. |\n| `Media-URL` | URL for the media to [download media content](#cbe5ece3-246c-48f3-b338-074187dfef66). |\n| `Upload-ID` | Session ID for uploading data (images) to Meta using [Resumable Upload API](https://documenter.getpostman.com/view/13382743/UVC5FTHT?fbclid=IwAR083mCseNzJm-JgxlIQbdF30hkAbEOHkbBaw9bA7-isGKU6uwtq1RJKc0o#ec2c5110-134c-4f2d-ba13-4d33ad13d1df). |\n\n# Changelog\n\nFor information relating to what has changed in the WhatsApp Business Platform, see [WhatsApp Business Platform Changelog](https://developers.facebook.com/docs/whatsapp/business-platform/changelog#october-6--2022).\n\n# What's New for Cloud API\n\nThis section covers new features and updates for WhatsApp Business Platform Cloud API Postman collection. \n\n## December 13th 2022\n\nThe following features are now available on the Cloud API, hosted by Meta, with no upgrades necessary: \n\n**Improved Media Link Caching**\nFor Media message sends using links, the Cloud API now supports HTTP Caching Protocol. This allows businesses to set their preferred caching options and communicate them with the Cloud API by setting relevant HTTP Headers. To learn more about media link caching, see [Media HTTP Caching](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/send-messages#media-http-caching).  \n\n**Reduction for Webhook Retry Policy** \nCloud API calls the businesses Webhook to notify the business of message deliveries, reads, and replies. If the Webhook is down for any reason Cloud API previously retried notifying the Webhook for up to 30 days. Going forward we have reduced this number to 7 days. For more information, see our [developer documentation](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/set-up-webhooks#webhook-delivery-failure). \n\n**New media endpoint that checks ownership permissions**\nIntroducing a new optional media endpoint parameter for verifying media ownership on a phone number level, which allows for differentiation for types of media owned by a specific phone number. For more information, see **API Reference**>>**Media**:\n\n* **Retrieve Media URL**\n* **Delete Media**\n\n## November 3rd 2022\n\n**Messages Per Second**\n\nCloud API now supports up to 500 (up from 350) messages per second (MPS) of combined text and media messages, by request. For more information, see [Throughput](https://developers.facebook.com/docs/whatsapp/cloud-api/overview#throughput) for details.\n\nIf you already have 350 MPS it will be increased to 500 MPS automatically. If you already requested 350 MPS but the process has not been completed, you will receive 500 MPS upon completion.\n\n**Document Captions**\n\nCloud API now supports captions on documents sent to and received from customers. For more information, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n**Error Codes**\nWe have updated our error codes documentation to be more actionable. There have been some changes to existing error codes in Graph API **`v15.0`** and above. Some error titles also have been updated. For more information, see [Error Codes](https://developers.facebook.com/docs/whatsapp/cloud-api/support/error-codes).\n\n## October 6th 2022\n\n**Graph API v15 update**\n\nWhatsApp Cloud API has been updated for Graph API version 15.0. The `Version` environment variable for environment **Cloud API [Cloud API]** has been automatically updated to **`v15.0`**.\n\n**Embedded Signup**\n\nEmbedded Signup now supports mobile web browsers. The user interface will automatically optimize for a mobile experience when it detects that the viewer is using a mobile web browser.\n\n**Callback Override**\n\nYou can now use different callback URLs for each of your WhatsApp Business Accounts without having to create a unique app for each WhatsApp Business Account. See [Overriding the Callback URL](https://developers.facebook.com/docs/whatsapp/embedded-signup/webhooks#overriding-the-callback-url) for details.\n\n**Message Templates**\n\nText parameters (**`messages.parameters.text`**) for message templates that only use a body component (**`messages.type:body`**) can now total up to 32,768 characters. For more information, see [Parameters object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#parameter-object).\n\n**Messages Per Second**\n\nCloud API now supports up to 350 (up from 250) messages per second (MPS) of combined text and media messages, by request. For more information, see [Throughput](https://developers.facebook.com/docs/whatsapp/cloud-api/overview#throughput) for details.\n\nIf you already have 250 MPS it will be increased to 350 MPS automatically. If you already requested 250 MPS but the process has not been completed, you will receive 350 MPS upon completion.\n\n## September 22nd 2022 - Reaction Messages and Business Profile\n\nThe September 22nd 2022 release contains the following features:\n\n**Reactions**\n\nYou can now send and receive reactions on messages. Additionally, we have also added Webhook support for reaction messages.\n\nThe new API request has been added to **API Reference**>>**Messages**>>**Send Reply with Reaction Message**.\n\n**Business Profile**\n\nThe **`about`** field on business profiles is now supported. For more information, see [Business Profiles](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/business-profiles).\n\n## August 25th 2022 - Products and Services and Animated Sticker support\n\nThe August 25th 2022 release contains the following features:\n\n**Products and Services**\n\nTwo new Product and Services API requests have been added to **API Reference**>>**Messages**:\n\n* **Send Single Product Message**\n* **Send Multi-Product Message**\n\nAdditionally, there are two new Product related Webhooks:\n\n* **Received Product Enquiry Message**\n* **Received Order Messages**\n\nFor overview information, see [Sell Product and Services](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/sell-products-and-services).\n\n**Animated Stickers**\n\nYou can now include animated stickers in outbound, business-initiated messages and receive Webhooks describing those messages the same way you would if you were sending a non-animated sticker. Refer to the **API Reference**>>**Messages**>>**Message Object** sticker property and for sticker asset requirements.\n\n#### August 17th 2022 - Resumable Upload API requests for updating your business profile picture\n\nWhatsApp Cloud API now has three new requests in **API Reference**>>**Business Profiles**:\n\n* **Resumable Upload - Create an Upload Session**\n* **Resumable Upload - Upload File Data**\n* **Resumable Upload - Query File Upload Status**\n\nThese requests simplify uploading profile pictures to Meta. You can now get a **`profile_picture_handle`** that you can use to update pictures.\n\n## August 2nd 2022 - Graph API v14 update\nWhatsApp Cloud API has been updated for Graph API version 14.0. The `Version` environment variable for environment **Cloud API [Cloud API]** has been automatically updated to **`v14.0`**.\n\n\n## July 21st 2022\nThe July 2022 release contains the following features:\n\n* Businesses can now reply to any message in a conversation thread. Replies will include a contextual bubble referencing the replied-to message. Refer to the [Send Messages](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/send-messages#replies) guide to learn how to reply to a message. \nFor more information on the new Postman Reply-To Requests, see [API **Reference**>>**Messages**](https://documenter.getpostman.com/view/13382743/UVC5FTHT#1f4f7644-cc97-40b5-b8e4-c19da268fff1). For our WhatsApp Cloud API developer docs, see [**Reference**>>**Messages**](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages).\n\n* WhatsApp Cloud API now supports up to 250 messages per second (MPS) of combined sending and receiving (inclusive of text and media messages). If you are an enterprise partner you can open a Direct Support ticket to request 250 MPS throughput by selecting Question Topic: \"**Cloud API Issues**\", Request Type: \"**Request to migrate to 250 MPS throughput**\" and fill out the required information.\n\n## May 2022\nExisting direct partners and Business Service Providers (BSPs) can now send and receive messages using a cloud-hosted version of the WhatsApp Business API. Compared to On-Premises API, the Cloud-based API is simpler to use and is a more cost-effective way for businesses to use WhatsApp.",
        		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
        		"_exporter_id": "17570659"
        	},
        	"item": [
        		{
        			"name": "Overview",
        			"item": [
        				{
        					"name": "On-Premises API vs. Cloud API",
        					"item": [],
        					"description": "Businesses looking to use the WhatsApp Business Platform have two hosting options: On-Premises API and Cloud API. In general, we recommend that the majority of businesses use the Cloud API due to ease of implementation and maintenance. For a full performance comparison, see:\n\n*   [Key Differences](https://developers.facebook.com/docs/whatsapp/cloud-api/overview/on-premises-cloud-api-comparison#key-differences)\n*   [Messaging Features Comparison](https://developers.facebook.com/docs/whatsapp/cloud-api/overview/on-premises-cloud-api-comparison#messaging-features)\n    \n\n<!-- Saved by grahamp 09/02/2021: Back up of Key differences in the case we want to keep Postman complete and not link out to developers.facebook.com. -->\n\n<!-- \n<br>| <b>Feature</b> | <b>On-Premises API (v2.37)</b> | <b>Cloud API/Beta</b> |<br>|--------------------|-----------------------------|--------------------|<br>| Check Contact | ✅ | 🆇 (For Cloud API, this is no longer required to send messages. You can just use the person’s phone number.) |<br>| Text Messages | ✅ | ✅ |<br>| Media Messages | ✅ | ✅ &nbsp; Images and documents are supported in October 2021. Audio and video will be supported starting November 2021.|<br>| Media Provider | ✅ | 🆇 |<br>| Text Message Templates | ✅ | ✅ |<br>| Media Message Templates | ✅ | ✅ |<br>| Interactive Message Templates | ✅ | ✅ |<br>| Contacts send/receive (not in template) | ✅ | ✅ |<br>| Location send/receive (not in template) | ✅ | ✅ |<br>| Stickers | ✅ | 🆇 |<br>| Sticker packs | ✅ | 🆇 |<br>| Conversation-Based Pricing - global rollout | ✅ &nbsp;(Feb, 2022) | ✅ &nbsp;(Feb, 2022) |<br>| List Messages | ✅ &nbsp;(v.2.35) | 🆇 |<br>| Dynamic Reply Buttons | ✅ &nbsp;(v.2.35) | 🆇 |<br>| Product List Messages | ✅ &nbsp;(v.2.37) | 🆇 |<br>| Ads with Content | ✅ | 🆇 | \n-->",
        					"auth": {
        						"type": "noauth"
        					},
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "Cloud API Overview",
        					"item": [],
        					"description": "To use the Cloud API, businesses make calls to Meta’s [Graph API](https://developers.facebook.com/docs/graph-api/) to send messages and Webhooks to receive events, such as messages and status updates. The Graph API is a form of Remote Procedure Call expressed over HTTP, where operations and their parameters are expressed using a combination of URL parameters, headers, and request body.\n\nA call to the Graph API from UNIX-based command lines looks like this:\n\n``` shell\ncurl -X POST \\\nhttps://graph.facebook.com/{{Version}}/{{Recipient-Phone-Number}}/messages \\\n  -H \"Authorization: {{User-Access-Token}}\" \\\n  -d '{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"1650XXXXXXX\",\n    \"text\": {\"body\" : \"hi\"}\n }'\n\n```\n\nCompared to the On-Premises implementation, a Graph API integration relies on a different authentication mechanism (see [User Access Token](#3dd2844f-10f4-4e52-8c59-b6f4f4f92b4b)), a different Webhook setup process (see [Setup Webhooks](#41946d3c-6460-4bc2-963f-d5732e48e84e)), and has different latencies and error rates. For more information on how to use the Graph API, see the [Graph API Developer Documentation](https://developers.facebook.com/docs/graph-api/).",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Message Throughput",
        					"item": [],
        					"description": "The Cloud API supports up to 80 messages per second (mps) combined sending and receiving of text and media messages by default, and up to 500 mps by request.\n\nFor more information, see [Throughput](https://developers.facebook.com/docs/whatsapp/cloud-api/overview#throughput)."
        				},
        				{
        					"name": "Rate Limits",
        					"item": [],
        					"description": "Cloud API follows [Business Use Case Rate Limits](https://developers.facebook.com/docs/graph-api/overview/rate-limiting/#buc-rate-limits). Each WhatsApp Business Account (WABA) has a call count rate limit and each call made by your app counts toward the limit. An app’s call count for a WABA is the number of calls it can make to business accounts under this WABA during a rolling one hour window and is calculated as follows:\n\n```Calls within one hour for a WABA = 1800000 * Number of Registered Numbers under this WABA```\n\nYou will receive an **`80007`** error code when you hit call limits.\n\nBesides platform rate limits, we have the following rate limits applicable to Cloud API accounts:\n\n* [Test message rate limit](https://www.facebook.com/business/help/2640149499569241): apply to unverified WhatsApp business accounts.\n* [Quality rating and messaging limits](https://developers.facebook.com/docs/whatsapp/api/rate-limits#quality-rating-and-messaging-limits): apply to verified WhatsApp business accounts \n* [Capacity rate limit](https://developers.facebook.com/docs/whatsapp/api/rate-limits#capacity): apply to all accounts.\n* [Business phone rate limit](https://developers.facebook.com/docs/whatsapp/messaging-limits#capacity): Applies to all accounts and limits the throughput per business phone number.",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Available Metrics",
        					"item": [],
        					"description": "Cloud API users can see the number of messages sent and delivered, as well as other metrics. Fore more information, see [Get Account Metrics](https://developers.facebook.com/docs/whatsapp/business-management-api/analytics)."
        				},
        				{
        					"name": "Scaling",
        					"item": [],
        					"description": "Within Meta infrastructure, the Cloud API automatically scales and adjusts to handle your workload, within your rate limit (messaging volume and number of WhatsApp business accounts).",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Data Privacy & Security",
        					"item": [
        						{
        							"name": "Message Flows",
        							"item": [],
        							"description": "When a user sends a message to one of these businesses, the message travels end-to-end encrypted between the user and the Cloud API. As per the Signal protocol, the user and the Cloud API, on behalf of the business, negotiate encryption keys and establish a secure communication channel. WhatsApp cannot access any message content exchanged between users and businesses.\n\nOnce a message is received by the Cloud API, it gets decrypted and forwarded to the Business. Messages are only temporarily stored by the Cloud API as required to provide the base API functionality.\n\nMessages from a business to a user flow on the reverse path. Businesses send messages to Cloud API. The Cloud API service stores the messages temporarily and takes on the task to send the message to the WhatsApp platform. Messages are stored for any necessary retransmissions.\n\nAll messages are encrypted by the Cloud API before being sent to WhatsApp using the Signal protocol with keys negotiated with the user (recipient).\n\nWhatsApp acts as the transport service. It provides the message forwarding software; both client and server. It has no visibility into the messages being sent. It protects the users data by detecting unusual messaging patterns (like a business trying to message all users) or collecting spam reports from users.\n\nCloud API, operated by Meta, acts as the intermediary between WhatsApp and the Cloud API businesses. In other words, those businesses have given Cloud API the power to operate on their behalf. Because of this, WhatsApp forwards all message traffic destined for those businesses to Cloud API. WhatsApp also expects to receive from Cloud API all message traffic from those businesses. This is the same client behavior that the On-Premise client has.\n\nWhatsApp gives Cloud API metering and billing information for the Cloud API businesses. It does not share any other messaging information.\n\nMeta, in providing the WhatsApp Cloud API service, acts as a Data Processor on behalf of the business. In other words, the businesses have requested Meta to provide programmatic access to the WhatsApp platform.\n\nCloud API receives from WhatsApp the messages destined for the businesses that use Cloud API. Cloud API also sends to WhatsApp the messages sent by those businesses.  \nOther parts of Meta (other than Cloud API) do not have access to the Cloud API business communications, including message content and metadata. Meta does not use any Cloud API data for advertising."
        						},
        						{
        							"name": "Stored and Collected Data",
        							"item": [],
        							"description": "All data collected, stored and accessed by Cloud API is controlled and monitored to ensure proper usage and maintain the high level of privacy expected from a WhatsApp client.\n\nInformation about the businesses, including their phone numbers, business address, contacts, type, etc. is maintained by Meta and the Business Manager product and complies with the terms of service set by Meta. Cloud API relies on Business Manager and other Meta systems to identify any access to Cloud API on behalf of the business.\n\nMessages sent or received through Cloud API are only accessed by Cloud API, no other part of Meta can use this information. Messages have a maximum retention period of 30 days in order to provide the base features and functionality of the Cloud API service; for example, retransmissions. After 30 days, these features and functionality are no longer available.\n\nCloud API does not rely on any information about the user (customer/consumer) the business is communicating with other than the phone number used to identify the account. This information is used to deliver the messages through the WhatsApp client code. User phone numbers are used as sources or destinations for individual messages; as such they are deleted when messages are deleted. No other part of Meta has access to this information.\n\nLike the On-Premise client, the WhatsApp client code used by Cloud API collects messaging information about the business as required by WhatsApp. This is information used by WhatsApp to detect malicious activity. No message content is shared or sent to WhatsApp at any time and no WhatsApp employee has access to any message content.\n\n| Cloud API Data | System | Available to rest of Meta | Available to WhatsApp |\n| --- | --- | --- | --- |\n| Message content | Cloud API | No | No |\n| Consumer phone number | Cloud API | No | Yes |\n| Non-identifiable statistics | Cloud API | Yes | Yes |\n| Integrity signals - per business | WhatsApp Client | No | Yes |\n| Business Information | Business Manager | Yes | Yes |\n| Billing - per business | WhatsApp | Yes | Yes |"
        						},
        						{
        							"name": "Encryption",
        							"item": [],
        							"description": "With the Cloud API, every WhatsApp message continues to be protected by Signal protocol encryption that secures messages before they leave the device. This means messages with a WhatsApp business account are securely delivered to the destination chosen by each business.  \n<!--\nWhatsApp determines the end-to-end encryption status based on the business’s choice of who operates its API endpoint:\n\n*   Messages with businesses that choose a third-party Business Solution Provider to operate the WhatsApp Business API on their behalf are not considered end-to-end encrypted as these businesses have chosen to give an intermediary access to these messages. This applies to both the Cloud API and the on-premise API when working with a BSP.\n*   Messages with businesses that use the WhatsApp Business app or directly integrate the Business API on-premise are considered end-to-end encrypted as these messages are delivered directly to the business.\n-->    \n\nFor additional details, see [Encryption Overview Whitepaper](https://scontent.whatsapp.net/v/t39.8562-34/122249142_469857720642275_2152527586907531259_n.pdf/WA_Security_WhitePaper.pdf?ccb=1-5&_nc_sid=2fbf2a&_nc_ohc=-gqGulAzS8kAX-qgWRK&_nc_ht=scontent.whatsapp.net&oh=040d462095ff61f2f89f06a3f4ff1e90&oe=6131E1D9).\n\nThe Cloud API uses industry standard encryption techniques to protect data in transit and at rest. The API uses Graph API for sending messages and Webhooks for receiving events, and both operate over industry standard HTTPS, protected by TLS."
        						}
        					],
        					"description": "For in depth information relating to Data Privacy and Security, see [Data Privacy and Security FAQs](#5b46e4da-04db-4eb1-9898-0e01295048c7).",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Migration",
        					"item": [],
        					"description": "**Migrate From On-Premises to Cloud**\n\nFor information on how to migrate from On-Premises to WhatsApp Cloud API, see [Migrate from On-Premises API to Cloud API](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/migrate-between-on-premises-and-cloud-api#on-premises-to-cloud).\n\n**Migrate From Cloud to On-Premises**\n\nFor information on how to migrate from WhatsApp Cloud API to On-Premises, see [Migrate from Cloud API to On-Premises API](hhttps://developers.facebook.com/docs/whatsapp/cloud-api/guides/migrate-between-on-premises-and-cloud-api#cloud-to-on-premises).\n\n**Migrate Phone Number to a Different WABA**\n\nFor information on how to migrate a registered phone number from one WhatsApp Business Account to another, see [Migrate Phone Number to a Different WABA](https://developers.facebook.com/docs/whatsapp/business-management-api/guides/migrate-phone-to-different-waba).",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Constraints",
        					"item": [],
        					"description": "**A single phone number can only be used on one platform at a time. One phone number for Cloud API and another number for On-Premise.** This means that you cannot use a production phone number with both the On-Premises and Cloud APIs. We recommend doing any testing with a test number (either an existing test number or a new one) and then moving your production phone number to the Cloud API when you’re confident you’re ready for production use.",
        					"auth": {
        						"type": "noauth"
        					}
        				}
        			],
        			"description": "The WhatsApp Business Platform Cloud API allows medium and large businesses to communicate with their customers at scale. Using the API, businesses can build systems that connect thousands of customers with agents or bots, enabling both programmatic and manual communication. Additionally, you can integrate the API with numerous backend systems, such as CRM and marketing platforms.",
        			"auth": {
        				"type": "noauth"
        			},
        			"event": [
        				{
        					"listen": "prerequest",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				},
        				{
        					"listen": "test",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				}
        			]
        		},
        		{
        			"name": "Get Started",
        			"item": [
        				{
        					"name": "Step 1: Set up Developer Assets and Platform Access",
        					"item": [],
        					"description": "The [WhatsApp Cloud API](https://developers.facebook.com/docs/whatsapp/cloud-api) and [WhatsApp Business Management API](https://developers.facebook.com/docs/whatsapp/business-management-api) are part of Meta’s Graph API, so you need to set up a Meta developer account and a Meta developer app. To set that up:\n\n*   [Register as a Meta Developer](https://developers.facebook.com/apps)\n*   [Enable two-factor authentication for your account](https://www.facebook.com/help/148233965247823)\n*   [Create a Meta App](https://developers.facebook.com/docs/development/create-an-app/): Go to **developers.facebook.com** > **My Apps** > **Create App**. Select the \"Business\" type and follow the prompts on your screen.\n    \n\nFrom the App Dashboard, click on the app you would like to connect to WhatsApp. Scroll down to find the \"WhatsApp\" product and click **Set up**.\n\nNext, you will see the option to select an existing Business Manager (if you have one) or, if you would like, the onboarding process can create one automatically for you (you can customize your business later, if needed). Make a selection and click **Continue**.\n\nWhen you click **Continue**, the onboarding process performs the following actions:\n\n*   Your App is associated with the Business Manager that you chose, or that was created automatically.\n*   A WhatsApp test phone number is added to your business. You can use this test phone number to explore the WhatsApp Business Platform without registering or migrating a real phone number. Test phone numbers can send unlimited messages to up to 5 recipients (which can be anywhere in the world).\n    \n\nAfter that, your browser navigates to the Getting Started tab, where you can learn about and experiment with the WhatsApp Business Platform. The Getting Started tab contains tools and information to help you send test messages. It can be used with test phone numbers and any of your own phone numbers that you register."
        				},
        				{
        					"name": "Step 2: Send a Test Message",
        					"item": [
        						{
        							"name": "Send Test Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"response": [
        								{
        									"name": "Send Test Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": null,
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						}
        					],
        					"description": "In the Getting Started tab:\n\n1.  Select your test phone number in the From field. (Unless you have registered other phone numbers, this will be the only phone number in From.)\n2.  Enter the phone number you would like to message in the To field. Ensure the number is correct, and that you want to add it to your list of 5 possible message recipients —as you add phone numbers, follow the prompts on the screen to verify you have access to them. Once this number has been added, it cannot be removed from your list. Note: This limitation is only for WhatsApp-provided test phone numbers. Real phone numbers that you register do not have a limit on the number of recipients.\n3.  Once you enter a recipient phone number, the code sample on the page will be updated to demonstrate an API call that sends a test message to that number. The code sample will look like this:\n    \n\n```\ncurl -i -X POST \\\nhttps://graph.facebook.com/{{Version}}/FROM_PHONE_NUMBER_ID/messages \\\n-H 'Authorization: Bearer ACCESS_TOKEN' \\\n-H 'Content-Type: application/json' \\\n-d '{ \"messaging_product\": \"whatsapp\", \"to\": \"TO_PHONE_NUMBER\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }'Copy Code\n\n```\n\n4\\. Click **Send message** to send the first message. As an alternative, you can copy the code sample provided and execute it in your Terminal or in Postman. You have just sent a test message!\n\nThe code sample on the page is formatted for use in Unix-style terminal shells, and is expected to work on MacOS and distributions of Gnu/Linux. If you use Windows, we suggest you perform your first API call using Postman, to avoid platform-related cURL formatting concerns. If you are a Windows 10 user, cURL is available, but requires a different syntax than the one shown in the Getting Started tab to execute in PowerShell or cmd.exe. For more information, see [cURL Comes to Windows](https://devblogs.microsoft.com/commandline/tar-and-curl-come-to-windows/) or [cURL for Windows](https://curl.se/windows/). If you have access to the [Windows Subsystem for Linux (WSL)](https://docs.microsoft.com/en-us/windows/wsl/about), you can also consider launching a Linux distribution and using its terminal."
        				},
        				{
        					"name": "Step 3: Configure a Webhook",
        					"item": [],
        					"description": "To get alerted when you receive a message or when a message’s status has changed, you need to set up a Webhooks endpoint for your app. Setting up Webhooks doesn’t affect the status of your phone number and does interfere with you sending or receiving messages.\n\nTo get started, first you need to create the endpoint. You can create a custom Webhook URL running on a web server, or use services that help you set up an endpoint, such as Glitch. See [Create a Sample App Endpoint for Webhooks Testing](https://developers.facebook.com/docs/whatsapp/sample-app-endpoints#cloud-api-sample-app-endpoint) for help.\n\nOnce your endpoint is ready, go to your App Dashboard.\n\nIn your App Dashboard, find the WhatsApp product and click **Configuration**. Then, find the Webhooks section and click **Configure a webhook**. After the click, a dialog appears on your screen and asks you for two items:\n\n*   Callback URL: This is the URL Meta will be sending the events. See the [Webhooks, Getting Started](https://developers.facebook.com/docs/graph-api/webhooks/getting-started) guide for information on creating the URL.\n*   Verify Token: This string is set up by you, when you create your Webhook endpoint.\n    \n\nAfter adding the information, click **Verify and Save**.\n\nBack in the App Dashboard, click **WhatsApp** > **Configuration** in the left-side panel. Under Webhooks, click **Manage**. A dialog box will open with all the objects you can get notified about. To receive messages from your users, click **Subscribe** for **messages**."
        				},
        				{
        					"name": "Step 4: Receive a Test Message",
        					"item": [],
        					"description": "Now that your Webhook is set up, send a message to the test number you have used. You should immediately get a Webhooks notification with the content of your message."
        				},
        				{
        					"name": "Next Steps",
        					"item": [],
        					"description": "### Phone Number\n\nWhen you’re ready to use your app for a production use case, you need to use your own phone number to send messages to your users. When choosing a phone number, consider the following:\n\n*   If you want to use a number that is already being used in the WhatsApp customer or business app, you will have to fully migrate that number to the business platform. Once the number is migrated, you will lose access to the WhatsApp customer or business app. See for information.\n*   We have a set of rules regarding numbers that can be used in the platform. .\n*   Once you have chosen your phone number, you have to add it to your WhatsApp Business Account. See [How to Connect Your Phone Number to Your WhatsApp Business Account](https://www.facebook.com/business/help/456220311516626?id=2129163877102343).\n\n### Opt-In\n\nYou are required to obtain user opt-in before sending proactive business-initiated messages. See [Get Opt-In for WhatsApp](https://developers.facebook.com/docs/whatsapp/overview/getting-opt-in) for more information.\n\n### Pricing & Payment Methods\n\nBusinesses are charged per conversation, which includes all messages delivered in a 24 hour session. The first 1,000 conversations each month are free. If you want to send more than 1,000 conversations, you need to add a credit card to your account.\n\nFor more information see [Pricing and Payment Methods](https://developers.facebook.com/docs/whatsapp/getting-started/signing-up#pricing---payment-methods).\n\n### Send More Messages\n\nTo send more business-initiated messages, you need to use message templates. WhatsApp message templates are specific message formats that businesses use to send out notifications or customer care messages to people that have opted in to notifications.\n\nBefore sending a message template, you [need to create one](https://www.facebook.com/business/help/2055875911147364) or you can [use one of our pre-approved templates](https://www.facebook.com/business/help/722393685250070). Check [this guide](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/send-message-templates) to learn how to send message templates.\n\nIncoming messages are unlimited, but there are limits for outgoing messages. See [Messaging Limits](https://developers.facebook.com/docs/whatsapp/api/rate-limits/#messaging) for more information on messaging tiers."
        				}
        			],
        			"description": "This guide helps you get started with the WhatsApp Business Platform, and is intended for people developing for themselves or their organization, not on behalf of a client. At this time, the platform is not open to those developing on behalf of clients.\n\n**NOTE**: All developers must follow [WhatsApp’s Commerce Policy](https://www.whatsapp.com/legal/commerce-policy). \n\nTo send and receive a first message using a test number, complete the following steps:"
        		},
        		{
        			"name": "Get Started for BSPs",
        			"item": [
        				{
        					"name": "Prepare and Plan",
        					"item": [],
        					"description": "### Read Documentation\n\nBefore you start, we recommend reading through our [developer documentation](https://developers.facebook.com/docs/whatsapp/cloud-api) and our [Postman collection](https://documenter.getpostman.com/view/13382743/UVC5FTHT?fbclid=IwAR1ZHbYefEkctmotxeooOM9ZV9U5yiWsD4fh64RC-QihuKgDDurd15J6ZR0). This helps you understand how the Cloud API works, including how to get started and migrate numbers.\n\n### Plan Onboarding & Migration\n\n**You must use Embedded Signup to onboard new customers to the Cloud API.** If you haven’t already, integrate and launch [Embedded Signup](https://developers.facebook.com/docs/whatsapp/embedded-signup). Embedded Signup is the fastest and easiest way to register customers, enabling them to start sending messages in less than five minutes.\n\nNext, **think about which clients you want to migrate to the Cloud API first**. Our general recommendation is to migrate all of your clients from the On-Premises to the Cloud API, but each client’s need may vary. As you think about which clients to migrate, consider:\n\n| Consideration | More Context |\n| --- | --- |\n|   <br>Are my client’s throughputs and message volumes supported by Cloud API?  <br>  <br> |   <br>The Cloud API supports most businesses at 80 messages/second cumulative peak throughput, including text/media and ingoing/outgoing.  <br>  <br> |\n|   <br>Are my client’s compliance needs met by the Cloud API?  <br> |   <br>The Cloud API is GDPR compliant and has SOC 2 certification. Servers are hosted in North America.  <br>  <br> |\n|   <br>Are my clients using features supported by the Cloud API?  <br>  <br> |   <br>Most major features are supported. See full list [here](https://developers.facebook.com/docs/whatsapp/cloud-api/overview/on-premises-cloud-api-comparison#messaging-features). |\n\nOnce you know who’s going to be migrated, you can build a migration plan and timeline.\n\nAs you create your plan, remember to design your system for two scenarios: onboarding new customers and migrating current customers from On-Premises to Cloud API. For the migration scenario, include plans to backup your current On-Premises instance and migrate those numbers to the Cloud API.\n\n### Plan Communication With Clients\n\nFirst, you need to decide whether to notify existing clients about migration. Then, you should determine if you need to create or update any documentation to support the Cloud API setup.\n\n### Make Pricing Decisions\n\nSince the hosting costs for the Cloud API are covered by Meta, you should decide if you would like to update your prices accordingly."
        				},
        				{
        					"name": "Set up Assets",
        					"item": [],
        					"description": "To use the Cloud API, BSPs need to have the following assets:\n\n| Asset | Specific Instructions |\n| --- | --- |\n|   <br>  <br>**Business Manager**  <br>  <br> |   <br>  <br>You can use an existing, or [set up a new one](https://www.facebook.com/business/help/1710077379203657). Save the Business Manager ID.  <br>  <br> |\n|   <br>  <br>**WhatsApp Business Account** (WABA)  <br>  <br> |   <br>  <br>See [Create a WhatsApp Business Account for the WhatsApp Business API](https://www.facebook.com/business/help/2087193751603668) for help.  <br>  <br> |\n|   <br>  <br>[**Meta App**](https://developers.facebook.com/apps/)  <br>  <br> |   <br>  <br>If you don’t have an app, you need to [create one](https://developers.facebook.com/docs/development/create-an-app) with the “Business” type. Remember to add a display name and a contact email to your app.  <br>  <br>  <br>  <br>As a BSP (Business Solution Provider), your app must go through [App Review](https://developers.facebook.com/docs/app-review) and request Advanced Access to the following permissions:  <br>  <br>* [`whatsapp_business_management`](https://developers.facebook.com/docs/permissions/reference/whatsapp_business_management) — Used to manage phone numbers, message templates, registration, business profile under a WhatsApp Business Account. To get this permission, your app must go through [App Review](https://developers.facebook.com/docs/app-review).  <br>* `whatsapp_business_messaging` — Used to send/receive messages from WhatsApp users, upload/download media under a WhatsApp Business Account. To get this permission, your app must go through [App Review](https://developers.facebook.com/docs/app-review).  <br>* `business_management`  <br>  <br>[See a sample App Review submission here](https://developers.facebook.com/docs/app-review/resources/sample-submissions/whatsapp-business-api).  <br>  <br>  <br>  <br>As a BSP, you can also feel free to use the same Meta app across different clients and WABAs. But be aware that each app can only have one webhook endpoint and each app needs to go through App Review.  <br>  <br> |\n|   <br>  <br>**System User**  <br>  <br> |   <br>  <br>See [Add System Users to Your Business Manager](https://www.facebook.com/business/help/503306463479099) for help.  <br>  <br>  <br>  <br>Currently, a Meta App with `whatsapp_business_messaging`, `whatsapp_business_management`, and `business_messaging` permissions has access to up to:  <br>  <br>* 1 admin system user, and  <br>* 1 employee system user  <br>  <br>We recommend using the admin system user for your production deployment. See [About Business Manager Roles and Permissions](https://www.facebook.com/business/help/442345745885606) for more information.  <br>  <br> |\n|   <br>  <br>**Business Phone Number**  <br>  <br> |   <br>  <br>This is the phone number the business will use to send messages. Phone numbers need to be verified through SMS/voice call.  <br>  <br>  <br>  <br>For BSPs and Direct Businesses: If you wish to use your own number, then you should [add a phone number](https://www.facebook.com/business/help/456220311516626) in WhatsApp Manager and verify it with the verify endpoint via [Graph API](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#verify).  <br>  <br>  <br>  <br>For Businesses using BSPs: If you wish to use your own number, then you should add and verify their numbers using the BSP’s [Embedded Signup flow](https://developers.facebook.com/docs/whatsapp/embedded-signup).  <br>  <br>  <br>The verification status of a phone number doesn't impact the migration between On-Premises and Cloud API. If you don't have access to Embedded Signup to verify phone numbers, we recommend verifying the phone numbers using the On-Premises solution, and then migrating those numbers to the Cloud API.  <br>  <br>There is no limit to the amount of business phone numbers that can be onboarded to the Cloud API.  <br>  <br>  <br>Only one phone number can be used on a platform at a time: One for [Cloud API](https://developers.facebook.com/docs/whatsapp/cloud-api) and another for [On-Premises](https://developers.facebook.com/docs/whatsapp/on-premises). This means that you cannot use a production phone number with both the On-Premises and Cloud APIs. We recommend doing any testing with a test number (either an existing test number or a new one) and then moving your own phone number to the Cloud API when you’re confident you’re ready for production use.  <br>  <br> |\n|   <br>  <br>**Consumer Phone Number**  <br>  <br> |   <br>  <br>This is a phone number that is currently using the consumer WhatsApp app. This number will be receiving the messages sent by your business phone number.  <br>  <br> |"
        				},
        				{
        					"name": "Sign Contracts",
        					"item": [],
        					"description": "### Accepting Terms of Service\n\nIn order to access the WhatsApp Business Messaging Cloud API you need to first accept the WhatsApp Business Platform Terms of Service on behalf of your business.\n\nTo do so, navigate to [WhatsApp Manager](https://business.facebook.com/wa/manage/insights) and accept the terms of service in the informational banner.\n<!--\n![](https://scontent-sea1-1.xx.fbcdn.net/v/t39.8562-6/278029604_1023061871635518_1209334055945847333_n.png?_nc_cat=101&_nc_map=test-rt&ccb=1-6&_nc_sid=6825c5&_nc_ohc=qX1VCDz2U08AX_zk9NK&_nc_ht=scontent-sea1-1.xx&oh=00_AT_knfH2QFVIQd1n5FSZ7O5D6tpKKJYr5Ifqrx1-yY3uBw&oe=628391E4)\n-->\n\nIf you are an existing beta partner for the Cloud API you have a grace period of 90 days. This means you need to accept the terms before July 5, 2022 or you will lose access.\n\nFor any new Cloud API businesses, including those migrating from the on-premises API, you will need to accept terms of service before you can start using the Cloud API. Registration calls will fail until you accept the terms of service.\n\n***NOTE: You as a developer need to accept the terms of service. If you are a Business Solution Provider, you do not need your customers to accept.***"
        				},
        				{
        					"name": "Build Integration",
        					"item": [],
        					"description": "### Step 1: Get System User Access Token\n\nGraph API calls use access tokens for authentication. For more information, see [Access Tokens](https://developers.facebook.com/docs/facebook-login/access-tokens/). We recommend using your system user to generate your token.\n\nTo generate a system user access token:\n\n1.  Go to [Business Manager](https://business.facebook.com/) > Business Settings > Users > System Users to view the system user you created.\n2.  Click on that user and select Add Assets. This action launches a new window.\n3.  Under Select Asset Type on the left side pane, select Apps. Under Select Assets, choose the Meta app you want to use (your app must have the correct permissions). Enable Develop App for that app.\n4.  Select Save Changes to save your settings and return to the system user main screen.\n5.  Now you are ready to generate your token. In the system user main screen, click Generate Token and select your Meta app. After selecting the app, you see a list of available permissions. Select `whatsapp_business_management`, `whatsapp_business_messaging`, and `business_management`. Click Generate Token.\n6.  A new window opens with your system user, assigned app and access token. Save your token.\n7.  Optionally, you can click on your token and see the Token Debugger. In your debugger, you should see the two permissions you have selected. You can also directly paste your token into the [Access Token Debugger](https://developers.facebook.com/tools/debug/accesstoken).\n    \n\n### Step 2: Set up Webhooks\n\nWith Webhooks set up, you can receive real-time HTTP notifications from the WhatsApp Business Platform. This means you get notified when, for example, you get a message from a customer or there are changes to your WhatsApp Business Account (WABA).\n\nTo set up your Webhook, you need to create an internet-facing web server with a URL that meets Meta’s and WhatsApp’s requirements. See [Creating an Endpoint](https://developers.facebook.com/docs/graph-api/webhooks/getting-started/#create-endpoint) for instructions on how to do that. If you need an endpoint for testing purposes, [you can generate a test Webhooks endpoint](https://developers.facebook.com/docs/whatsapp/api/webhooks/generate-endpoint).\n\n#### App Setup\n\nOnce the endpoint is ready, configure it to be used by your Meta app:\n\nIn your App Dashboard, find the WhatsApp product and click **Configuration**. Then, find the webhooks section and click **Configure a webhook**. After the click, a dialog appears on your screen and asks you for two items:\n\n*   Callback URL: This is the URL Meta will be sending the events to. See the [Webhooks, Getting Started](https://developers.facebook.com/docs/graph-api/webhooks/getting-started) guide for information on creating the URL.\n*   Verify Token: This string is set up by you, when you create your webhook endpoint.\n    \n\nAfter adding the information, click **Verify and Save**.\n\nBack in the App Dashboard, click **WhatsApp** > **Configuration** in the left-side panel. Under Webhooks, click **Manage**. A dialog box will open with all the objects you can get notified about. To receive messages from your users, click **Subscribe** for **messages**.\n\nYou only need to set up Webhooks once for every application you have. You can use the same Webhook to receive multiple event types from multiple WhatsApp Business Accounts. For more information, see our Webhooks section.\n\nAt any time, each Meta App can have only one endpoint configured. If you need to send your webhook updates to multiple endpoints, you need multiple Meta Apps.\n\n### Step 3: Subscribe to your WABA\n\nTo make sure you get notifications for the correct account, subscribe your app:\n\n```\ncurl -X POST \\\n  'https://graph.facebook.com/{{Version}}/WHATSAPP_BUSINESS_ACCOUNT_ID/subscribed_apps' \\\n  -H 'Authorization: Bearer ACCESS_TOKEN'\n\n```\n\n[Copy Code](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#)\n\nIf you get the response below, all Webhook events for the phone numbers under this account will be sent to your configured Webhooks endpoint.\n\n```\n{\n  \"success\": \"true\"\n}\n\n```\n\n### Step 4: Get Phone Number ID\n\nTo send messages, you need to register the phone number you want to use —this is the business phone number we mentioned in [Before You Start](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#before-you-start).\n\nBefore you can proceed to registration, you need to find that phone number’s ID. To get your phone number’s ID, make the following API call:\n\n```\ncurl -X GET \\\n 'https://graph.facebook.com/{{Version}}/WHATSAPP_BUSINESS_ACCOUNT_ID/phone_numbers' \\\n -H 'Authorization: Bearer ACCESS_TOKEN'\n\n```\n\n[Copy Code](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#)\n\nIf the request is successful, the response includes all phone numbers connected to your WABA:\n\n```\n{\n  \"data\": [\n    {\n      \"verified_name\": \"Jasper's Market\",\n      \"display_phone_number\": \"+1 631-555-5555\",\n      \"id\": \"1906385232743451\",\n      \"quality_rating\": \"GREEN\"\n    },\n    {\n      \"verified_name\": \"Jasper's Ice Cream\",\n      \"display_phone_number\": \"+1 631-555-5556\",\n      \"id\": \"1913623884432103\",\n      \"quality_rating\": \"NA\"\n    }\n  ]\n}\n\n```\n\nSave the ID for the phone number you want to register. See [Read Phone Numbers](https://developers.facebook.com/docs/whatsapp/business-management-api/phone-numbers) for more information about this endpoint.\n\n#### Migration Exception\n\nIf you are migrating a phone number from the On-Premises API to the Cloud API, there are extra steps you need to perform before registering a phone number with the Cloud API. See [Migrate Between On-Premises and Cloud API](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/migrate-between-on-premises-and-cloud-api) for the full process.\n\n### Step 5: Register Phone Number\n\nWith the phone number’s ID in hand, you can register it. In the registration API call, you perform two actions at the same time:\n\n1.  Register the phone.\n2.  [Enable two-step verification](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/two-step-verification) by setting a 6-digit registration code —you must set this code on your end. Save and memorize this code as it can be requested later. **Setting up two-factor authentication is a requirement to use the Cloud API.**\n    \n\nSample request:\n\n```\ncurl -X POST \\\n  'https://graph.facebook.com/{{Version}}/FROM_PHONE_NUMBER_ID/register' \\\n  -H 'Authorization: Bearer ACCESS_TOKEN' \\\n  -d '{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"6_DIGIT_PIN\"\n  }'\n\n```\n\n[Copy Code](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#)\n\nSample response:\n\n```\n{\n  \"success\": \"true\"\n}\n\n```\n\n#### Embedded Signup Users\n\nA phone number **must** be registered up to 14 days after going through the Embedded Signup flow. If a number is not registered during that window, the phone must go through to the Embedded Signup flow again prior to registration.\n\n### Step 6: Receive a Message From Consumer App\n\nOnce participating customers send a message to your business, you get **24 hours of free messages with them** —that window of time is called the customer service window. For testing purposes, we want to enable this window, so you can send as many messages as you would like.\n\nFrom a personal WhatsApp iOS/Android app, send a message to the phone number you just registered. Once the message is sent, you should receive an incoming message to your Webhook with a notification in the following format.\n\n```\n{\n  \"object\": \"whatsapp_business_account\",\n  \"entry\": [\n    {\n      \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n      \"changes\": [\n        {\n          \"value\": {\n            \"messaging_product\": \"whatsapp\",\n            \"metadata\": {\n              \"display_phone_number\": \"16315551234\",\n              \"phone_number_id\": \"PHONE_NUMBER_ID\"\n            },\n            \"contacts\": [\n              {\n                \"profile\": {\n                  \"name\": \"Kerry Fisher\"\n                },\n                \"wa_id\": \"16315555555\"\n              }\n            ],\n            \"messages\": [\n              {\n                \"from\": \"16315555555\",\n                \"id\": \"wamid.ABGGFlA5FpafAgo6tHcNmNjXmuSf\",\n                \"timestamp\": \"1602139392\",\n                \"text\": {\n                  \"body\": \"Hello!\"\n                },\n                \"type\": \"text\"\n                }\n            ]\n          },\n        \"field\": \"messages\"\n        }\n      ]\n    }\n  ]\n}\n\n```\n\n### Step 7: Send a Test Message\n\nOnce you have enabled the customer service window, you can send a test message to the consumer number you used in the previous step. To do that, make the following API call:\n\n```\ncurl -X  POST \\\n  'https://graph.facebook.com/{{Version}}/FROM_PHONE_NUMBER_ID/messages' \\\n  -H 'Authorization: Bearer ACCESS_TOKEN' \\\n  -d '{\n      \"messaging_product\": \"whatsapp\", \n      \"to\": \"16315555555\",\n      \"text\": {\"body\" : \"hello world!\"}\n  }'\n\n```\n\n[Copy Code](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#)\n\nIf your call is successful, your response will include a message ID. Use that ID to track the progress of your messages via Webhooks.\n\nSample response:\n\n```\n{\n    \"id\":\"wamid.gBGGFlaCGg0xcvAdgmZ9plHrf2Mh-o\"\n}\n\n```\n\nWith the Cloud API, there is no longer a way to explicitly check if a phone number has a WhatsApp ID. To send someone a message using the Cloud API, just send it directly to the customer's phone number —after they have [opted-in](https://developers.facebook.com/docs/whatsapp/overview/getting-opt-in). See [Reference, Messages](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#examples) for examples."
        				},
        				{
        					"name": "Step 1: Get System User Access Token",
        					"item": [],
        					"description": "Graph API calls use access tokens for authentication. For more information, see [Access Tokens](https://developers.facebook.com/docs/facebook-login/access-tokens/). We recommend using your system user to generate your token.\n\nTo generate a system user access token:\n\n1.  Go to [Business Manager](https://business.facebook.com/) > Business Settings > Users > System Users to view the system user you created.\n2.  Click on that user and select Add Assets. This action launches a new window.\n3.  Under Select Asset Type on the left side pane, select Apps. Under Select Assets, choose the Meta app you want to use (your app must have the correct permissions). Enable Develop App for that app.\n4.  Select Save Changes to save your settings and return to the system user main screen.\n5.  Now you are ready to generate your token. In the system user main screen, click Generate Token and select your Meta app. After selecting the app, you see a list of available permissions. Select `whatsapp_business_management`, `whatsapp_business_messaging`, and `business_management`. Click Generate Token.\n6.  A new window opens with your system user, assigned app and access token. Save your token.\n7.  Optionally, you can click on your token and see the Token Debugger. In your debugger, you should see the two permissions you have selected. You can also directly paste your token into the [Access Token Debugger](https://developers.facebook.com/tools/debug/accesstoken)."
        				},
        				{
        					"name": "Step 2: Set up Webhooks",
        					"item": [],
        					"description": "With Webhooks set up, you can receive real-time HTTP notifications from the WhatsApp Business Platform. This means you get notified when, for example, you get a message from a customer or there are changes to your WhatsApp Business Account (WABA).\n\nTo set up your Webhook, you need to create an internet-facing web server with a URL that meets Meta’s and WhatsApp’s requirements. See [Creating an Endpoint](https://developers.facebook.com/docs/graph-api/webhooks/getting-started/#create-endpoint) for instructions on how to do that. If you need an endpoint for testing purposes, [you can generate a test Webhooks endpoint](https://developers.facebook.com/docs/whatsapp/api/webhooks/generate-endpoint).\n\n#### App Setup\n\nOnce the endpoint is ready, configure it to be used by your Meta app:\n\nIn your App Dashboard, find the WhatsApp product and click **Configuration**. Then, find the webhooks section and click **Configure a webhook**. After the click, a dialog appears on your screen and asks you for two items:\n\n*   Callback URL: This is the URL Meta will be sending the events to. See the [Webhooks, Getting Started](https://developers.facebook.com/docs/graph-api/webhooks/getting-started) guide for information on creating the URL.\n*   Verify Token: This string is set up by you, when you create your Webhook endpoint.\n    \n\nAfter adding the information, click **Verify and Save**.\n\nBack in the App Dashboard, click **WhatsApp** > **Configuration** in the left-side panel. Under Webhooks, click **Manage**. A dialog box will open with all the objects you can get notified about. To receive messages from your users, click **Subscribe** for **messages**.\n\nYou only need to set up Webhooks once for every application you have. You can use the same Webhook to receive multiple event types from multiple WhatsApp Business Accounts. For more information, see our Webhooks section.\n\nAt any time, each Meta App can have only one endpoint configured. If you need to send your Webhook updates to multiple endpoints, you need multiple Meta Apps."
        				},
        				{
        					"name": "Step 3: Subscribe to your WABA",
        					"request": {
        						"method": "POST",
        						"header": [],
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{WABA-ID}}",
        								"subscribed_apps"
        							]
        						},
        						"description": "To make sure you receive notifications for your account you need to subscribe your app to your WABA.\n\n#### Response\n\nIf your request is successful, all Webhook events for the phone numbers under this account are sent to your configured Webhooks endpoint."
        					},
        					"response": [
        						{
        							"name": "Step 3: Subscribe to your WABA",
        							"originalRequest": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"subscribed_apps"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"success\": \"true\"\n}"
        						}
        					]
        				},
        				{
        					"name": "Step 4: Get Phone Number ID",
        					"request": {
        						"method": "GET",
        						"header": [],
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/phone_numbers",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{WABA-ID}}",
        								"phone_numbers"
        							]
        						},
        						"description": "To send messages, you need to register the phone number you want to use. This is the business phone number we mentioned in [Set up Assets](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#set-up-assets).\n\nBefore you can proceed to registration, you need to find that phone number’s ID.\n\n#### Response\n\nIf the request is successful, the response includes all phone numbers connected to your WABA.\n\nSave the ID for the phone number you want to register. For more information about this endpoint, see [Read Phone Numbers](https://developers.facebook.com/docs/whatsapp/business-management-api/phone-numbers).\n\n#### Migration Exception\n\nIf you are migrating a phone number from the On-Premises API to the Cloud API, there are extra steps you need to perform before registering a phone number with the Cloud API. For more information regarding the full process, see [Migrate Between On-Premises and Cloud API](#79b8156b-8c83-4db9-94ac-dd9aaaa67390).\n\nTo get your phone number’s ID, make the following API call:"
        					},
        					"response": [
        						{
        							"name": "Step 4: Get Phone Number ID",
        							"originalRequest": {
        								"method": "GET",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/phone_numbers",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"phone_numbers"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"data\": [\n        {\n            \"verified_name\": \"Jasper's Market\",\n            \"display_phone_number\": \"+1 631-555-5555\",\n            \"id\": \"1906385232743451\",\n            \"quality_rating\": \"GREEN\"\n        },\n        {\n            \"verified_name\": \"Jasper's Ice Cream\",\n            \"display_phone_number\": \"+1 631-555-5556\",\n            \"id\": \"1913623884432103\",\n            \"quality_rating\": \"NA\"\n        }\n    ]\n}"
        						}
        					]
        				},
        				{
        					"name": "Step 5: Register Phone Number",
        					"request": {
        						"method": "POST",
        						"header": [
        							{
        								"key": "Content-Type",
        								"value": "application/json",
        								"type": "text"
        							}
        						],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"6-digit-pin\"\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{Phone-Number-ID}}",
        								"register"
        							]
        						},
        						"description": "With your phone number’s ID in hand, you can register it. In the registration API call, you perform two actions at the same time:\n\n1.  Register the phone.\n2.  Enable [two-step verification](https://faq.whatsapp.com/general/verification/about-two-step-verification) by setting a 6-digit registration code — you must set this code on your end. Save and memorize this code as it can be requested later. **Setting up two-factor authentication is a requirement to use the Cloud API.**\n    \n\n**Embedded Signup Users**\n\nA phone number **must** be registered within 14 days after going through the Embedded Signup flow. If the phone number is not registered during that window, the phone number must go through the Embedded Signup flow again prior to registration."
        					},
        					"response": [
        						{
        							"name": "Step 5: Register Phone Number",
        							"originalRequest": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"6-digit-pin\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"register"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"success\": \"true\"\n}"
        						}
        					]
        				},
        				{
        					"name": "Step 6: Receive a Message from Consumer App",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"1900820329959633\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16315551234\",\n                            \"phone_number_id\": \"16315551234\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"Kerry Fisher\"\n                                },\n                                \"wa_id\": \"16315555555\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"16315555555\",\n                                \"id\": \"wamid.ABGGFlA5FpafAgo6tHcNmNjXmuSf\",\n                                \"timestamp\": \"1602139392\",\n                                \"text\": {\n                                    \"body\": \"Hello!\"\n                                },\n                                \"type\": \"text\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "Once participating customers send a message to your business, you get **24 hours of free messages with them** — that window of time is called the customer service window. For testing purposes, we want to enable this window, so you can send as many messages as you want.\n\nFrom a personal WhatsApp iOS/Android app, send a message to the phone number you just registered. Once the message is sent, you should receive an incoming message to your Webhook with a notification in the following format."
        					},
        					"response": []
        				},
        				{
        					"name": "Step 7: Send a Test Message",
        					"request": {
        						"method": "POST",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"16315555555\",\n    \"text\": {\n        \"body\": \"hello world!\"\n    }\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{Phone-Number-ID}}",
        								"messages"
        							]
        						},
        						"description": "After you have enabled the customer service window, you can send a test message to the consumer number you used in the previous steps. To do this, see the API call below.\n\n#### Response\n\nIf successful, your sample response includes a message ID (`wamid.############`). You need to use that ID to track the progress of your messages via Webhooks."
        					},
        					"response": [
        						{
        							"name": "Step 7: Send a Test Message",
        							"originalRequest": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"16315555555\",\n    \"text\": {\n        \"body\": \"hello world!\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"id\":\"wamid.gBGGFlaCGg0xcvAdgmZ9plHrf2Mh-o\"\n}"
        						}
        					]
        				}
        			],
        			"description": "<!--At this time, this API is only available to Business Solution Providers (BSPs) and existing direct developers.-->\n\nThis guide goes over the steps Business Solution Providers (BSP) need to take in order to offer the Cloud API to their customers. There are 4 main stages:\n\n1.  [Prepare & Plan](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#prepare-plan)\n2.  [Set up Assets](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#set-up-assets)\n3.  [Sign Contracts](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#sign-contracts)\n4.  [Build Integration](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#build-integration)\n    \n\nAfter you’re done, please [keep up with monthly updates](https://developers.facebook.com/docs/whatsapp/cloud-api/get-started#keep-up-monthly-updates)."
        		},
        		{
        			"name": "Guides",
        			"item": [
        				{
        					"name": "Mark Messages as Read",
        					"item": [],
        					"description": "When you receive an incoming message from Webhooks, you can use the `/messages` endpoint to change the status of it to read. We recommend marking incoming messages as read within 30 days of receipt.\n\nYou cannot mark outgoing messages you sent as read.\n\n## Step 1: Obtain `message_id` of incoming message\n\nFirst, find the `message_id` for the message you want to mark as `read`. You can find this ID in the webhooks notification you get when you receive a message. See [Webhooks](https://developers.facebook.com/docs/whatsapp/cloud-api/webhooks) for information.\n\n## Step 2: Make API call to update message status\n\nCall the `/PHONE_NUMBER_ID/messages` endpoint and include your `message_id`. Set the `status` field to `read`:\n\n``` json\ncurl -X  POST \\\n 'https://graph.facebook.com/{{Version}}/FROM_PHONE_NUMBER_ID/messages' \\\n -H 'Authorization: Bearer SYSTEM_USER_ACCESS_TOKEN' \\\n -d '{\n  \"messaging_product\": \"whatsapp\",\n  \"status\": \"read\",\n  \"message_id\": \"MESSAGE_ID\"\n  }\n}'    \n\n```\n\nA successful response looks like this:\n\n``` json\n{\n  \"success\": true\n}\n\n```\n\nFor more information on marking messages as read, see [Mark Messages as Read](https://documenter.getpostman.com/view/13382743/UVC5FTHT#b481139d-c887-40ec-8c07-732d1b69d468) in the Cloud API reference."
        				},
        				{
        					"name": "Migrate Between On-Premise and Cloud API",
        					"item": [
        						{
        							"name": "On-Premise to Cloud API",
        							"item": [],
        							"description": "To migrate from On-Premises to Cloud API, follow these steps:\n\n**Optional Step 0: Verify Phone Numbers**\n\n*(This step is only relevant if you don’t have access to Embedded Signup to verify phone numbers. If that doesn’t apply to you, move to Step 1.)*\n\nThe verification status of a phone number doesn't impact the migration between On-Premises and Cloud API. If you don't have access to Embedded Signup to verify phone numbers, we recommend verifying the phone numbers using the On-Premises solution, and then migrating those numbers to the Cloud API.\n\n**Step 1: Select Best Time for Migration**\n\nWe recommend performing migration during a maintenance window while traffic to the On-Premises deployment is low/turned off. Downtime is typically less than 5 minutes, and no re-verification of the phone number is required.\n\n**Step 2: Backup On-Premises Content**\n\nWe don’t provide full database migration, so you must back up your data before moving a phone number from On-Premises to Cloud API. Before starting the backup, make sure you are in single instance mode and not multi-connect.\n\nOnce you are ready, use the On-Premises [Backup API](https://developers.facebook.com/docs/whatsapp/api/settings/backup-and-restore/):\n\n``` json\nPOST /v1/settings/backup  \n{  \n    \"password\": \"your-password\"  \n}\n\n```\n\nA successful response looks like this:\n\n``` json\n{  \n    \"settings\": \n    {  \n      \"data\": \"encrypted-backup-data\"  \n    }  \n}\n\n```\n\nSave the data for the next step.\n\n**Step 3: Call /register to Start Migration**\n\nTo migrate your account, make a **POST** call to the **`PHONE_NUMBER_ID/register`** endpoint. Inside the backup object, include the backup data obtained during the previous step:\n\n``` bash\n`curl -X POST \\ 'https://graph.facebook.com/{{Version}}/FROM_PHONE_NUMBER_ID/register' \\\n-H 'Authorization: Bearer SYSTEM_USER_ACCESS_TOKEN' \\   \n-d '{   \n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"6_DIGIT_PIN\",\n    \"backup\": \n    {   \n        \"password\": \"PASSWORD\",   \n        \"data\": \"BACKUP_DATA\"   \n    }   \n}'\n\n```\n\nA successful response looks like this:\n\n``` json\n{  \n  \"success\": \"true\"  \n}\n\n```\n\nSee all available fields for this endpoint [here](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/account-migration#parameters).\n\n**Step 4: Prepare to Send Messages**\n\nYou have been migrated to the Cloud API. But, before sending messages, you should perform necessary operations, like [re-uploading media files](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media) you want to send. Be aware that data (messages, media, etc.) received on the On-Premises API deployment before migration won’t be available in the Cloud API after migration.\n\n**Step 5: Start Sending Messages**\n\nYou are ready to send messages to your customers. See the [/messages endpoint documentation](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages) for guidance."
        						},
        						{
        							"name": "Cloud API to On-Premise",
        							"item": [],
        							"description": "To migrate from Cloud API to On-Premises API, follow these steps:\n\n**Step 1: Prepare for Migration**\n\nWe suggest that you stop sending messages while you complete the migration.\n\nThe WhatsApp Business API On-Premises client has certain network requirements for connecting to the WhatsApp servers. To make sure you are ready, check [Set Up and Debug Your Network](https://developers.facebook.com/docs/whatsapp/guides/network-requirements).\n\n**Step 2: Register API Client**\n\nTo move from Cloud API to On-Premises you need to register your WhatsApp Business API client. To do that, call the [**`/account`**](https://developers.facebook.com/docs/whatsapp/api/account) endpoint:\n\n``` json\nPOST /v1/account  \n{  \n    \"cc\": \"<your-country-code>\",  \n    \"phone_number\": \"<your-phone-number-without-country-code>\",  \n    \"method\": \"<sms-or-voice>\",  \n    \"cert\": \"<your-verified-name-cert-in-base64>\",  \n    \"pin\": \"<your-existing-6-digit-pin>\"\n}\n\n```\n\nDepending on the response received, the registration procedure can be considered complete or require another step to complete. If successful, you receive one of the following HTTP status codes. Follow the instructions that match the response you received:\n\n*   **201 Created** — The account already exists. You are already registered, so you do not need to do anything else.\n*   **202 Accepted** — The account does not exist. Depending on the method selected in the request, check your SMS or voice number for the registration code. This response will include a returned payload that contains the **`vname`** decoded from the cert parameter for you to confirm the right display name is being set. If correct, proceed to [Completing Your Account Registration](https://developers.facebook.com/docs/whatsapp/api/account/verify) to complete registration.\n    \n\nSee all available fields for this endpoint [here](https://developers.facebook.com/docs/whatsapp/api/account#parameters).\n\n**Step 3: Set Shards**\n\nOnce your client is registered, you can [set shards](https://developers.facebook.com/docs/whatsapp/multiconnect_mc), if needed.\n\n**Step 4: Start Sending Messages**\n\nYou are ready to send messages to your customers. For more information, see [**`/messages`**](https://developers.facebook.com/docs/whatsapp/guides/messages) endpoint documentation."
        						}
        					],
        					"description": "This guide walks you through the process of migrating between On-Premises and Cloud API.\n\n*   [Migrate from On-Premises API to Cloud API](#c2070ecb-9bf6-41c8-8452-7209c7820254)\n*   [Migrate from Cloud API to On-Premises API](#46bb6f38-12f5-417f-af0b-729c08c027fe)",
        					"auth": {
        						"type": "noauth"
        					}
        				},
        				{
        					"name": "Sell Products and Services",
        					"item": [],
        					"description": "Businesses using the WhatsApp Business API can showcase and share their products and services with customers for them to browse items and add to a cart without leaving the chat. To learn more about products and services, see [Sell Products & Services](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/sell-products-and-services)."
        				}
        			],
        			"description": "These guides walk through various tasks you can accomplish with the WhatsApp Business Cloud API."
        		},
        		{
        			"name": "API Reference",
        			"item": [
        				{
        					"name": "Registration",
        					"item": [
        						{
        							"name": "Register Phone",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"<your-6-digit-pin>\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"register"
        									]
        								},
        								"description": "To register your phone, make a **POST** call to **`/{{Phone-Number-ID}}/register`**. You need to include the following parameters.\n\n## Request Parameters\n\n| Name                    | Description              |\n|-------------------------|--------------------------|\n| **`messaging_product`** | **Required**.<br/>The messaging service used. This value always needs to be set to `\"whatsapp\"`. |\n| **`pin`**               | **Required**.<br/>A 6-digit pin you previously set up. For more information, see [Set Two-Step Verification](#08c441cc-8837-4ce5-8e0d-73fb0125c323). |"
        							},
        							"response": [
        								{
        									"name": "Register Phone",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"123456\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"register"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Deregister Phone",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": ""
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/deregister",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"deregister"
        									]
        								},
        								"description": "To deregister your phone, make a **POST** call to **`{{Phone-Number-ID}}/deregister`**. **Deregister Phone** removes a previously registered phone. You can always re-register your phone using by repeating the registration process.\n\n#### Response\n\nA successful response returns:\n\n``` json\n{\n    \"success\": true\n}\n\n```"
        							},
        							"response": [
        								{
        									"name": "Deregister Phone",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/deregister",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"deregister"
        											]
        										}
        									},
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						}
        					],
        					"description": "You need to register your phone number in the following scenarios:\n\n*   Account Creation: When you implement this API, you need to register the phone number you want to use to send messages. We enforce [setting two-step verification](#fc57a30c-97e0-4e06-b74b-89fd7fc5f783) during account creation to add an extra layer of security of your accounts.\n*   Name Change: In this case, your phone is already registered and you want to change your display name. To do that, you must first [file for a name change on WhatsApp Manager](https://www.facebook.com/business/help/378834799515077). Once the name is approved, you need to register your phone again under the new name.\n    \n\nBefore registering your phone, you need to verify that you own that phone number with a SMS or voice code. For details, see [Verify Phone Ownership](https://developers.facebook.com/docs/whatsapp/business-management-api/guides/migrate-phone-to-different-waba#step-2--verify-phone-ownership).\n\nIn case you would like to remove your phone from the Cloud API, you can deregister a phone. This can be used in cases where you want to move to the On-Premises API or you want to use your phone number in the regular WhatsApp customer app. You can always reregister your phone with Cloud API later by repeating the registration process.\n\n**You set up** [**two-factor verification**](#fc57a30c-97e0-4e06-b74b-89fd7fc5f783) **and** [**register a phone number**](#b22af3db-9d13-4467-a7a6-4026f71984cb) **in the same API call.**\n\n#### Reminders\n\n*   To use these endpoints, you need to authenticate yourself with a system user access token with the **`whatsapp_business_messaging`** permission.\n*   If you need to find your phone number ID, see [Get Phone Number ID](#c72d9c17-554d-4ae1-8f9e-b28a94010b28)."
        				},
        				{
        					"name": "Account Migration",
        					"item": [
        						{
        							"name": "Migrate Account",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"6-digit-pin\",\n    \"backup\": {\n        \"data\": \"backup_data\",\n        \"password\": \"password\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"register"
        									]
        								},
        								"description": "To migrate your account, make a **POST** call to the **`/{{Phone-Number-ID}}/register`** endpoint and include the parameters listed below. \n\nYour request may take as long as 15 seconds to finish. During this period, your on-premise deployment is automatically disconnected from WhatsApp server and shutdown; the business account will start up in the cloud-hosted service at the same time. After the request finishes successfully, you can send messages immediately.\n\n## Request Parameters\n| Name                  | Description               |\n|-----------------------|---------------------------|\n|**`messaging_product`**| **Required**.<br/>Messaging service used for the request. For account migration, always use `\"whatsapp\"`. \n| **`pin`**             |**Required**.<br/>A 6-digit pin you have previously set up — See [Set Two-Step Verification](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/two-step-verification) If you use the wrong pin, your on-premise deployment will be down and will be disconnected from the WhatsApp servers.<br/>If you haven't set up or you have disabled two-step verification, provide a random 6-digit pin in the request, and this will be used to enable two-step verification in the WhatsApp Business Cloud-Based API. |\n|**`backup.data`** | **Required**.<br/>The data value you get when you backup your on-premise deployment. This contains the account registration info and application settings. <br/>For more information, see [Backup and Restore](https://developers.facebook.com/docs/whatsapp/api/settings/backup-and-restore) to backup your on-premise implementation. |\n|**`backup.password`** | **Required**.<br/>The password you used in the backup API of your On-Premise deployment.|"
        							},
        							"response": [
        								{
        									"name": "Migrate Account",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"pin\": \"123678\",\n    \"backup\": {\n        \"data\": \"BACKUP_DATA\",\n        \"password\": \"P455w0rd##\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/register",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"register"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": \"true\"\n}"
        								}
        							]
        						}
        					],
        					"description": "Use the **`/{{Phone-Number-ID}}/register`** endpoint to migrate your WhatsApp Business Accounts from your current on-premise deployment to the new Cloud-Based API. \n\n#### Reminders\n\n* To use this endpoint, you need to authenticate yourself with a system user access token with the **`whatsapp_business_management`** permission.\n\n* If you need to find your phone number ID, see [Get Phone Number ID](#c72d9c17-554d-4ae1-8f9e-b28a94010b28)."
        				},
        				{
        					"name": "Business Profiles",
        					"item": [
        						{
        							"name": "Resumable Upload - Create an Upload Session",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "OAuth {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/app/uploads/?file_length=<YOUR_FILE_LENGTH>&file_type=image/jpeg&file_name=myprofile.jpg",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"app",
        										"uploads",
        										""
        									],
        									"query": [
        										{
        											"key": "file_length",
        											"value": "<YOUR_FILE_LENGTH>",
        											"description": "**Required**<br/>Specifies the size of your file in bytes."
        										},
        										{
        											"key": "file_type",
        											"value": "image/jpeg",
        											"description": "**Required**<br/>Specifies the MIME type. Values are <ul><li>`image/jpeg`</li><li>`image/png`</li><li>`video/mp4`</li></ul>"
        										},
        										{
        											"key": "file_name",
        											"value": "myprofile.jpg",
        											"description": "**Optional**<br/>Specifies the file name you are using to create the session."
        										}
        									]
        								},
        								"description": "The Resumable Upload series of requests allow you to upload Profile Pictures to Meta so you can receive a handle to update these pictures in the Business Profile API. The Resumable Upload requests consist of the following:\n\n* **Resumable Upload - Create an Upload Session**\n* **Resumable Upload - Upload File Data**\n* **Resumable Upload - Query File Upload Status**\n\nTo create a new upload session, make a **POST** call using the `/app/uploads` endpoint.\n\nFor more information, see [Create an Upload Session](https://developers.facebook.com/docs/graph-api/guides/upload#step-1--create-a-session).\n\n**Response**  \nThe call returns a server-generated value that includes the session ID that you can use in later calls. \n\n>Copy this value and Open the **Environment quick look** in Postman and paste it in the **CURRENT VALUE** for `Upload-ID`."
        							},
        							"response": [
        								{
        									"name": "Resumable Upload - Create an Upload Session",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        												"key": "Authorization",
        												"value": "OAuth {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/app/uploads/?file_length=14502&file_type=image/jpeg&file_name=myprofile.jpg",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"app",
        												"uploads",
        												""
        											],
        											"query": [
        												{
        													"key": "file_length",
        													"value": "14502",
        													"description": "**Required**<br/>Specifies the size of your file in bytes."
        												},
        												{
        													"key": "file_type",
        													"value": "image/jpeg",
        													"description": "**Required**<br/>Specifies the MIME type. Values are <ul><li>`image/jpeg`</li><li>`video/mp4`</li></ul>"
        												},
        												{
        													"key": "file_name",
        													"value": "myprofile.jpg",
        													"description": "**Optional**<br/>Specifies the file name you are using to create the session."
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"id\": \"upload:MTphdHRhY2htZW50Ojlk2mJiZxUwLWV6MDUtNDIwMy05yTA3LWQ4ZDPmZGFkNTM0NT8=?sig=ARZqkGCA_uQMxC8nHKI\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Resumable Upload - Upload File Data",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "image/jpeg",
        										"type": "text"
        									},
        									{
        										"key": "file_offset",
        										"value": "0",
        										"description": "Specifies the offset to start the upload. The offset value should always be `0`.",
        										"type": "text"
        									},
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "OAuth {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "file",
        									"file": {
        										"src": "/Users/Sample.jpg"
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Upload-ID}}",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Upload-ID}}"
        									]
        								},
        								"description": "To upload a profile picture to your business profile, make a **POST** call to the named endpoint {{Version}}/{{Upload-ID}}, where **Upload-ID** is the value you received from **Resumable Upload - Create an Upload Session**. This value should look like the following:\n\n``` json\n\"upload:MTphdHRhY2htZW50Ojlk2mJiZxUwLWV6MDUtNDIwMy05yTA3LWQ4ZDPmZGFkNTM0NT8=?sig=ARZqkGCA_uQMxC8nHKI\"\n\n```\n\nThe **`file_offset`** parameter **must** be included as an HTTP header. It will not work as a query parameter.\n\nThe access token must be included in an Authorization HTTP header. It cannot work as a query parameter.\n\nFor more information, see [Initiate Data Upload](https://developers.facebook.com/docs/graph-api/guides/upload#step-2--initiate-upload).\n\n**Response**  \nThe call returns a handle that includes the session ID that you can use to update your profile picture using **Update Business Profile**."
        							},
        							"response": [
        								{
        									"name": "Resumable Upload - Upload File Data",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "image/jpeg",
        												"type": "text"
        											},
        											{
        												"key": "file_offset",
        												"value": "0",
        												"description": "Specifies the offset to start the upload. The offset value should always be `0`.",
        												"type": "text"
        											},
        											{
        												"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        												"key": "Authorization",
        												"value": "OAuth {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "file",
        											"file": {
        												"src": "/Users/Sample.jpg"
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Upload-ID}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Upload-ID}}"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": null,
        									"cookie": [],
        									"body": "{\n    \"h\":\"2:c2FtcGxlLm1wNA==:image/jpeg:GKAj0gAUCZmJ1voFADip2iIAAAAAbugbAAAA:e:1472075513:ARZ_3ybzrQqEaluMUdI\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Resumable Upload - Query File Upload Status",
        							"request": {
        								"method": "GET",
        								"header": [
        									{
        										"key": "Cache-Control",
        										"value": "no-cache",
        										"type": "text"
        									},
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "OAuth {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Upload-ID}}",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Upload-ID}}"
        									]
        								},
        								"description": "You can query the status of an upload session by making a **GET** call to an endpoint that is named based on the **`Upload-ID`** that was returned through the **Resumable Upload - Create an Upload Session** request.\n\nWhen uploading data, you **must include the access token as an HTTP header.**\n\n**Example**\n``` bash\nGET https://graph.facebook.com/v14.0/upload:MTphdHRhY2htZW50Ojlk2mJiZxUwLWV6MDUtNDIwMy05yTA3LWQ4ZDPmZGFkNTM0NT8=?sig=ARZqkGCA_uQMxC8nHKI HTTP/1.1\nAuthorization: OAuth {{USER_ACCESS_TOKEN}}\n\n```\n\nFor more information, see [Query File Upload Status after an Interruption](https://developers.facebook.com/docs/graph-api/guides/upload#interruptions).\n\n**Response**\nThe result will be a JSON-encoded ID and offset that looks like the following:\n\n``` json\n{ \"id\": \"upload:MTphdHRhY2htZW50Ojlk2mJiZxUwLWV6MDUtNDIwMy05yTA3LWQ4ZDPmZGFkNTM0NT8=?sig=ARZqkGCA_uQMxC8nHKI\", \"file_offset\": 0 }\n```"
        							},
        							"response": [
        								{
        									"name": "Resumable Upload - Query File Upload Status",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Cache-Control",
        												"value": "no-cache",
        												"type": "text"
        											},
        											{
        												"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        												"key": "Authorization",
        												"value": "OAuth {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Upload-ID}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Upload-ID}}"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": null,
        									"cookie": [],
        									"body": "{ \n    \"id\": \"upload:MTphdHRhY2htZW50Ojlk2mJiZxUwLWV6MDUtNDIwMy05yTA3LWQ4ZDPmZGFkNTM0NT8=?sig=ARZqkGCA_uQMxC8nHKI\", \n    \"file_offset\": 0 \n}"
        								}
        							]
        						},
        						{
        							"name": "Get Business Profile",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/whatsapp_business_profile",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"whatsapp_business_profile"
        									]
        								},
        								"description": "To get information about a business profile, make a **GET** call to the **`/{{Phone-Number-ID}}/whatsapp_business_profile`** endpoint. Within the **`whatsapp_business_profile`** request, you can specify what you want to know from the business. \n\n## Fields\n\n| Name                    | Description                          |\n|-------------------------|--------------------------------------|\n| **`messaging_product`** | **Required**.<br/>The messaging service used for the request. Always set the value to `\"whatsapp\"` if you are using WhatsApp for Business API.|\n| **`address`**           | The address of the business. The maximum character limit for the string is 256. |\n| **`description`** |Description of the business. The maximum character limit for the string is 256. |\n| **`vertical`** | **Optional**. <br/>The industry type of the business. This can be either an empty string or one of the accepted values<br/><br/>**Values**: `UNDEFINED`, `OTHER`, `AUTO`, `BEAUTY`, `APPAREL`, `EDU`, `ENTERTAIN`, `EVENT_PLAN`, `FINANCE`, `GROCERY`, `GOVT`, `HOTEL`, `HEALTH`, `NONPROFIT`, `PROF_SERVICES`, `RETAIL`, `TRAVEL`, `RESTAURANT`, or `NOT_A_BIZ`.|\n| **`about`** | **Optional**. <br/>The text to display in business profile's About section The max length for the string is 139 characters. The minimum length for the string is 1 character.<br/><br/>Rendered emojis are supported however their unicode values are not. Emoji unicode values must be Java- or JavaScript-escape encoded.|\n| **`email`** | **Optional**. <br/>The contact email address (in valid email format) of the business. The maximum character limit for the string is 128 characters.|\n| **`websites`** | **Optional**. <br/>The URLs associated with the business. For instance, a website, Facebook Page, or Instagram. You must include the http:// or https:// portion of the URL. There is a maximum of 2 websites with a maximum of 256 characters each. |\n| **`profile_picture_handle`** | **Optional**. <br/>The handle of the profile picture generated from a call to the [Resumable Upload API](https://developers.facebook.com/docs/graph-api/guides/upload).|\n\n<!-- grahamp 10262022: Removed table item:\n| **`id`**                | **Required**.<br/>The ID of the business profile object. |-->"
        							},
        							"response": [
        								{
        									"name": "Get Business Profile ID",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/whatsapp_business_profile?fields=about,address,description,email,profile_picture_url,websites,vertical",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"whatsapp_business_profile"
        											],
        											"query": [
        												{
        													"key": "fields",
        													"value": "about,address,description,email,profile_picture_url,websites,vertical"
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"data\": [\n        {\n            \"business_profile\": {\n                \"messaging_product\": \"whatsapp\",\n                \"address\": \"business-address\",\n                \"description\": \"business-description\",\n                \"vertical\": \"business-industry\",\n                \"about\": \"profile-about-text\",\n                \"email\": \"business-email\",\n                \"websites\": [\n                    \"https://website-1\",\n                    \"https://website-2\"\n                ],\n                \"profile_picture_url\": \"<PROFILE_PICTURE_URL>\"                \n            }\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Update Business Profile",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"address\": \"<business-address>\",\n    \"description\": \"<business-description>\",\n    \"vertical\": \"<business-industry>\",\n    \"about\": \"<profile-about-text>\",\n    \"email\": \"<business-email>\",\n    \"websites\": [\n        \"<https://website-1>\",\n        \"<https://website-2>\"\n    ],\n    \"profile_picture_handle\": \"<IMAGE_HANDLE_ID>\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/whatsapp_business_profile",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"whatsapp_business_profile"
        									]
        								},
        								"description": "Update the business profile information such as the business description, email or address. To update your profile, make a **POST** call to **`/{{Phone-Number-ID}}/whatsapp_business_profile`**. In your request, you can include the parameters listed below.\n\nIt is recommended that you use **Resumable Upload - Create an Upload Session** to obtain an upload ID. Then use this upload ID in a call to **Resumable Upload - Upload File Data** to obtain the picture handle. This handle can be used for the **`profile_picture_handle`**.\n\n## Request Parameters\n\n| Name                    | Description                          |\n|-------------------------|--------------------------------------|\n| **`messaging_product`** | **Required**.<br/>The messaging service used for the request. Always set the value to `\"whatsapp\"` if you are using WhatsApp for Business API.|\n| **`address`**           | **Optional**.<br/>The address of the business. The maximum character limit for the string is 256. |\n| **`description`** | **Optional**.<br/>The description of the business. The maximum character limit for the string is 256. |\n| **`vertical`** | **Optional**. <br/>The industry type of the business. This can be either an empty string or one of the accepted values.<br/><br/>**Values**: `UNDEFINED`, `OTHER`, `AUTO`, `BEAUTY`, `APPAREL`, `EDU`, `ENTERTAIN`, `EVENT_PLAN`, `FINANCE`, `GROCERY`, `GOVT`, `HOTEL`, `HEALTH`, `NONPROFIT`, `PROF_SERVICES`, `RETAIL`, `TRAVEL`, `RESTAURANT`, or `NOT_A_BIZ`.|\n| **`about`** | **Optional**. <br/>The text to display in business profile's About section. The max length for the string is 139 characters. The minimum length for the string is 1 character. <br/><br/>Rendered emojis are supported however their unicode values are not. Emoji unicode values must be Java- or JavaScript-escape encoded.|\n| **`email`** | **Optional**. <br/>The contact email address (in valid email format) of the business. The maximum character limit for the string is 128 characters.|\n| **`websites`** | **Optional**. <br/>The URLs associated with the business. For instance, a website, Facebook Page, or Instagram. You must include the http:// or https:// portion of the URL. There is a maximum of 2 websites with a maximum of 256 characters each. |\n| **`profile_picture_handle`** | **Optional**. <br/>The handle of the profile picture generated from a call to **Resumable Upload - Upload File Data**. For more information, see [Resumable Upload API](https://developers.facebook.com/docs/graph-api/guides/upload).|\n\n## Delete Business Profile\n\nTo delete your business profile, you must [delete your phone number](https://developers.facebook.com/docs/whatsapp/phone-numbers#delete-phone-number-from-a-business-account)."
        							},
        							"response": [
        								{
        									"name": "Update Business Profile",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"address\": \"<business-address>\",\n    \"description\": \"<business-description>\",\n    \"vertical\": \"<business-industry>\",\n    \"about\": \"<profile-about-text>\",\n    \"email\": \"<business-email>\",\n    \"websites\": [\n        \"<https://website-1>\",\n        \"<https://website-2>\"\n    ],\n    \"profile_picture_handle\": \"4:VGVzdC5wbmc=:aW1hZ2UvanBlZw==:ARat4Mt-L09JON3f30SmDkdyPSuyBkDDYiB4TFXuXISjdgHoNp2b7j882_9Jzr2tPrdKxi92UygyVzTivJiOvmebMpZ6MIjTik3gTyI3ZCQAgQ:e:1659995302:2022308451254161:636685196:ARZf1ftR5N6-qSLtklU\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/whatsapp_business_profile",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"whatsapp_business_profile"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"data\": [\n        {\n            \"business_profile\": {\n                \"messaging_product\": \"whatsapp\",\n                \"address\": \"<business-address>\",\n                \"description\": \"<business-description>\",\n                \"vertical\": \"<business-industry>\",\n                \"about\": \"<profile-about-text>\",\n                \"email\": \"<business-email>\",\n                \"websites\": [\n                    \"https://website-1\",\n                    \"https://website-2\"\n                ],\n                \"profile_picture_url\": \"https://pps.whatsapp.net/...\",\n                \"id\": \"<business-profile-id>\"\n            },\n            \"id\": \"<phone-number-id>\"\n        }\n    ]\n}"
        								}
        							]
        						}
        					],
        					"description": "To complete the following API calls, you need to get a business profile ID. To do that, make a **GET** call to the **`/{{Phone-Number-ID}}`** endpoint and add `business_profile` as a URL field. Within the **`business_profile`** request, you can specify what you want to know from your business.\n\n#### Reminders\n\n* To use this endpoint, you need to authenticate yourself with a system user access token with the **`whatsapp_business_management`** permission."
        				},
        				{
        					"name": "Media",
        					"item": [
        						{
        							"name": "Upload Image",
        							"request": {
        								"method": "POST",
        								"header": [],
        								"body": {
        									"mode": "formdata",
        									"formdata": [
        										{
        											"key": "messaging_product",
        											"value": "whatsapp",
        											"type": "text"
        										},
        										{
        											"key": "file",
        											"type": "file",
        											"src": "/Users/Sample.jpg"
        										}
        									]
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"media"
        									]
        								},
        								"description": "This request uploads an image as .jpeg. The parameters are specified as **form-data** in the request **body**."
        							},
        							"response": [
        								{
        									"name": "Upload Image (form-data)",
        									"originalRequest": {
        										"method": "POST",
        										"header": [],
        										"body": {
        											"mode": "formdata",
        											"formdata": [
        												{
        													"key": "messaging_product",
        													"value": "whatsapp",
        													"type": "text"
        												},
        												{
        													"key": "file",
        													"type": "file",
        													"src": "/Users/Sample.jpg"
        												}
        											]
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"media"
        											]
        										}
        									},
        									"_postman_previewlanguage": "json",
        									"header": null,
        									"cookie": [],
        									"body": "{\n    \"id\": \"<MEDIA_ID>\"\n}"
        								},
        								{
        									"name": "Upload Image JSON",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \n    \"file\": \"@/local/path/file.jpg;type=image/jpeg\",\n    \"messaging_product\": \"whatsapp\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"media"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"id\": \"4490709327384033\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Upload Sticker",
        							"request": {
        								"method": "POST",
        								"header": [],
        								"body": {
        									"mode": "formdata",
        									"formdata": [
        										{
        											"key": "messaging_product",
        											"value": "whatsapp",
        											"type": "text"
        										},
        										{
        											"key": "file",
        											"type": "file",
        											"src": "/Users/sticker.webp"
        										}
        									]
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"media"
        									]
        								},
        								"description": "This request uploads a sticker as .webp. The parameters are specified as **form-data** in the request **body**."
        							},
        							"response": [
        								{
        									"name": "Upload Sticker File (form-data)",
        									"originalRequest": {
        										"method": "POST",
        										"header": [],
        										"body": {
        											"mode": "formdata",
        											"formdata": [
        												{
        													"key": "messaging_product",
        													"value": "whatsapp",
        													"type": "text"
        												},
        												{
        													"key": "file",
        													"type": "file",
        													"src": "/Users/sticker.webp"
        												}
        											]
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"media"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": null,
        									"cookie": [],
        									"body": "{\n    \"id\": \"<MEDIA_ID>\"\n}"
        								},
        								{
        									"name": "Upload Sticker File JSON",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n \n    \"file\": \"@/local/path/file.webp;type=webp\",\n    \"messaging_product\": \"whatsapp\"    \n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/media",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"media"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"id\": \"4490709327384033\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Retrieve Media URL",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Media-ID}}?phone_number_id=<PHONE_NUMBER_ID>",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Media-ID}}"
        									],
        									"query": [
        										{
        											"key": "phone_number_id",
        											"value": "<PHONE_NUMBER_ID>",
        											"description": "Specifies that this action only be performed if the media belongs to the provided phone number."
        										}
        									]
        								},
        								"description": "To retrieve your media’s URL, make a **GET** call to **`/{{Media-ID}}`**. Use the returned URL to download the media file. Note that clicking this URL (i.e. performing a generic GET) will not return the media; you must include an access token. For more information, see [Download Media](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media#download-media).\n\nYou can also use the optional query **`?phone_number_id`** for **`Retrieve Media URL`** and **`Delete Media`**. This parameter checks to make sure the media belongs to the phone number before retrieval or deletion.\n\n#### Response\n\nA successful response includes an object with a media URL. The URL is only valid for 5 minutes. To use this URL, see [Download Media](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media#download-media)."
        							},
        							"response": [
        								{
        									"name": "Retrieve Media URL",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Media-ID}}?phone_number_id=<PHONE_NUMBER_ID>",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Media-ID}}"
        											],
        											"query": [
        												{
        													"key": "phone_number_id",
        													"value": "<PHONE_NUMBER_ID>"
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": ""
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"url\": \"<URL>\",\n    \"mime_type\": \"image/jpeg\",\n    \"sha256\": \"<HASH>\",\n    \"file_size\": \"303833\",\n    \"id\": \"2621233374848975\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Delete Media",
        							"request": {
        								"method": "DELETE",
        								"header": [],
        								"body": {
        									"mode": "formdata",
        									"formdata": []
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Media-ID}}/?phone_number_id=<PHONE_NUMBER_ID>",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Media-ID}}",
        										""
        									],
        									"query": [
        										{
        											"key": "phone_number_id",
        											"value": "<PHONE_NUMBER_ID>",
        											"description": "Specifies that deletion of the media  only be performed if the media belongs to the provided phone number."
        										}
        									]
        								},
        								"description": "To delete media, make a **DELETE** call to the ID of the media you want to delete.\n\n## Prerequisites\n- [User Access Token](https://developers.facebook.com/docs/facebook-login/access-tokens#usertokens) with **`whatsapp_business_messaging`** permission\n- Media object ID from either uploading media endpoint or media message Webhooks\n\n## Request\n[Perform requests in Graph API Explorer](https://developers.facebook.com/tools/explorer/?method=DELETE&path=media_id&version=v8.0)"
        							},
        							"response": [
        								{
        									"name": "Delete Media",
        									"originalRequest": {
        										"method": "DELETE",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}"
        											}
        										],
        										"body": {
        											"mode": "formdata",
        											"formdata": []
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Media-ID}}?phone_number_id=<PHONE_NUMBER_ID>",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Media-ID}}"
        											],
        											"query": [
        												{
        													"key": "phone_number_id",
        													"value": "<PHONE_NUMBER_ID>"
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": ""
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Download Media",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Media-URL}}",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Media-URL}}"
        									]
        								},
        								"description": "When you retrieve a media URL through the **GET** Media endpoint, you must use a User Access Token to download media content from the URL. If you click the URL from a browser, you will get an access error. <br/>\n> **Note**: all media URLs will expire after 5 minutes, you need to retrieve the media URL again if it expires.\n\n## Prerequisites\n- [User Access Token](https://developers.facebook.com/docs/facebook-login/access-tokens#usertokens) with **`whatsapp_business_messaging`** permission\n- A media URL obtained from retrieving media url endpoint \n\n## Response:\nIf successful,  you receive the binary data of media saved in **`media_file`**, response headers contain a `content-type` header to indicate the mime type of returned data. For more information, see [Supported Media Types](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media#supported-media-types).\n\nIf media fails to download, you receive a **404 Not Found** response code. In that case, we recommend that you try to [Retrieve Media URL](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media#retrieve-media-url) and download again. If doing so doesn't resolve the issue, please try to renew the **`USER_ACCESS_TOKEN`** then retry downloading the media."
        							},
        							"response": [
        								{
        									"name": "Download Media",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Media-URL}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Media-URL}}"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "image/jpeg",
        											"description": "",
        											"type": "text"
        										},
        										{
        											"key": "media_file",
        											"value": "",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": ""
        								}
        							]
        						}
        					],
        					"description": "You can use the following endpoints to upload, retrieve, or delete media:\n\n| Endpoint       | Uses |\n| ----------- | ---------- |\n| [POST /{phone-number-ID}/media]() | Upload media. |\n| [GET /{media-ID}]() | Retrieve the URL for a specific media item. |\n| [DELETE /{media-ID}]() | Delete a specific media item. |\n| [GET /{media-URL}]() | Download media from a media URL. |\n\n#### Reminders\n\n* To use these endpoints, you need to authenticate yourself with a system user access token with the **`whatsapp_business_messaging`** permission.\n* If you need to find your phone number ID, see [Get Phone Number ID](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers).\n* If you need to find your media URL, see [Retrieve Media URL](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/media#download-media).\n\n#### Support Media Types\n| Media       | Supported File Type(s) | Size Limit |\n| ----------- | ----------------------- | ---------- |\n| `audio`       | <ul><li>`audio/aac`</li><li>`audio/mp4`</li><li>`audio/mpeg`</li><li>`audio/amr`</li><li>`audio/ogg`</li></ul><br> **Note**: only opus codecs, base audio/ogg is not supported | 16MB |\n| `document`    | <ul><li>`text/plain`</li><li>`application/pdf`</li><li>`application/vnd.ms-powerpoint`</li><li>`application/msword`</li><li>`application/vnd.ms-excel`</li><li>`application/vnd.openxmlformats-officedocument.wordprocessingml.document`</li><li>`application/vnd.openxmlformats-officedocument.presentationml.presentation`</li><li>`application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`</li></ul> | 100MB |\n| `image` | <ul><li>`image/jpeg`</li><li>`image/png`</li></ul> | 5MB |\n| `sticker` | <ul><li>`image/webp`</li></ul> | 100KB |\n| `video` | <ul><li>`video/mp4`</li><li>`video/3sp`</li></ul><br/>**Notes**:<ul><li>Only H.264 video codec and AAC audio codec is supported.</li><li>We support videos with a single audio stream or no audio stream.</li><ul> | 16MB |\n\n\n#### Get Media ID\nTo complete some of the following API calls, you need to have a media ID. There are two ways to get this ID:\n\n* **From the API call**: Once you have successfully uploaded media files to the API, the media ID is included in the response to your call.\n* **From Webhooks**: When a business account receives a media message, it downloads the media and uploads it to the Cloud API automatically. That event triggers the Webhooks and sends you a notification that includes the media ID."
        				},
        				{
        					"name": "Messages",
        					"item": [
        						{
        							"name": "Messages Object",
        							"item": [
        								{
        									"name": "Text Object",
        									"item": [],
        									"description": "A **`Text Object`** consists of the following fields and formatting options:\n\n#### Fields\n\n| Name           |  Description |\n|----------------|--------------|\n| **`body`**     | **Required for text messages**.<br/>The text of the text message that can contain URLs and supports formatting. To view available formatting options, see [Text Object Formatting Options](https://developers.facebook.com/docs/whatsapp/api/messages#formatting). <br/><br/>If you include URLs in your text **and** want to include a preview box in text messages (`\"preview_url\": true`), ensure it starts with `http://` or `https://`. You must include a hostname, since IP addresses are not matched.<br/><br/>Maximum length: 4096 characters. |\n|**`preview_url`**<br/>type: `Boolean`| **Optional**.<br/>By default, WhatsApp recognizes URLs and makes them clickable, but you can also include a preview box with more information about the link. Set this field to `true` if you want to include a URL preview box.<br/><br/>The majority of the time when you send a URL, whether with a preview or not, the receiver of the message will see a URL that they can click on.<br/><br/>URL previews are only rendered after one of the following has occurred:<ul><li>The business has sent a message template to the user.</li><li>The user initiates a conversation with a \"click to chat\" link.</li><li>The user adds the business phone number to their address book and initiates a conversation.</li></ul><br/>**Default**: `false`|"
        								},
        								{
        									"name": "Reaction Object",
        									"item": [],
        									"description": "The `Reaction Object` consists of a message ID and a emoji.\n\n#### Fields\n\n| Name            | Description         |\n|-----------------|---------------------|\n| **`message_id`**        | **Required**.<br/><br/>Specifies the WhatsApp message ID (WAMID) that this reaction is being sent to. <br/><br/> You cannot send a reaction to a **`message_id`** that previously sent or received reaction messages. |\n| **`emoji`**      | **Required**.<br/><br/>The emoji used for the reaction.<br/><br/>All emojis are supported, however only one emoji can be sent in a reaction message. Set this value to \"\" (empty string) to remove the reaction. <br/>Unicode is not supported. However, unicode values can be Java or JavaScript-escape encoded. |"
        								},
        								{
        									"name": "Media Object",
        									"item": [],
        									"description": "The `Media Object` consists of audio, document, image, sticker, and video objects.\n\n#### Fields\n\n| Name            | Description         |\n|-----------------|---------------------|\n| **`id`**        | **Required when type is an image, audio, document, sticker, or video and you are not using a link**.<br/>The media object ID. For more information, see [Get Media ID](#39a02bc0-ede1-4848-b24e-4ac3d501aaea). |\n| **`link`**      | **Required when type is audio, document, image, sticker, or video and you are not using an uploaded media ID.** <br/>The protocol and URL of the media to be sent. Use only with HTTP/HTTPS URLs. |\n| **`caption`** | **Optional**.<br/>Describes the specified image, document, or video. Do not use it with audio or sticker media.|\n|**`filename`** | **Optional**. <br/> Describes the filename for the specific document. Use only with document media.|"
        								},
        								{
        									"name": "Template Object",
        									"item": [],
        									"description": "The `Template Object` contains the following fields:\n\n#### Fields\n\n| Name             | Description              |\n|------------------|--------------------------|\n| **`name`**  |**Required**. <br/> The name of the template. |\n| **`language`**  |**Required**. <br/> Specifies a [language object](https://documenter.getpostman.com/view/13382743/UVC5FTHT?fbclid=IwAR083mCseNzJm-JgxlIQbdF30hkAbEOHkbBaw9bA7-isGKU6uwtq1RJKc0o#d9272e38-c3db-458c-a23b-07953abc73a4). Specifies the language the template may be rendered in.<br/><br/>Only the **`deterministic`** language policy works with media template messages.|\n| **`components`**  |**Optional**. <br/> An array of [components objects](https://documenter.getpostman.com/view/13382743/UVC5FTHT?fbclid=IwAR0V3m0B47q6rsaPsrwcWzb5FbNtQD7K8I1RulisTB4Mj-rB0AYiSkdc9lY#8225365a-acb8-48c7-8e57-079dfc532865) that contain the parameters of the message. |"
        								},
        								{
        									"name": "Language Object",
        									"item": [],
        									"description": "The `Language Object` contains the following fields:\n\n#### Fields\n\n| Name             | Description              |\n|------------------|--------------------------|\n| **`policy`**  |**Optional**. <br/> For more information, see [Language Policy Options](https://developers.facebook.com/docs/whatsapp/api/messages/message-templates#language-policy-options). <br/><br/> **Default** (and only supported value): `deterministic`|\n| **`code`**  |**Required**. <br/>The code of the language or locale to use. This field accepts both language (for example, `‘en’`) and language_locale (for example, `‘en_US’`) formats. <br/> For more information regarding all codes, see [Supported Languages](https://developers.facebook.com/docs/whatsapp/api/messages/message-templates#supported-languages). |"
        								},
        								{
        									"name": "Components Object",
        									"item": [],
        									"description": "The `Components Object` contains the following fields:\n\n#### Fields\n\n| Name             | Description              |\n|------------------|--------------------------|\n| **`type`**  |**Required**. <br/> Describes the component type. <br/><br/> **Values**: `header`, `body`, `button` <br/> For text-based templates, only `body` is supported. |\n| **`parameters`**  |**Required when type is `button`**. <br/> The namespace of the template. |\n| **`sub_type`**  |**Required when type is `button`. Not used for the other types.**<br/> The type of button to create. <br/><br/>**Values**: `quick_reply`, `url`|\n| **`index`**  |**Required when type is `button`. Not used for the other types.** <br/> The position index of the button. You can have up to 3 buttons using index values of 0 to 2. |"
        								},
        								{
        									"name": "Parameter Object",
        									"item": [],
        									"description": "The **`Parameter Object`** contains the following fields:\n\n#### Fields\n\n| Name             | Description              |\n|------------------|--------------------------|\n| **`type`**       |**Required**. <br/> Describes the parameter type. <br/><br/><br/>**Values**: `text`, `currency`, `date_time`, `image`, `document`<br/><br/>For text-based templates, the only supported parameter types are `text`, `currency`, and `date_time` |\n| **`text`**       |**Required when type=`text`**. <br/> The message of the text.<br/>For the `header component`, the character limit is 60 characters. <br/>For the `body component`, the character limit is 1024.<br/><br/>The exception to these character limits applies to template messages in the following conditions:<br/><ul><li>When sending a **`template`** message with a **`body`** component only, the character limit for the `text` parameter and the full template text is 32768 characters.</li><li>When sending a **`template`** message with **`body`** and other components, The character limit for the `text` parameter and the full template text is 1024 characters.</li><ul/>|\n| **`currency`**       |**Required when type is `currency`**. <br/> A [currency object](#424b70af-ced8-456d-b1e1-1360c5afb9e9).|\n| **`date_time`**       |**Required when type is `date_time`**. <br/> A [date_time object](#ec955b05-7bd4-4273-ad87-ae755b580f6e).|\n| **`image`**       |**Required when type is `image`**. <br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) of type image.|\n| **`document`**       |**Required when type is `document`**. <br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) of type document. <br/>Only PDF documents are supported for media-based message templates.|"
        								},
        								{
        									"name": "Currency Object",
        									"item": [],
        									"description": "The `Currency Object` contains the following fields:\n\n#### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`fallback_value`** |**Required**. <br/> The default text if localization fails. <br/> |\n| **`code`**           |**Required**. <br/> The currency code as defined in [ISO 4217](https://en.wikipedia.org/wiki/ISO_4217#Active_codes). <br/> |\n| **`amount_1000`**           |**Required**. <br/> The amount multiplied by 1000. <br/> |"
        								},
        								{
        									"name": "Date_Time Object",
        									"item": [],
        									"description": "The `Date_Time Object` contains the following fields:\n\n#### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`fallback_value`** | **Required**. <br/> The default text if localization fails. |\n| **`day_of_week`**    | **Optional**. <br/> If it is different from the value derived from the date (if specified), use the derived value. Both strings and numbers are accepted.<br/>Supported values: <ul><li>`\"MONDAY\"` or 1</li><li>`\"TUESDAY\"` or 2</li><li>`\"WEDNESDAY\"` or 3</li><li>`\"THURSDAY\"` or 4</li><li>`\"FRIDAY\"` or 5</li><li>`\"SATURDAY\"` or 6</li><li>`\"SUNDAY\"` or 7</li></ul> |\n| **`year`** | **Optional**. <br/> Specifies the year. |\n| **`month`** | **Optional**. <br/> Specifies the month. |\n| **`day_of_month`** | **Optional**. <br/> Specifies the day of the month. |\n| **`hour`** | **Optional**. <br/> Specifies the hour. |\n| **`minute`** | **Optional**. <br/> Specifies the minute. |\n| **`calendar`** | **Optional**. <br/> The type of calendar.<br/><br/>**Values**: `\"GREGORIAN\"` or `\"SOLAR_HIJRI\"`. |"
        								},
        								{
        									"name": "Button Parameter Object",
        									"item": [],
        									"description": "The `Button Parameter Object` contains the following fields:\n\n#### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`type`** | **Required**. <br/> Specifies the type of parameter for the button. <br/><br/>**Values**: `payload`,`text`|\n| **`payload`** | **Required for `quick_reply` buttons**. <br/> Developer-defined payload that is returned when the button is clicked in addition to the display text on the button.<br/><br/>For more information on usage, see [Callback from a Quick Reply Button Click](#eb99d8fb-170a-4284-b7da-454484a0333a).|\n| **`text`** | **Required for url buttons**. <br/> Developer-provided suffix that is appended to the predefined prefix URL in the template.|"
        								},
        								{
        									"name": "Contacts Object",
        									"item": [
        										{
        											"name": "addresses Object",
        											"item": [],
        											"description": "| Name | Description |\n| --- | --- |\n| **`street`** | **Optional**.  <br>Steet number and name. |\n| **`city`** | **Optional**.  <br>The name of the city. |\n| **`state`** | **Optional**.  <br>The abbreviation name of the state. |\n| **`zip`** | **Optional**.  <br>The ZIP code. |\n| **`country`** | **Optional**.  <br>The full name of the country. |\n| **`country_code`** | **Optional**.  <br>The two-letter country abbreviation. |\n| **`type`** | **Optional**.  <br>Standard values: `HOME`, `WORK`. |"
        										},
        										{
        											"name": "emails Object",
        											"item": [],
        											"description": "| Name | Description |\n| --- | --- |\n| **`email`** | **Optional**.  <br>Email address. |\n| **`type`** | **Optional**.  <br>Standard Values: `HOME`, `WORK` |"
        										},
        										{
        											"name": "name Object",
        											"item": [],
        											"description": "| Name | Description |\n|---|---|\n| **`formatted_name`** | **Required.**<br>Full name, as it normally appears. |\n| **`first_name`** | **Optional**.<br>First name.|\n| **`last_name`** | **Optional**.<br>Last name. |\n| **`middle_name`** | **Optional**.<br>Middle name. |\n| **`suffix`** | **Optional**.<br>Name suffix.|\n| **`prefix`**| **Optional**.<br>Name prefix. |\n\n* At least one of the optional parameters needs to be included along with the **`formatted_name`** parameter."
        										},
        										{
        											"name": "org Object",
        											"item": [],
        											"description": "| Name | Description |\n| --- | --- |\n| **`company`** | **Optional.**  <br>Name of the contact's company. |\n| **`department`** | **Optional.**  <br>Name of the contact's department. |\n| **`title`** | **Optional.**  <br>The contact's business title. |"
        										},
        										{
        											"name": "phone Object",
        											"item": [],
        											"description": "| Name | Description |\n| --- | --- |\n| **`phone`** | **Optional.**  <br>Automatically populated with the **`wa_id`** value as a formatted phone number. |\n| **`type`** | **Optional.**  <br>Standard Values: `CELL`, `MAIN`, `IPHONE`, `HOME`, `WORK` |\n| **`wa_id`** | **Optional.**  <br>WhatsApp ID. |"
        										},
        										{
        											"name": "urls Object",
        											"item": [],
        											"description": "| Name | Description |\n| --- | --- |\n| **`url`** | **Optional.**  <br>The URL. |\n| **`type`** | **Optional.**  <br>Standard Values: `HOME`, `WORK` |"
        										}
        									],
        									"description": "The `Contacts Object` contains the following fields:\n\n#### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`addresses`** | **Optional**. <br/> Specifies an array of address objects. For more information, see [address object](https://developers.facebook.com/docs/whatsapp/api/messages#addresses-object).|\n| **`birthday`** | **Optional**. <br/> A **YYYY-MM-DD** formatted string.|\n| **`emails`** | **Optional**. <br/> Specifies an array of email objects. For more information, see [emails object](https://developers.facebook.com/docs/whatsapp/api/messages#emails-object).|\n| **`name`** | **Required**. <br/> Specifies the name object. For more information, see [name object](https://developers.facebook.com/docs/whatsapp/api/messages#name-object).|\n| **`org`** | **Optional**. <br/> Specifies the org object. For more information, see [org object](https://developers.facebook.com/docs/whatsapp/api/messages#org-object).|\n| **`phones`** | **Optional**. <br/> Specifies an array of phone objects. For more information, see [phone object](https://developers.facebook.com/docs/whatsapp/api/messages#phone-object).|\n| **`urls`** | **Optional**. <br/> Specifies an array of url objects. For more information, see [url object](https://developers.facebook.com/docs/whatsapp/api/messages#urls-object).|"
        								},
        								{
        									"name": "Location Object",
        									"item": [],
        									"description": "The `Location Object` contains the following fields:\n\n<h5>Fields</h5>\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`longitude`** | **Required**. <br/> The longitude of the location.|\n| **`latitude`** | **Required**. <br/> The latitude of the location.|\n| **`name`** | **Optional**. <br/> The name of the location.|\n| **`address`** | **Optional**. <br/> The address of the location. This field is only displayed if **`name`** is present.|"
        								},
        								{
        									"name": "Interactive Object",
        									"item": [
        										{
        											"name": "Header Object",
        											"item": [],
        											"description": "##### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`type`**      | **Required**.<br/>The header type you would like to use. Supported values are:<ul><li>`text`: Used for List Messages, Reply Buttons, and Multi-Product Messages.</li><li>`video`: Used for Reply Buttons.</li><li>`image`: Used for Reply Buttons.</li><li>`document`: Used for Reply Buttons.</li></ul>|\n| **`text`**      | **Required if `type` is set to `text`**.<br/>The text for the header. Formatting allows emojis, but not markdown.<br/><br/>Maximum length: 60 characters.|\n| **`video`**      | **Required if `type` is set to `video`**.<br/>Contains the **`media`** object for this video.|\n| **`image`**      | **Required if `type` is set to `image`**.<br/>Contains the **`media`** object for this image.|\n| **`document`**      | **Required if `type` is set to `document`**.<br/>Contains the **`media`** object for this document.|"
        										},
        										{
        											"name": "Body Object",
        											"item": [],
        											"description": "##### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`text`**      | **Required**.<br/>The body content of the message. Emojis and markdown are supported. Links are supported.<br/><br/>Maximum length: 1024 characters.|"
        										},
        										{
        											"name": "Footer Object",
        											"item": [],
        											"description": "##### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`text`**      | **Required if the `footer` object is present**.<br/>The footer content of the message. Emojis and markdown are supported. Links are supported.<br/><br/>Maximum length: 60 characters.|"
        										},
        										{
        											"name": "Action Object",
        											"item": [],
        											"description": "##### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`button`**      | **Required for all List Messages**.<br/>The Button content. It cannot be an empty string and must be unique within the message. Does not allow emojis or markdown.<br/><br/>Maximum length: 20 characters.|\n| **`buttons`**      | **Required for Reply Button**.<br/>A button can contain the following parameters:<ul><li>**`type`**: only supported if **`type`**=**`reply`**(for Reply Button)</li><li>**`title`**: The Button title. It cannot be an empty string and must be unique within the message. Does not allow emojis or markdown. Maximum length: 20 characters.</li><li>**`id`**: Unique identifier for your button. This ID is returned in the Webhook when the button is clicked by the user. Maximum length: 256 characters.</li></ul><br/>You can have a maximum of 3 buttons.|\n| **`sections`**      | **Required for List Messages and Multi-Product Messages**.<br/>The array of **`section`** objects. There is a minimum of 1 and maximum of 10. For more information, see [section object](#).|\n| **`catalog_id`**  |  **Required for Single Product and Multi-Product messages.**<br/><br/>Unique identifier of the Facebook catalog linked to your WhatsApp Business Account. **`catalog_id`** can be retrieved through [Commerce Manager](https://business.facebook.com/commerce/). |\n| **`product_retailer_id`**  | **Required for Single Product and Multi-Product messages.**<br/><br/> The unique identifier of the product in the catalog.<br/><br/>To get the **`product_retailer_id`**, go to [Commerce Manager](https://business.facebook.com/commerce/), select your Facebook Business account, and you will see a list of shops connected to your account. Click the shop you want to use. On the left-side panel, click **Catalog** > **Items**, and find the item you want to mention. The ID for that item is displayed under the item's name.     |"
        										},
        										{
        											"name": "Section Object",
        											"item": [],
        											"description": "##### Fields\n\n| Name | Description |\n| --- | --- |\n| **`title`** | **Required if the message has more than one** **`section`**.  <br/>The title of the section.  <br/>  <br/>Maximum length: 24 characters. |\n| **`rows`** | **Required for List Messages**.  <br/Contains a list of rows. You can have a maximum of 10 rows across your sections.  <br/><br/>Each row must have a **`title`** (Maximum length: 24 characters) and an **`ID`** (Maximum length: 200 characters). You can add a **`description`** (Maximum length: 72 characters), but it is optional. |\n| **`product_items`** | **Required for Multi-Product Messages**.<br/><br/>Specifies an array of `product` objects.<br/>There is a minimum of 1 product per section and a maximum of 30 products across all sections.<br/><br/>Each `product` object contains the following field:<br/>**`product_retailer_id`** – **Required for Multi-Product Messages**. Specifies the unique identifier of the product in a catalog. To get this ID, go to [Commerce Manager](https://business.facebook.com/commerce/), select your account and the shop you want to use. Then, click **Catalog** > **Items**, and find the item you want to mention. The ID for that item is displayed under the item's name.\n\n\n##### **`Rows`** Example\n\n``` json\n\"rows\": [\n  {\n   \"id\":\"unique-row-identifier-here\",\n   \"title\": \"row-title-content-here\",\n   \"description\": \"row-description-content-here\",\n   }\n]\n\n```"
        										}
        									],
        									"description": "The **`Interactive Object`** contains four main components: **`header`**, **`body`**, **`footer`**, and **`action`**. Additionally, some of those components can contain one or more different objects:\n\n* Inside **`header`**, you can nest **`media`** objects.\n* Inside **`action`**, you can nest **`section`** and **`button`** objects.\n\n#### Fields\n\n| Name                 | Description              |\n|----------------------|--------------------------|\n| **`type`**      | **Required**.<br/>The type of interactive message you want to send. Supported values:<ul><li>`list`: Use it for List Messages.</li><li>`button`: Use it for Reply Buttons.</li><li>`product`: Use this for Single Product Messages</li><li>`product_list`: Use this for Multi-Product Messages</li></ul>|\n| **`header`**      | **Required for type `product_list`**. **Optional for other types**.<br/>This contains the header content displayed on top of a message. You cannot set a **`header`** if your **`interactive`** object is type `product`.<br/><br/>The **`header`** object contains the following fields:<ul><li>`document`: **Required** if **`type`** is set to `document`. Contains the **`media`** object with the document.</li><li>`image`: **Required** if **`type`** is set to `image`. Contains the **`media`** object with the image.</li><li>`video`: **Required** if **`type`** is set to `video`. Contains the **`media`** object with the video.</li><li>`text`: **Required** if **`type`** is set to `text`. The text for the header. Formatting allows emojis, but not markdown. Maximum length: 60 characters.</li></ul><br/><br/>Supported interactive message type by header type:<ul><li>`text` - for List Messages, Reply Buttons, and Multi-Product Messages</li><li>`video` - for Reply Buttons.</li><li>`image`: for Reply Buttons.</li><li>`document` - For Reply Buttons.</li></ul>|\n| **`body`**      |  **Optional** for type `product`. **Required** for all other message types.<br/>The body of the message. The **`text`** field for the **`body`** object supports Emojis and markdown.<br/><br/>Maximum length: 1024 characters.|\n| **`footer`**      | **Optional**.<br/>An object with the footer of the message.Emojis and markdown are supported.<br/><br/>Maximum length: 60 characters.|\n| **`action`**      | **Required**.<br/>The action you want the user to perform after reading the message.|"
        								}
        							],
        							"description": "To send a message, you must first assemble a message object with the content you want to send. The **`Message Object`** contains the following fields used to create a message object:\n\n#### Fields\n\n| Name | Description |\n|------|----------|\n|**`messaging_product`** | **Required**. <br/> Messaging service used for the request. Always use `\"whatsapp\"`.|\n|**`recipient_type`** | **Optional**. <br/> Currently, you can only send messages to individuals. Set this value to `\"individual\"`. <br/><br/>**Default**: `individual` |\n|**`to`** | **Required**. <br/> WhatsApp ID or phone number for the person you want to send a message to.<br></br>See [Phone Numbers, Formatting](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#formatting) for more information. |\n|**`context`** | **Optional**. Only used for Cloud API. <br/>Used to mention a specific message you are replying to. The reply can be any message type currently supported by the Cloud API. |\n|**`type`** | **Optional**. <br/>The type of message you want to send. <br/><br/>The supported options for beta users are: <ul><li>**text**: for text messages.</li><li>**template**: for template messages. Only text-based templates are supported.</li><li>**document**: for document messages.</li><li>**image**: for image messages.</li><li>**interactive**: for list and reply button messages.</li><li>**audio**: for audio messages.</li><li>**contacts**: for contacts messages.</li><li>**location**: for location messages.</li><li>**sticker**: for sticker messages.</li><li>**video**: for video messages.</li></ul><br/>**Default**: `text`|\n|**`text`** | **Required for text messages**.<br/> A [text object](#fa59d67b-dc6f-446a-a0fd-f97537afbd2e). |\n|**`audio`** | **Required when `type` is set to `audio`**.<br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) containing audio. |\n|**`contact`** | **Required when `type` is set to `contacts`**.<br/> A [contacts object](#5bb1e554-402c-4278-bce4-657a9c4dc12f).|\n|**`document`** | **Required when `type` is set to `document`**.<br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) containing a document.  |\n|**`image`** | **Required when `type` is set to `image`**.<br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) containing an image.  |\n|**`interactive`** | **Required when `type` is set to `interactive`**.<br/> An [interactive object](#68fe0550-aba5-4ee3-b79d-0846f3dddef1). This option is used to send List Messages and Reply Buttons.<br/><br/>The components of each interactive object generally follow a consistent pattern: **`header`**, **`body`**, **`footer`**, and **`action`**. |\n|**`location`** | **Required when type `type` is set to `location`**.<br/> A [location object](#2ad90ff7-6fef-40a8-96cf-83f1144763c1).  |\n|**`reaction`** | **Required when type `type` is set to `reaction`**. <br/> A [reaction object](). |\n|**`sticker`** | **Required when `type` is set to `sticker`**.<br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) containing a sticker. Currently, we only support third-party static stickers. Static stickers must be 512x512 pixels and cannot exceed 100 KB. Animated stickers must be 512x512 pixels and cannot exceed 500 KB.|\n|**`video`** | **Required when `type` is set to `video`**.<br/> A [media object](#77f64012-481d-45d8-855f-e1620c6b2a5e) containing a video.  |\n|**`template`** | **Required when type is set to `template`**.<br/> A [template object](#fb5ad9b7-7991-443a-a1b5-97fdc5731673).  |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Send Text Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",    \n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"text\",\n    \"text\": {\n        \"preview_url\": false,\n        \"body\": \"text-message-content\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a text message, make a **POST** call to **`/{{{Phone-Number-ID}}/messages`** and attach a message object with `type = \"text\"`. Then, add a `text object`.\n\nFor more information about the `Text Object` structure, see [Text object](#fa59d67b-dc6f-446a-a0fd-f97537afbd2e).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Text Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",   \n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"text\",\n    \"text\": {\n        \"preview_url\": false,\n        \"body\": \"text-message-content\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Text Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"text\",\n    \"text\": {\n        \"preview_url\": false,\n        \"body\": \"<TEXT_MSG_CONTENT>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply-to text message, make a **POST** call to **`/{{{Phone-Number-ID}}/messages`** and use the **`context.message_id`** field. \n\nContext field is used for specifying to which previous message (**`context.message_id`**) you are replying to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message being replied to. |\n\nFor more information about the `Text Object` structure, see [Text object](#fa59d67b-dc6f-446a-a0fd-f97537afbd2e).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Text Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"text\",\n    \"text\": {\n        \"preview_url\": false,\n        \"body\": \"<TEXT_MSG_CONTENT>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Text Message with Preview URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"text\": {\n        \"preview_url\": true,\n        \"body\": \"Please visit https://youtu.be/hpltvTEiRrY to inspire your day!\"\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"response": [
        								{
        									"name": "Send Text Message with Preview URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"text\": {\n        \"preview_url\": true,\n        \"body\": \"Please visit https://youtu.be/hpltvTEiRrY.\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n  \"messaging_product\": \"whatsapp\",\n  \"contacts\": [{\n      \"input\": \"15555551234\",\n      \"wa_id\": \"<WHATSAPP_ID>\"\n    }],\n  \"messages\": [{\n      \"id\": \"wamid.ID\"\n    }]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply with Reaction Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"reaction\",\n    \"reaction\": {\n        \"message_id\": \"<WAM_ID>\",\n        \"emoji\": \"<EMOJI>\"\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send reaction messages, make a **POST** call to `/PHONE_NUMBER_ID/messages` and attach a message object with `type = reaction`. Then, add a **`reaction`** object.\n\n\n### Parameters\n\n| Name | Description |\n| --- | --- |\n| **`messaging_product`** | **Required**.<br/>Always set this value to `\"whatsapp\"`. |\n| **`recipient_type`** | **Optional**.<br/> Currently, you can only send messages to individuals. Set this value to `\"individual\"`. <br/><br/>**Default**: `individual` |\n| **`to`** | **Required**. <br/> WhatsApp ID or phone number for the person you want to send a message to.<br></br>See [Phone Numbers, Formatting](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#formatting) for more information. |\n| **`type`** | **Required**.<br/> To send a reply with a reaction, set this string value to `\"reaction\"`. |\n| **`reaction`** | **Required**. <br/> The [reaction object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#reaction-object) you are using for your reply. |\n\nIf the previous message is more than 30 days old or doesn't correspond to any message in the conversation, the reaction message will not be delivered and you will receive a Webhook with the code `131009`."
        							},
        							"response": [
        								{
        									"name": "Send Reply with Reaction Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"reaction\",\n    \"reaction\": {\n        \"message_id\": \"wamid.HBgLM...\",\n        \"emoji\": \"\\uD83D\\uDE00\"\n    }\n}\n",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v14.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v14.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"<PHONE_NUMBER>\",\n            \"wa_id\": \"<WHATSAPP_ID>\"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.HBgLM...\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Image Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"image\",\n    \"image\": {\n        \"id\": \"<IMAGE_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply to an image message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `image`. Then add a corresponding `image object`.\n\nSend an image message to your customers using link or media ID uploaded from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Image Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"image\",\n    \"image\": {\n        \"id\": \"<IMAGE_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}\n"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Image Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"image\",\n    \"image\": {\n        \"id\": \"<IMAGE_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply-to image message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `image`. Add the `image.id` of the image. \n\nAssign **`context.message_id`** the message id of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message being replied to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"image\".|\n| **`image.id`** | Specifies the ID of the image you are replying to. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Image Message by ID",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"image\",\n    \"image\": {\n        \"id\": \"<IMAGE_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Image Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"image\",\n    \"image\": {\n        \"link\": \"http(s)://image-url\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `image`. Then, be sure to include the link to the image.\n\nSend an audio message to your customers using a link to an image file.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object)."
        							},
        							"response": [
        								{
        									"name": "Send Image Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"image\",\n    \"image\": {\n        \"link\": \"http(s)://image-url\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Image Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"image\",\n    \"image\": {\n        \"link\": \"http(s)://image-url\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send an image link as a reply to message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `image`. Then, be sure to include the link to the image.\n\nAssign **`context.message_id`** the message ID of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message being replied to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"image\".|\n| **`image.link`** | Specifies the link of the image you are using for your reply. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Image Message by URL",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"image\",\n    \"image\": {\n        \"link\": \"http(s)://image-url\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Audio Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"audio\",\n    \"audio\": {\n        \"id\": \"<AUDIO_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `audio`. Then, add the corresponding `audio object` ID.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend an audio message to your customers using the media ID uploaded from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Audio Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"audio\",\n    \"audio\": {\n        \"id\": \"<AUDIO_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Audio Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"audio\",\n    \"audio\": {\n        \"id\": \"<AUDIO_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `audio`. Then, add the corresponding `audio object` ID.\n\nAssign **`context.message_id`** the message id of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message being replied to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"audio\".|\n| **`audio.id`** | Specifies the ID of the media object you are replying to. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Audio Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"audio\",\n    \"audio\": {\n        \"id\": \"<AUDIO_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Audio Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"audio\",\n    \"audio\": {\n        \"link\": \"http(s)://audio-url\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `audio`. Be sure to include the link to the audio.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend an audio message to your customers using link from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Audio Message By URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"audio\",\n    \"audio\": {\n        \"link\": \"<http(s)://audio-url>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Audio Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"audio\",\n    \"audio\": {\n        \"link\": \"http(s)://audio-url\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send an audio link as a reply to message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `image`. Then, be sure to include the link to the audio.\n\nAssign **`context.message_id`** the message ID of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"audio\".|\n| **`audio.link`** | Specifies the link of the image you are using for your reply. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Audio Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"audio\",\n    \"audio\": {\n        \"link\": \"http(s)://audio-url\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Document Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"document\",\n    \"document\": {\n        \"id\": \"<DOCUMENT_OBJECT_ID>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TO_SEND>\",\n        \"filename\": \"<DOCUMENT_FILENAME>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `document`. Then, add the corresponding `document object` ID.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a document message to your customers using media ID uploaded from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Document Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"document\",\n    \"document\": {\n        \"id\": \"<DOCUMENT_OBJECT_ID>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TO_SEND>\",\n        \"filename\": \"<DOCUMENT_FILENAME>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Document Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"document\",\n    \"document\": {\n        \"id\": \"<DOCUMENT_OBJECT_ID>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TO_SEND>\",\n        \"filename\": \"<DOCUMENT_FILENAME>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply to document message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `document`. Then, add the corresponding `document object` ID.\n\nAssign **`context.message_id`** the message id of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message being replied to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"document\".|\n| **`document.id`** | Specifies the ID of the document object you are replying to. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a document message to your customers using media ID uploaded from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Document Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"document\",\n    \"document\": {\n        \"id\": \"<DOCUMENT_OBJECT_ID>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TO_SEND>\",\n        \"filename\": \"<DOCUMENT_FILENAME>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Document Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"document\",\n    \"document\": {\n        \"link\": \"<http(s)://document-url>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TEXT>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `document`. Then, include the link to the document.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a document message to your customers using link.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Document Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"document\",\n    \"document\": {\n        \"link\": \"<http(s)://document-url>\",\n        \"caption\": \"<DOCUMENT-CAPTION-TEXT>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Document Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"document\",\n    \"document\": {\n        \"link\": \"<http(s)://document-url>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TEXT>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a document link as a reply to message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `document`. Then, be sure to include the link to the document.\n\nAssign **`context.message_id`** the message ID of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"document\".|\n| **`document.link`** | Specifies the link of the document you are sending for your reply. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Document Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"document\",\n    \"document\": {\n        \"link\": \"<http(s)://document-url>\",\n        \"caption\": \"<DOCUMENT_CAPTION_TEXT>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Sticker Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"id\": \"<MEDIA_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `sticker`. Then, add the corresponding **`sticker`** object ID.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a sticker message to your customers using the media ID uploaded from media endpoint.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Always use `\"whatsapp\"`.\n| **`type`** | **Required**<br/> Specifies the media type. Use `\"sticker\"`.|\n| **`sticker.id`** | Specifies the ID of the sticker object you are sending. |\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Sticker Message By ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"id\": \"<MEDIA_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Sticker Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"id\": \"<MEDIA_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply to sticker message, make a **POST** call to **/{{Phone-Number-ID}}/messages** and attach a media object with type = sticker. Then, add the corresponding sticker object ID.\n\nAssign **`context.message_id`** the message id of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use `\"sticker\"`.|\n| **`sticker.id`** | Specifies the ID of the sticker object you are sending. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Sticker Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"id\": \"<MEDIA_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Sticker Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"link\": \"<http(s)://sticker-url>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `sticker`. Then, add a link to the corresponding `sticker` file.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a sticker message to your customers using link.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Sticker Message By URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"link\": \"<http(s)://sticker-url>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Sticker Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"link\": \"<http(s)://sticker-url>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a sticker link as a reply to message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `sticker`. Then, be sure to include the link to the sticker.\n\nAssign **`context.message_id`** the message ID of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"sticker\".|\n| **`sticker.link`** | Specifies the link of the sticker you are sending for your reply. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Sticker Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"sticker\",\n    \"sticker\": {\n        \"link\": \"<http(s)://sticker-url>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Video Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"video\",\n    \"video\": {\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\",\n        \"id\": \"<VIDEO_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `video`. Then, add a corresponding `video object` ID.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a video message to your customers using the media ID uploaded from media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Video Message By ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{AdminAuthToken}}"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"video\",\n    \"video\": {\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\",\n        \"id\": \"<VIDEO_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Video Message by ID",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"video\",\n    \"video\": {\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\",\n        \"id\": \"<VIDEO_OBJECT_ID>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a reply to video message, make a **POST** call to **/{{Phone-Number-ID}}/messages** and attach a media object with type = \"video\". Then, add the corresponding video object ID.\n\nAssign **`context.message_id`** the message id of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"video\".|\n| **`video.id`** | Specifies the ID of the video object you are sending. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSends a video message as a reply to using the media ID uploaded from the /media endpoint.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Video Message by ID",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"video\",\n    \"video\": {\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\",\n        \"id\": \"<VIDEO_OBJECT_ID>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Video Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"video\",\n    \"video\": {\n        \"link\": \"<http(s)://video-url>\",\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a media message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `video`. Then, add a link to the video.\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\nSend a video message to your customers using a link.\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Video Message By URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{AdminAuthToken}}"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"video\",\n    \"video\": {\n        \"link\": \"<http(s)://video-url>\",\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Video Message by URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"video\",\n    \"video\": {\n        \"link\": \"<http(s)://video-url>\",\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a video link as a reply to message, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a media object with type = `video`. Then, be sure to include the link to the video.\n\nAssign **`context.message_id`** the message ID of the message you want to reply to.\n\n## Request Parameters\n\n| Parameter | Description |\n|---|---|\n| **`messaging_product`** | **Required**<br/>Messaging service used for the request. Use `\"whatsapp\"`.\n| **`context.message_id`** | **Required for context object**<br/>Specifies the ID of the message you are replying to. |\n| **`type`** | **Required**<br/> Specifies the media type. Use \"video\".|\n| **`video.link`** | Specifies the link to the video you are sending for your reply. |\n\nFor more information about the `Media Object` structure, see [Media Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#media-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**. Use the ID listed after **`wamid`** to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Video Message by URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_MSG_YOU_ARE_REPLYING_TO>\"\n    },\n    \"type\": \"video\",\n    \"video\": {\n        \"link\": \"<http(s)://video-url>\",\n        \"caption\": \"<VIDEO_CAPTION_TEXT>\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Contact Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"contacts\",\n    \"contacts\": [\n        {\n            \"addresses\": [\n                {\n                    \"street\": \"<ADDRESS_STREET>\",\n                    \"city\": \"<ADDRESS_CITY>\",\n                    \"state\": \"<ADDRESS_STATE>\",\n                    \"zip\": \"<ADDRESS_ZIP>\",\n                    \"country\": \"<ADDRESS_COUNTRY>\",\n                    \"country_code\": \"<ADDRESS_COUNTRY_CODE>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ],\n            \"birthday\": \"<CONTACT_BIRTHDAY>\",\n            \"emails\": [\n                {\n                    \"email\": \"<CONTACT_EMAIL>\",\n                    \"type\": \"<WORK|HOME>\"\n                }\n            ],\n            \"name\": {\n                \"formatted_name\": \"<CONTACT_FORMATTED_NAME>\",\n                \"first_name\": \"<CONTACT_FIRST_NAME>\",\n                \"last_name\": \"<CONTACT_LAST_NAME>\",\n                \"middle_name\": \"<CONTACT_MIDDLE_NAME>\",\n                \"suffix\": \"<CONTACT_SUFFIX>\",\n                \"prefix\": \"<CONTACT_PREFIX>\"\n            },\n            \"org\": {\n                \"company\": \"<CONTACT_ORG_COMPANY>\",\n                \"department\": \"<CONTACT_ORG_DEPARTMENT>\",\n                \"title\": \"<CONTACT_ORG_TITLE>\"\n            },\n            \"phones\": [\n                {\n                    \"phone\": \"<CONTACT_PHONE>\",\n                    \"wa_id\": \"<CONTACT_WA_ID>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ],\n            \"urls\": [\n                {\n                    \"url\": \"<CONTACT_URL>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ]\n        }\n    ]\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send contact messages, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type: **`contact`**. Then, add a [contacts object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#contacts-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Contact Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"contacts\",\n    \"contacts\": [\n        {\n            \"addresses\": [\n                {\n                    \"street\": \"1 Hacker Way\",\n                    \"city\": \"Menlo Park\",\n                    \"state\": \"CA\",\n                    \"zip\": \"94025\",\n                    \"country\": \"United States\",\n                    \"country_code\": \"us\",\n                    \"type\": \"HOME\"\n                },\n                {\n                    \"street\": \"200 Jefferson Dr\",\n                    \"city\": \"Menlo Park\",\n                    \"state\": \"CA\",\n                    \"zip\": \"94025\",\n                    \"country\": \"United States\",\n                    \"country_code\": \"us\",\n                    \"type\": \"WORK\"\n                }\n            ],\n            \"birthday\": \"2012-08-18\",\n            \"emails\": [\n                {\n                    \"email\": \"test@fb.com\",\n                    \"type\": \"WORK\"\n                },\n                {\n                    \"email\": \"test@whatsapp.com\",\n                    \"type\": \"HOME\"\n                }\n            ],\n            \"name\": {\n                \"formatted_name\": \"John Smith\",\n                \"first_name\": \"John\",\n                \"last_name\": \"Smith\",\n                \"middle_name\": \"D.\",\n                \"suffix\": \"Jr\",\n                \"prefix\": \"Dr\"\n            },\n            \"org\": {\n                \"company\": \"WhatsApp\",\n                \"department\": \"Design\",\n                \"title\": \"Manager\"\n            },\n            \"phones\": [\n                {\n                    \"phone\": \"+1 (940) 555-1234\",\n                    \"type\": \"HOME\"\n                },\n                {\n                    \"phone\": \"+1 (650) 555-1234\",\n                    \"type\": \"WORK\",\n                    \"wa_id\": \"16505551234\"\n                }\n            ],\n            \"urls\": [\n                {\n                    \"url\": \"https://www.facebook.com\",\n                    \"type\": \"WORK\"\n                },\n                {\n                    \"url\": \"https://www.whatsapp.com\",\n                    \"type\": \"HOME\"\n                }\n            ]\n        }\n    ]\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Contact Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"contacts\",\n    \"contacts\": [\n        {\n            \"addresses\": [\n                {\n                    \"street\": \"<ADDRESS_STREET>\",\n                    \"city\": \"<ADDRESS_CITY>\",\n                    \"state\": \"<ADDRESS_STATE>\",\n                    \"zip\": \"<ADDRESS_ZIP>\",\n                    \"country\": \"<ADDRESS_COUNTRY>\",\n                    \"country_code\": \"<ADDRESS_COUNTRY_CODE>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ],\n            \"birthday\": \"<CONTACT_BIRTHDAY>\",\n            \"emails\": [\n                {\n                    \"email\": \"<CONTACT_EMAIL>\",\n                    \"type\": \"<WORK|HOME>\"\n                }\n            ],\n            \"name\": {\n                \"formatted_name\": \"<CONTACT_FORMATTED_NAME>\",\n                \"first_name\": \"<CONTACT_FIRST_NAME>\",\n                \"last_name\": \"<CONTACT_LAST_NAME>\",\n                \"middle_name\": \"<CONTACT_MIDDLE_NAME>\",\n                \"suffix\": \"<CONTACT_SUFFIX>\",\n                \"prefix\": \"<CONTACT_PREFIX>\"\n            },\n            \"org\": {\n                \"company\": \"<CONTACT_ORG_COMPANY>\",\n                \"department\": \"<CONTACT_ORG_DEPARTMENT>\",\n                \"title\": \"<CONTACT_ORG_TITLE>\"\n            },\n            \"phones\": [\n                {\n                    \"phone\": \"<CONTACT_PHONE>\",\n                    \"wa_id\": \"<CONTACT_WA_ID>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ],\n            \"urls\": [\n                {\n                    \"url\": \"<CONTACT_URL>\",\n                    \"type\": \"<HOME|WORK>\"\n                }\n            ]\n        }\n    ]\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send contact messages, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type: **`contact`**. Then, add a [contacts object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#contacts-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Contact Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"contacts\",\n    \"contacts\": [\n        {\n            \"addresses\": [\n                {\n                    \"street\": \"1 Hacker Way\",\n                    \"city\": \"Menlo Park\",\n                    \"state\": \"CA\",\n                    \"zip\": \"94025\",\n                    \"country\": \"United States\",\n                    \"country_code\": \"us\",\n                    \"type\": \"HOME\"\n                },\n                {\n                    \"street\": \"200 Jefferson Dr\",\n                    \"city\": \"Menlo Park\",\n                    \"state\": \"CA\",\n                    \"zip\": \"94025\",\n                    \"country\": \"United States\",\n                    \"country_code\": \"us\",\n                    \"type\": \"WORK\"\n                }\n            ],\n            \"birthday\": \"2012-08-18\",\n            \"emails\": [\n                {\n                    \"email\": \"test@fb.com\",\n                    \"type\": \"WORK\"\n                },\n                {\n                    \"email\": \"test@whatsapp.com\",\n                    \"type\": \"HOME\"\n                }\n            ],\n            \"name\": {\n                \"formatted_name\": \"John Smith\",\n                \"first_name\": \"John\",\n                \"last_name\": \"Smith\",\n                \"middle_name\": \"D.\",\n                \"suffix\": \"Jr\",\n                \"prefix\": \"Dr\"\n            },\n            \"org\": {\n                \"company\": \"WhatsApp\",\n                \"department\": \"Design\",\n                \"title\": \"Manager\"\n            },\n            \"phones\": [\n                {\n                    \"phone\": \"+1 (940) 555-1234\",\n                    \"type\": \"HOME\"\n                },\n                {\n                    \"phone\": \"+1 (650) 555-1234\",\n                    \"type\": \"WORK\",\n                    \"wa_id\": \"16505551234\"\n                }\n            ],\n            \"urls\": [\n                {\n                    \"url\": \"https://www.facebook.com\",\n                    \"type\": \"WORK\"\n                },\n                {\n                    \"url\": \"https://www.whatsapp.com\",\n                    \"type\": \"HOME\"\n                }\n            ]\n        }\n    ]\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Location Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"location\",\n    \"location\": {\n        \"latitude\": \"<LOCATION_LATITUDE>\",\n        \"longitude\": \"<LOCATION_LONGITUDE>\",\n        \"name\": \"<LOCATION_NAME>\",\n        \"address\": \"<LOCATION_ADDRESS>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send location messages, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `location`. Then, add a location object.\n\nFor more information on the structure of `Location Object`, see [Location Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#location-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Location Messages",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"location\",\n    \"location\": {\n        \"latitude\": 37.758056,\n        \"longitude\": -122.425332,\n        \"name\": \"META HQ\",\n        \"address\": \"1 Hacker Way, Menlo Park, CA 94025\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to Location Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"location\",\n    \"location\": {\n        \"latitude\": \"<LOCATION_LATITUDE>\",\n        \"longitude\": \"<LOCATION_LONGITUDE>\",\n        \"name\": \"<LOCATION_NAME>\",\n        \"address\": \"<LOCATION_ADDRESS>\"\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send location messages, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `location`. Then, add a location object.\n\nFor more information on the structure of `Location Object`, see [Location Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#location-object).\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Reply to Location Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"location\",\n    \"location\": {\n        \"latitude\": 37.758056,\n        \"longitude\": -122.425332,\n        \"name\": \"META HQ\",\n        \"address\": \"1 Hacker Way, Menlo Park, CA 94025\"\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Message Template Text",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "You need to create a message template before you can send one. See [Create Template Using the WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation#step-1--create-template-using-the-whatsapp-manager) for more information. If your account is not verified yet, you can use one of our pre-approved templates.\n\nTo send a text-based message template, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `template`. Then, add a template object.\n\n## Prerequisites\n- You need to create a message template through [WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation) or [BM API](https://developers.facebook.com/docs/whatsapp/business-management-api/message-templates#creating-message-templates)\n- For an unverified account, we have already [pre-created message templates](https://www.facebook.com/business/help/722393685250070) for you to use.\n\n## Message Objects\n\nMessage templates can contain the following `Message Objects`:\n\n* [Template Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#template-object)\n* [Component Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#components-object)\n* [Parameter Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#parameter-object)\n* [Currency Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#currency-object)\n* [Date_Time Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#date-time-object)\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Message Template Text",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Message Template Media",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"header\",\n                \"parameters\": [\n                    {\n                        \"type\": \"image\",\n                        \"image\": {\n                            \"link\": \"http(s)://the-image-url\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "Media message templates allow you to send media content as a header component beyond text template with parameters in the body component. Before sending a message template, you need to create one. See [Create Template Using the WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation) for more information. If your account is not verified yet, you can use one of our pre-approved templates.\n\nTo send a media-based message template, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `template`. Then, add a template object.\n\n## Prerequisites\n\n- You need to create a message template through [WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation) or [BM API](https://developers.facebook.com/docs/whatsapp/business-management-api/message-templates#creating-message-templates)\n- For an unverified account, we have already [pre-created message templates](https://www.facebook.com/business/help/722393685250070) for you to use.\n\n## Message Objects\n\nMedia-Based message templates can contain the following `Message Objects`:\n\n* [Template Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#template-object)\n* [Component Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#components-object)\n* [Parameter Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#parameter-object)\n* [Currency Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#currency-object)\n* [Date_Time Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#date-time-object)\n\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Message Template Media",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"header\",\n                \"parameters\": [\n                    {\n                        \"type\": \"image\",\n                        \"image\": {\n                            \"link\": \"http(s)://the-image-url\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Message Template Interactive",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"header\",\n                \"parameters\": [\n                    {\n                        \"type\": \"image\",\n                        \"image\": {\n                            \"link\": \"http(s)://the-image-url\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"button\",\n                \"sub_type\": \"quick_reply\",\n                \"index\": \"0\",\n                \"parameters\": [\n                    {\n                        \"type\": \"payload\",\n                        \"payload\": \"aGlzIHRoaXMgaXMgY29v\"\n                    }\n                ]\n            },\n            {\n                \"type\": \"button\",\n                \"sub_type\": \"quick_reply\",\n                \"index\": \"1\",\n                \"parameters\": [\n                    {\n                        \"type\": \"payload\",\n                        \"payload\": \"9rwnB8RbYmPF5t2Mn09x4h\"\n                    }\n                ]\n            }\n        ]\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "Before sending a message template, you need to create one. See [Create Template Using the WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation) for more information. If your account is not verified yet, you can use one of our [pre-approved templates](https://www.facebook.com/business/help/722393685250070).\n\nTo send an interactive message template, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with `type=template`. Then, add a template object with your chosen button.\n\nInteractive message templates expand the content you can send recipients beyond the standard message template and media messages template types to include interactive buttons using the components object. There are two types of predefined buttons offered:\n\n* Call-to-Action — Allows your customer to call a phone number and visit a website.\n* Quick Reply — Allows your customer to return a simple text message.\n\nThese buttons can be attached to text messages or media messages. Once your interactive message templates have been created and approved, you can use them in notification messages as well as customer service/care messages.\n\n## Message Objects\n\nInteractive message templates can contain the following `Message Objects`:\n\n* [Template Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#template-object)\n* [Component Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#components-object)\n* [Parameter Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#parameter-object)\n* [Currency Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#currency-object)\n* [Date_Time Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#date-time-object)\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        							},
        							"response": [
        								{
        									"name": "Send Interactive Message Template",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"header\",\n                \"parameters\": [\n                    {\n                        \"type\": \"image\",\n                        \"image\": {\n                            \"link\": \"http(s)://the-image-url\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            },\n            {\n                \"type\": \"button\",\n                \"sub_type\": \"quick_reply\",\n                \"index\": \"0\",\n                \"parameters\": [\n                    {\n                        \"type\": \"payload\",\n                        \"payload\": \"aGlzIHRoaXMgaXMgY29v\"\n                    }\n                ]\n            },\n            {\n                \"type\": \"button\",\n                \"sub_type\": \"quick_reply\",\n                \"index\": \"1\",\n                \"parameters\": [\n                    {\n                        \"type\": \"payload\",\n                        \"payload\": \"9rwnB8RbYmPF5t2Mn09x4h\"\n                    }\n                ]\n            }\n        ]\n    }\n}\n",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send List Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"list\",\n        \"header\": {\n            \"type\": \"text\",\n            \"text\": \"<HEADER_TEXT>\"\n        },\n        \"body\": {\n            \"text\": \"<BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"button\": \"<BUTTON_TEXT>\",\n            \"sections\": [\n                {\n                    \"title\": \"<LIST_SECTION_1_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_1_ID>\",\n                            \"title\": \"<SECTION_1_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_2_ID>\",\n                            \"title\": \"<SECTION_1_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_2_DESC>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<LIST_SECTION_2_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_1_ID>\",\n                            \"title\": \"<SECTION_2_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_2_ID>\",\n                            \"title\": \"<SECTION_2_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_2_DESC>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a list message, make a **POST** call to `/messages` and attach a message object with **`type`** = `interactive`. Then, set the **`type`** of the `interactive` object = **`list`**. Finally, include the corresponding **`interactive`** object.\n\nFor more reference information about **`interactive`** objects, see [Interactive Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#interactive-object)."
        							},
        							"response": [
        								{
        									"name": "Send List Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"list\",\n        \"header\": {\n            \"type\": \"text\",\n            \"text\": \"<HEADER_TEXT>\"\n        },\n        \"body\": {\n            \"text\": \"<BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"button\": \"<BUTTON_TEXT>\",\n            \"sections\": [\n                {\n                    \"title\": \"<LIST_SECTION_1_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_1_ID>\",\n                            \"title\": \"<SECTION_1_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_2_ID>\",\n                            \"title\": \"<SECTION_1_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_2_DESC>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<LIST_SECTION_2_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_1_ID>\",\n                            \"title\": \"<SECTION_2_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_2_ID>\",\n                            \"title\": \"<SECTION_2_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_2_DESC>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"messaging_product\": \"whatsapp\",\n  \"contacts\": [{\n      \"input\": \"15555551234\",\n      \"wa_id\": \"<WHATSAPP_ID>\"\n    }],\n  \"messages\": [{\n      \"id\": \"wamid.ID\"\n    }]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply to List Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"list\",\n        \"header\": {\n            \"type\": \"text\",\n            \"text\": \"<HEADER_TEXT>\"\n        },\n        \"body\": {\n            \"text\": \"<BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"button\": \"<BUTTON_TEXT>\",\n            \"sections\": [\n                {\n                    \"title\": \"<LIST_SECTION_1_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_1_ID>\",\n                            \"title\": \"<SECTION_1_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_2_ID>\",\n                            \"title\": \"<SECTION_1_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_2_DESC>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<LIST_SECTION_2_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_1_ID>\",\n                            \"title\": \"<SECTION_2_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_2_ID>\",\n                            \"title\": \"<SECTION_2_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_2_DESC>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a list message, make a **POST** call to `/messages` and attach a message object with **`type`** = `interactive`. Then, set the **`type`** of the `interactive` object = **`list`**. Finally, include the corresponding **`interactive`** object.\n\nFor more reference information about **`interactive`** objects, see [Interactive Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#interactive-object)."
        							},
        							"response": [
        								{
        									"name": "Send Reply to List Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"context\": {\n        \"message_id\": \"<MSGID_OF_PREV_MSG>\"\n    },\n    \"interactive\": {\n        \"type\": \"list\",\n        \"header\": {\n            \"type\": \"text\",\n            \"text\": \"<HEADER_TEXT>\"\n        },\n        \"body\": {\n            \"text\": \"<BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"button\": \"<BUTTON_TEXT>\",\n            \"sections\": [\n                {\n                    \"title\": \"<LIST_SECTION_1_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_1_ID>\",\n                            \"title\": \"<SECTION_1_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_1_ROW_2_ID>\",\n                            \"title\": \"<SECTION_1_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_1_ROW_2_DESC>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<LIST_SECTION_2_TITLE>\",\n                    \"rows\": [\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_1_ID>\",\n                            \"title\": \"<SECTION_2_ROW_1_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_1_DESC>\"\n                        },\n                        {\n                            \"id\": \"<LIST_SECTION_2_ROW_2_ID>\",\n                            \"title\": \"<SECTION_2_ROW_2_TITLE>\",\n                            \"description\": \"<SECTION_2_ROW_2_DESC>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"messaging_product\": \"whatsapp\",\n  \"contacts\": [{\n      \"input\": \"15555551234\",\n      \"wa_id\": \"<WHATSAPP_ID>\"\n    }],\n  \"messages\": [{\n      \"id\": \"wamid.ID\"\n    }]\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Reply Button",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"button\",\n        \"body\": {\n            \"text\": \"<BUTTON_TEXT>\"\n        },\n        \"action\": {\n            \"buttons\": [\n                {\n                    \"type\": \"reply\",\n                    \"reply\": {\n                        \"id\": \"<UNIQUE_BUTTON_ID_1>\",\n                        \"title\": \"<BUTTON_TITLE_1>\"\n                    }\n                },\n                {\n                    \"type\": \"reply\",\n                    \"reply\": {\n                        \"id\": \"<UNIQUE_BUTTON_ID_2>\",\n                        \"title\": \"<BUTTON_TITLE_2>\"\n                    }\n                }\n            ]\n        }\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send interactive messages, make a **POST** call to `/PHONE_NUMBER_ID/messages`(see [Get Phone Number](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers)) and attach a message object with **`type`**\\=`interactive`. Then, add an **`interactive object`**.\n\nFor more reference information about **`interactive`** objects, see [Interactive Object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#interactive-object).\n\n## Response\nA successful response includes an object with an identifier prefixed with wamid. Use the ID listed after wamid to track your message status."
        							},
        							"response": [
        								{
        									"name": "Send Reply Button",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"button\",\n        \"body\": {\n            \"text\": \"<BUTTON_TEXT>\"\n        },\n        \"action\": {\n            \"buttons\": [\n                {\n                    \"type\": \"reply\",\n                    \"reply\": {\n                        \"id\": \"<UNIQUE_BUTTON_ID_1>\",\n                        \"title\": \"<BUTTON_TITLE_1>\"\n                    }\n                },\n                {\n                    \"type\": \"reply\",\n                    \"reply\": {\n                        \"id\": \"<UNIQUE_BUTTON_ID_2>\",\n                        \"title\": \"<BUTTON_TITLE_2>\"\n                    }\n                }\n            ]\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [
        										{
        											"expires": "Invalid Date"
        										}
        									],
        									"body": "{\n  \"messaging_product\": \"whatsapp\",\n  \"contacts\": [{\n      \"input\": \"PHONE_NUMBER\",\n      \"wa_id\": \"WHATSAPP_ID\"\n    }],\n  \"messages\": [{\n      \"id\": \"wamid.ID\"\n    }]\n}"
        								}
        							]
        						},
        						{
        							"name": "Mark Message As Read",
        							"request": {
        								"method": "PUT",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"status\": \"read\",\n    \"message_id\": \"<INCOMING_MSG_ID>\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "When you receive an incoming message from Webhooks, you could use messages endpoint to change the status of it to read.  \nWe recommend marking incoming messages as read within 30 days of receipt.  \n**Note**: you cannot mark outgoing messages you sent as read.\n\nYou need to obtain the **`message_id`** of the incoming message from Webhooks.\n\nFor a more in depth guide for marking messages as read, see [Guide: Mark Messages as Read](https://developers.facebook.com/docs/whatsapp/cloud-api/guides/mark-message-as-read)."
        							},
        							"response": [
        								{
        									"name": "Mark Message As Read",
        									"originalRequest": {
        										"method": "PUT",
        										"header": [
        											{
        												"key": "Authorization",
        												"type": "text",
        												"value": "Bearer {{User-Access-Token}}"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"status\": \"read\",\n    \"message_id\": \"wamid.234123\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v13.0/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v13.0",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Send Single Product Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"product\",\n        \"body\": {\n            \"text\": \"<OPTIONAL_BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<OPTIONAL_FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"catalog_id\": \"367025965434465\",\n            \"product_retailer_id\": \"<ID_TEST_ITEM_1>\"\n        }\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a single product message, make a **POST** call to the **`/{{Version}}/phone_number/messages`** endpoint.\n\nThis request uses an **`interactive`** object and parameter **`type`** should be set to `interactive`. The **`interactive`** parameter must also be set to the **`interactive`** object associated with the single product message. \n\n### Parameters\n\n| Name | Description |\n| --- | --- |\n| **`messaging_product`** | **Required**.<br/>Always set this value to `\"whatsapp\"`. |\n| **`recipient_type`** | **Optional**.<br/> Currently, you can only send messages to individuals. Set this value to `\"individual\"`. <br/><br/>**Default**: `individual` |\n| **`to`** | **Required**. <br/> WhatsApp ID or phone number for the person you want to send a message to.<br></br>See [Phone Numbers, Formatting](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#formatting) for more information. |\n| **`type`** | **Required**.<br/> To send single product messages, set this string value to `\"interactive\"`. |\n| **`interactive`** | **Required**. <br/> The [interactive object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#interactive-object) associated with the single product message. |\n\n### Interactive Object Parameters specific to Single Product Messages\n| Name | Description |\n| --- | --- |\n| **`type`** | **Required**.<br/>Specifies the type of interactive object. For single product messages set this value to `\"product\"`.|\n| **`action`** | **Required**.<br/> |"
        							},
        							"response": [
        								{
        									"name": "Send Single Product Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"product\",\n        \"body\": {\n            \"text\": \"<OPTIONAL_BODY_TEXT>\"\n        },\n        \"footer\": {\n            \"text\": \"<OPTIONAL_FOOTER_TEXT>\"\n        },\n        \"action\": {\n            \"catalog_id\": \"367025965434465\",\n            \"product_retailer_id\": \"<ID_TEST_ITEM_1>\"\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"+1-631-555-5555\",\n            \"wa_id\": \"16315555555\"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGFlaCGg0xcvAdgmZ9plHrf2Mh-o\"\n        }\n    ]\n}\n"
        								}
        							]
        						},
        						{
        							"name": "Send Multi-Product Message",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"product_list\",\n        \"header\": {\n            \"type\": \"<HEADER_TYPE>\",\n            \"text\": \"<YOUR_TEXT_HEADER_CONTENT>\"\n        },\n        \"body\": {\n            \"text\": \"<YOUR_TEXT_BODY_CONTENT>\"\n        },\n        \"footer\": {\n            \"text\": \"<YOUR_TEXT_FOOTER_CONTENT>\"\n        },\n        \"action\": {\n            \"catalog_id\": \"146265584024623\",\n            \"sections\": [\n                {\n                    \"title\": \"<SECTION1_TITLE>\",\n                    \"product_items\": [\n                        {\n                            \"product_retailer_id\": \"<YOUR_PRODUCT1_SKU_IN_CATALOG>\"\n                        },\n                        {\n                            \"product_retailer_id\": \"<YOUR_SECOND_PRODUCT1_SKU_IN_CATALOG>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<SECTION2_TITLE>\",\n                    \"product_items\": [\n                        {\n                            \"product_retailer_id\": \"<YOUR_PRODUCT2_SKU_IN_CATALOG>\"\n                        },\n                        {\n                            \"product_retailer_id\": \"<YOUR_SECOND_PRODUCT2_SKU_IN_CATALOG>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								},
        								"description": "To send a multi-product message, make a **POST** call to the **`/{{Version}}/phone_number/messages`** endpoint.\n\nThis request uses an **`interactive`** object and parameter **`type`** should be set to `interactive`. The **`interactive`** parameter must also be set to the **`interactive`** object associated with the single product message. \n\n### Parameters\n\n| Name | Description |\n| --- | --- |\n| **`messaging_product`** | **Required**.<br/>Always set this value to `\"whatsapp\"`. |\n| **`recipient_type`** | **Optional**.<br/> Currently, you can only send messages to individuals. Set this value to `\"individual\"`. <br/><br/>**Default**: `individual` |\n| **`to`** | **Required**. <br/> WhatsApp ID or phone number for the person you want to send a message to.<br></br>See [Phone Numbers, Formatting](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#formatting) for more information. |\n| **`type`** | **Required**.<br/> To send single product messages, set this string value to `\"interactive\"`. |\n| **`interactive`** | **Required**. <br/> The [interactive object](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/messages#interactive-object) associated with the single product message. |\n\n### Interactive Object Parameters specific to Multi-Product Messages\n| Name | Description |\n| --- | --- |\n| **`type`** | **Required**.<br/>Specifies the type of interactive object. For multi-product messages set this value to `\"product_list\"` |"
        							},
        							"response": [
        								{
        									"name": "Send Multi-Product Message",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"interactive\",\n    \"interactive\": {\n        \"type\": \"product_list\",\n        \"header\": {\n            \"type\": \"<HEADER_TYPE>\",\n            \"text\": \"<YOUR_TEXT_HEADER_CONTENT>\"\n        },\n        \"body\": {\n            \"text\": \"<YOUR_TEXT_BODY_CONTENT>\"\n        },\n        \"footer\": {\n            \"text\": \"<YOUR_TEXT_FOOTER_CONTENT>\"\n        },\n        \"action\": {\n            \"catalog_id\": \"146265584024623\",\n            \"sections\": [\n                {\n                    \"title\": \"<SECTION1_TITLE>\",\n                    \"product_items\": [\n                        {\n                            \"product_retailer_id\": \"<YOUR_PRODUCT1_SKU_IN_CATALOG>\"\n                        },\n                        {\n                            \"product_retailer_id\": \"<YOUR_SECOND_PRODUCT1_SKU_IN_CATALOG>\"\n                        }\n                    ]\n                },\n                {\n                    \"title\": \"<SECTION2_TITLE>\",\n                    \"product_items\": [\n                        {\n                            \"product_retailer_id\": \"<YOUR_PRODUCT2_SKU_IN_CATALOG>\"\n                        },\n                        {\n                            \"product_retailer_id\": \"<YOUR_SECOND_PRODUCT2_SKU_IN_CATALOG>\"\n                        }\n                    ]\n                }\n            ]\n        }\n    }\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"messages"
        											]
        										}
        									},
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"+1-631-555-5555\",\n            \"wa_id\": \"16315555555\"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGFlaCGg0xcvAdgmZ9plHrf2Mh-o\"\n        }\n    ]\n}"
        								}
        							]
        						}
        					],
        					"description": "<!-- \nYou can use this API to send text messages, media, and message templates to your customers. To send a message, create a **Message** object. Each message is identified by a unique ID. You can also mark an incoming message as read through the `/messages` endpoint. You can track message status with Webhooks by ID. \n-->\n\nUse the **`/{{Phone-Number-ID}}/messages`** endpoint to send text messages, media (audio, documents, images, and video), and message templates to your customers. For more information relating to the messages you can send, see [Messages](#1f4f7644-cc97-40b5-b8e4-c19da268fff1).\n\nMessages are identified by a unique ID. You can track message status in the Webhooks through its ID. You could also mark an incoming message as read through the **`/{{Phone-Number-ID}}/messages`** endpoint.\n\n## Prerequisites\n\n*   [User Access Token](https://developers.facebook.com/docs/facebook-login/access-tokens#usertokens) with **`whatsapp_business_messaging`** permission\n*   `phone-number-id` for your registered WhatsApp account. See [Get Phone Number](#c72d9c17-554d-4ae1-8f9e-b28a94010b28).",
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "Phone Numbers",
        					"item": [
        						{
        							"name": "Get Phone Numbers",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/phone_numbers",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"phone_numbers"
        									]
        								},
        								"description": "This API returns all phone numbers in a WhatsApp Business Account specified by the **`{{WABA-ID}}`** value. Get the **`id`** value for the phone number you want to use to send messages with WhatsApp Business Cloud API.\n\n#### Response Parameters\n\n| Name                  | Description                 |\n|-----------------------|-----------------------------|\n| **`verified_name`**         | The verified name associated with the phone number.  |\n| **`display_phone_number`**  | The string representation of the phone number.       |\n| **`id`**                    | The ID associated with the phone number.                            |\n| **`quality_rating`**        | The quality rating of the phone number based on how messages have been received by recipients in recent days. Valid values are: <br/><ul><li>**`Green`**: High Quality</li><li>**`Yellow`**: Medium Quality</li><li>**`Red`**: Low Quality</li><li>**`NA`**: Quality has not been determined</li></ul>  <br/>For more information relating to quality rating, see [About WhatsApp Business Account Message Quality Rating](https://www.facebook.com/business/help/896873687365001).                     |"
        							},
        							"response": [
        								{
        									"name": "Get Phone Numbers",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/phone_numbers",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{WABA-ID}}",
        												"phone_numbers"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"data\": [\n        {\n            \"verified_name\": \"Jasper's Market\",\n            \"display_phone_number\": \"+1 631-555-5555\",\n            \"id\": \"1906385232743451\",\n            \"quality_rating\": \"GREEN\"\n        },\n        {\n            \"verified_name\": \"Jasper's Ice Cream\",\n            \"display_phone_number\": \"+1 631-555-5556\",\n            \"id\": \"1913623884432103\",\n            \"quality_rating\": \"NA\"\n        }\n    ]\n}"
        								}
        							]
        						},
        						{
        							"name": "Get Phone Number By ID",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}"
        									]
        								},
        								"description": "When you query all the phone numbers for a WhatsApp Business Account, each phone number has an **`id`**. You can directly query for a phone number using this **`id`**.\n\n#### Response Parameters\n\n| Name                  | Description                 |\n|-----------------------|-----------------------------|\n| **`verified_name`**         | The verified name associated with the phone number.  |\n| **`display_phone_number`**  | The string representation of the phone number.       |\n| **`id`**                    | The ID associated with the phone number.                            |\n| **`quality_rating`**        | The quality rating of the phone number based on how messages have been received by recipients in recent days. Valid values are: <br/><ul><li>**`Green`**: High Quality</li><li>**`Yellow`**: Medium Quality</li><li>**`Red`**: Low Quality</li><li>**`NA`**: Quality has not been determined</li></ul>  <br/>For more information relating to quality rating, see [About WhatsApp Business Account Message Quality Rating](https://www.facebook.com/business/help/896873687365001).                     |"
        							},
        							"response": [
        								{
        									"name": "Get Phone Number By ID",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"verified_name\": \"Jasper's Market\",\n    \"display_phone_number\": \"+1 631-555-5555\",\n    \"id\": \"1906385232743451\",\n    \"quality_rating\": \"GREEN\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Get Display Name Status (Beta)",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}?fields=name_status",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}"
        									],
        									"query": [
        										{
        											"key": "fields",
        											"value": "name_status",
        											"description": "The status of a display name associated with a specific phone number. The **`name_status`** value can be one of the following:\n\n* `APPROVED`: The name has been approved. You can download your certificate now.\n* `AVAILABLE_WITHOUT_REVIEW`: The certificate for the phone is available and display name is ready to use without review.\n* `DECLINED`: The name has not been approved. You cannot download your certificate.\n* `EXPIRED`: Your certificate has expired and can no longer be downloaded.\n* `PENDING_REVIEW`: Your name request is under review. You cannot download your certificate.\nNONE: No certificate is available."
        										}
        									]
        								},
        								"description": "Include **`fields=name_status`** as a query string parameter to get the status of a display name associated with a specific phone number. This field is currently in beta and not available to all developers."
        							},
        							"response": [
        								{
        									"name": "Get Display Name Status (Beta)",
        									"originalRequest": {
        										"method": "GET",
        										"header": [],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}?fields=name_status",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}"
        											],
        											"query": [
        												{
        													"key": "fields",
        													"value": "name_status",
        													"description": "The status of a display name associated with a specific phone number. The **`name_status`** value can be one of the following:\n\n* `APPROVED`: The name has been approved. You can download your certificate now.\n* `AVAILABLE_WITHOUT_REVIEW`: The certificate for the phone is available and display name is ready to use without review.\n* `DECLINED`: The name has not been approved. You cannot download your certificate.\n* `EXPIRED`: Your certificate has expired and can no longer be downloaded.\n* `PENDING_REVIEW`: Your name request is under review. You cannot download your certificate.\nNONE: No certificate is available."
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"id\" : \"105954558954427\",\n  \"name_status\" : \"AVAILABLE_WITHOUT_REVIEW\"\n}"
        								}
        							]
        						},
        						{
        							"name": "Request Verification Code",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									},
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"code_method\": \"SMS\",\n    \"locale\": \"en_US\"\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/request_code",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"request_code"
        									]
        								},
        								"description": "You need to verify the phone number you want to use to send messages to your customers. Phone numbers must be verified through SMS/voice call. The verification process can be done through the Graph API calls specified below.\n    \n\nTo verify a phone number using Graph API, make a **POST** request to **`{{PHONE_NUMBER_ID}}/request_code`**. In your call, include your chosen verification method and locale. You need to authenticate yourself using **{{User-Access-Token}}** (This is automatically done for you in the **`Request Verification Code`** request).\n\n#### Request Parameters\n\n| Name                  | Description                 |\n|-----------------------|-----------------------------|\n| **`code_method`**         | **Required**.<br/>Specifies the method for verification. Supported options are: `SMS` or `VOICE`.  |\n| **`locale`**         | **Required**.<br/>Specifies your locale. For instance: `\"en_US\"`.  |\n\n#### Response\n\nAfter a successful call to **`Request Verification Code`**, you will receive your verification code via the method you selected in **`code_method`**. To finish the verification process, you need to use the [**`Verify Code`**](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#verify) request."
        							},
        							"response": [
        								{
        									"name": "Request Verification Code",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"code_method\": \"SMS\",\n    \"locale\": \"en_US\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/request_code",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"request_code"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Verify Code",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									},
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"code\": \"<your-requested-code>\"\n}"
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/verify_code",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"verify_code"
        									]
        								},
        								"description": "After you received a SMS or Voice request code from [**`Request Verification Code`**](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#verify), you need to verify the code that was sent to you. To verify this code, make a **POST** request to **`{{PHONE_NUMBER_ID}}/verify_code`** that includes the code as a parameter.\n\n#### Request Parameters\n\n| Name                  | Description                 |\n|-----------------------|-----------------------------|\n| **`code`**<br/>type: Numeric String | **Required**.<br/>The code you received after calling [**`Request Verification Code`**](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/phone-numbers#verify)(**`{{PHONE_NUMBER_ID}}/request_code`**).  |"
        							},
        							"response": [
        								{
        									"name": "Verify Code",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											},
        											{
        												"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"code\": \"<your-requested-code>\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/verify_code",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}",
        												"verify_code"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						}
        					],
        					"description": "Some API calls listed in this document require you to know your phone number’s ID. For more information on how to get a list of phone numbers associated with your WhatsApp Business Account, see [Get All Phone Numbers](https://developers.facebook.com/docs/whatsapp/business-management-api/phone-numbers#all-phone-numbers). The API call response includes IDs for each of the phone numbers connected to your WhatsApp Business Account. Save the ID for the phone you want to use with any **`/{phone-number-ID}`** calls.",
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "Two-Step Verification",
        					"item": [
        						{
        							"name": "Set Two-Step Verification Code",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"pin\": \"<6-digit-pin>\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}"
        									]
        								},
        								"description": "You can use this endpoint to change two-step verification code associated with your account. \nAfter you change the verification code, future requests like changing the name, must use the new code. \n\n**You set up two-factor verification and [register a phone number](https://developers.facebook.com/docs/whatsapp/cloud-api/reference/registration#register-phone) in the same API call.**\n\nYou must use the parameters listed below to change two-step verification \n\n\n## Parameters\n\n| Name          |  Description   |\n|-------------- |--------------- |\n| **`pin`**   | **Required**.<br/> A 6-digit PIN you want to use for two-step verification. |"
        							},
        							"response": [
        								{
        									"name": "Set Two-Step Verification Code",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"pin\": \"123456\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Phone-Number-ID}}"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"success\": true\n}"
        								}
        							]
        						}
        					],
        					"description": "You are required to set up two-step verification for your phone number, as this provides an extra layer of security to the business accounts. To set it up, make a **POST** call to **`/{{Phone-Number-ID}}`** and attach the following parameters. There is no endpoint to disable two-step verification.\n\n<h5>Reminders</h5>\n\n* To use these endpoints, you need to authenticate yourself with a system user access token with the **`whatsapp_business_messaging`** permission.\n* If you need to find your phone number ID, see [Get Phone Number ID](https://developers.facebook.com/docs/whatsapp/business-management-api/manage-phone-numbers#get-a-single-phone-number)."
        				},
        				{
        					"name": "WhatsApp Business Accounts (WABAs)",
        					"item": [
        						{
        							"name": "Get Shared WABA ID",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/debug_token",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"debug_token"
        									]
        								}
        							},
        							"response": [
        								{
        									"name": "Get Shared WABA ID",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/debug_token?input_token={{Token-Returned-From-Embedded-Signup-Flow}}",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"debug_token"
        											],
        											"query": [
        												{
        													"key": "input_token",
        													"value": "{{Token-Returned-From-Embedded-Signup-Flow}}"
        												}
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"data\": {\n    \"app_id\": \"facebook-app-id\",\n    \"type\": \"USER\",\n    \"application\": \"app-name\",\n    \"data_access_expires_at\": 1600000000,\n    \"expires_at\": 1600000000,\n    \"is_valid\": true,\n    \"scopes\": [\n      \"whatsapp_business_management\",\n      \"public_profile\"\n    ],\n    \"granular_scopes\": [\n      {\n        \"scope\": \"whatsapp_business_management\",\n        \"target_ids\": [\n          \"111111111111111\",\n          \"222222222222222\",\n          \"333333333333333\"\n        ]\n      }\n    ],\n    \"user_id\": \"XXXXXXXXXXXXXXX\"\n  }\n}"
        								}
        							]
        						},
        						{
        							"name": "Get List of Shared WABAs",
        							"request": {
        								"method": "GET",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Business-ID}}/client_whatsapp_business_accounts",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Business-ID}}",
        										"client_whatsapp_business_accounts"
        									]
        								}
        							},
        							"response": [
        								{
        									"name": "Get List of Shared WABAs",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{Business-ID}}/client_whatsapp_business_accounts",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{Business-ID}}",
        												"client_whatsapp_business_accounts"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{ \n    \"data\": [\n        {\n            \"id\": 1906385232743451, \n            \"name\": \"My WhatsApp Business Account\", \n            \"currency\": \"USD\", \n            \"timezone_id\": \"1\", \n            \"message_template_namespace\": \"abcdefghijk_12lmnop\" \n        },\n       {\n            \"id\": 1972385232742141, \n            \"name\": \"My Regional Account\", \n            \"currency\": \"INR\", \n            \"timezone_id\": \"5\", \n            \"message_template_namespace\": \"12abcdefghijk_34lmnop\" \n        }\n\n    ],\n    \"paging\": {\n\t\"cursors\": {\n\t\t\"before\": \"abcdefghij\",\n\t\t\"after\": \"klmnopqr\"\n\t}\n   }\n}"
        								}
        							]
        						}
        					],
        					"description": "Some API calls listed in this document require you to know your WhatsApp Business Account (WABA) ID. You have the following methods of getting a WABA ID:\n\n1.  **Business Manager**: This is the most simple way. Just open the [Business Manager](https://business.facebook.com/), select your business, go to Settings and find your WhatsApp Business Account. When you click on the account, you see `\"owned by\"` and `\"id\"`. Save that ID number.\n2.  **During Embedded Signup Onboarding**: See [Get Shared WABA ID with accessToken](https://developers.facebook.com/docs/whatsapp/embedded-signup/manage-accounts#get-shared-waba-id-with-accesstoken) for information.\n3.  **Getting all WABAs shared with your business**: See [Get List of Shared WABAs](https://developers.facebook.com/docs/whatsapp/embedded-signup/manage-accounts#get-list-of-shared-wabas) for information.",
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "WABA Subscriptions",
        					"item": [
        						{
        							"name": "Subscribe to a WABA",
        							"request": {
        								"method": "POST",
        								"header": [],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"subscribed_apps"
        									]
        								},
        								"description": "Subscribe an app to a WhatsApp Business Account. For more information about subscribing to a WhatsApp Business Account, see [Subscribe to a WhatsApp Business Account](https://developers.facebook.com/docs/whatsapp/embedded-signup/webhooks#subscribe-to-a-whatsapp-business-account)."
        							},
        							"response": [
        								{
        									"name": "Subscribe to a WABA",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{WABA-ID}}",
        												"subscribed_apps"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Get All Subscriptions for a WABA",
        							"request": {
        								"method": "GET",
        								"header": [
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"subscribed_apps"
        									]
        								},
        								"description": "To get a list of apps subscribed to Webhooks for a WABA, send a **GET** request to the **`subscribed_apps`** endpoint on the WABA:"
        							},
        							"response": [
        								{
        									"name": "Get All Subscriptions for a WABA",
        									"originalRequest": {
        										"method": "GET",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{WABA-ID}}",
        												"subscribed_apps"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"data\": [\n        {\n            \"whatsapp_business_api_data\": {\n                \"link\": \"<APP1_LINK>\",\n                \"name\": \"<APP1_NAME>\",\n                \"id\": \"7234002551525653\"\n            }\n        },\n        {\n            \"whatsapp_business_api_data\": {\n                \"link\": \"<APP2_LINK>\",\n                \"name\": \"<APP2_LINK>\",\n                \"id\": \"3736565603394103\"\n            }\n        }\n    ]\n}\n"
        								}
        							]
        						},
        						{
        							"name": "Unsubscribe from a WABA",
        							"request": {
        								"method": "DELETE",
        								"header": [
        									{
        										"warning": "This is a duplicate header and will be overridden by the Authorization header generated by Postman.",
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"subscribed_apps"
        									]
        								},
        								"description": "To unsubscribe your app from webhooks for a WhatsApp Business Account, send a **DELETE** request to the `/subscribed_apps/` endpoint on the WABA."
        							},
        							"response": [
        								{
        									"name": "Unsubscribe from a WABA",
        									"originalRequest": {
        										"method": "DELETE",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											}
        										],
        										"url": {
        											"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"{{Version}}",
        												"{{WABA-ID}}",
        												"subscribed_apps"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n    \"success\": true\n}"
        								}
        							]
        						},
        						{
        							"name": "Override Callback URL",
        							"request": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n  \"override_callback_uri\": \"<ALTERNATE_WEBHOOK_ENDPOINT_URL>\",\n  \"verify_token\": \"<ALTERNATE_WEBOOK_ENDPOINT_VERIFICATION_TOKEN>\"\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{WABA-ID}}/subscribed_apps",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{WABA-ID}}",
        										"subscribed_apps"
        									]
        								},
        								"description": "If you want to subscribe to Webhooks for multiple WhatsApp Business Accounts but want messages field Webhooks notifications to be sent to different callback URLs for each WABA, you can override the callback URL when subscribing to Webhooks for each WABA.\n\nTo do this, first verify that the alternate Webhook endpoint can receive and process Webhooks notifications. Then, subscribe to Webhooks for the WABA as your normally would, but include the alternate endpoint's callback URL along with its verification token as a JSON payload:\n\n\nFor more information, see [Overriding the Callback URL](https://developers.facebook.com/docs/whatsapp/embedded-signup/webhooks#overriding-the-callback-url)."
        							},
        							"response": [
        								{
        									"name": "Override Callback URL",
        									"originalRequest": {
        										"method": "POST",
        										"header": [
        											{
        												"key": "Authorization",
        												"value": "Bearer {{User-Access-Token}}",
        												"type": "text"
        											},
        											{
        												"key": "Content-Type",
        												"name": "Content-Type",
        												"value": "application/json",
        												"type": "text"
        											}
        										],
        										"body": {
        											"mode": "raw",
        											"raw": "{\n    \"override_callback_uri\": \"https://alternate-endpoint-callback.com/webhook\",\n    \"verify_token\": \"myvoiceismypassport?\"\n}",
        											"options": {
        												"raw": {
        													"language": "json"
        												}
        											}
        										},
        										"url": {
        											"raw": "https://graph.facebook.com/v15.0/{{WABA-ID}}/subscribed_apps",
        											"protocol": "https",
        											"host": [
        												"graph",
        												"facebook",
        												"com"
        											],
        											"path": [
        												"v15.0",
        												"{{WABA-ID}}",
        												"subscribed_apps"
        											]
        										}
        									},
        									"status": "OK",
        									"code": 200,
        									"_postman_previewlanguage": "json",
        									"header": [
        										{
        											"key": "Content-Type",
        											"value": "application/json",
        											"description": "",
        											"type": "text"
        										}
        									],
        									"cookie": [],
        									"body": "{\n  \"data\" : [\n    {\n      \"override_callback_uri\" : \"https://alternate-endpoint-callback.com/webhook\",\n      \"whatsapp_business_api_data\" : {\n        \"id\" : \"670843887433847\",\n        \"link\" : \"https://www.facebook.com/games/?app_id=67084...\",\n        \"name\" : \"Jaspers Market\"\n      }\n    }\n  ]\n}"
        								}
        							]
        						}
        					],
        					"description": "To get Webhook events for a specific WABA, you need to explicitly subscribe to that WABA. You only need to subscribe to a WABA once and then you receive all Webhook events for phone numbers under that WABA. You don’t need to subscribe for every phone number. \n<br><br>\n\n## Reminders\n- To use these endpoints, you need to authenticate yourself with a [System User Access Token](https://developers.facebook.com/docs/facebook-login/access-tokens#usertokens) with the **`whatsapp_business_management`** permission.\n- WhatsApp Business Account ID (WABA ID). If you need to find your WABA ID see [Get WABA ID](#b7f6e513-f4e4-4b62-b4a2-d18dc5e6249c).\n- You need to set up Webhooks server in your application to receive Webhooks events. To learn how to set up Webhooks, see [Webhooks](https://developers.facebook.com/docs/whatsapp/cloud-api/webhooks/components)."
        				}
        			],
        			"description": "This section explores the full list of WhatsApp Business Cloud API reference. *At this time, this API is only available to Business Solution Providers (BSPs) and existing direct developers.*\n\nThis is a list of root nodes and edges available for the WhatsApp Business API. See [Using the Graph API:Nodes](https://developers.facebook.com/docs/graph-api/overview#nodes) for more information on how the API is structured.\n\n## Main Root Nodes\n\n| Node | Description |\n| --- | --- |\n| **`/PHONE_NUMBER_ID`** | Represents a specific phone number. This endpoint is used to [set up two-factor authentication](#fc57a30c-97e0-4e06-b74b-89fd7fc5f783). |\n| **`/MEDIA_ID`** | Represents a specific media object. Use this endpoint to get and delete media objects. |\n| **`/MEDIA_URL`** | Represents a specific media URL. Use this endpoint to download media from a URL. |\n\n## Phone Number ID\n\nThe **`/PHONE_NUMBER_ID`** endpoint has the following edges:\n\n| Edge | Description |\n| --- | --- |\n| **`/PHONE_NUMBER_ID/register`** | Used to [register a phone number](#149d8034-dca8-4bce-827e-06244127b4b7) or to migrate WhatsApp Business Accounts from a current On-Premises deployment to the new Cloud-Based API. |\n| **`/PHONE_NUMBER_ID/deregister`** | Used to [deregister a phone number](#8f0c509c-ec45-44fb-bfcc-38417ce88e97). |\n| **`/PHONE_NUMBER_ID/whatsapp_business_profile`** | Used to [get and update a business' profile](#9bfce27c-ed1f-44b5-82df-b0508210e6be). |\n| **`/PHONE_NUMBER_ID/media`** | Used to [upload media](#39a02bc0-ede1-4848-b24e-4ac3d501aaea). |\n| **`/PHONE_NUMBER_ID/messages`** | Used to [send messages](#1f4f7644-cc97-40b5-b8e4-c19da268fff1). |\n| **`/PHONE_NUMBER_ID/request_code`** | Used to [request a code to verify a phone](#6d413167-648e-40d0-823a-479ec98a2e33) number's ownership. |\n| **`/PHONE_NUMBER_ID/verify_code`** | Used to [verify a phone number's ownership](#1426eb2d-3ff2-4cc7-9021-c8782672e151). |"
        		},
        		{
        			"name": "Examples",
        			"item": [
        				{
        					"name": "Send Sample Text Message",
        					"request": {
        						"method": "POST",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-WA-ID}}\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"hello_world\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{Phone-Number-ID}}",
        								"messages"
        							]
        						},
        						"description": "You need to create a message template before you can send one. See [Create Template Using the WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation#step-1--create-template-using-the-whatsapp-manager) for more information. If your account is not verified yet, you can use one of our pre-approved templates.\n\nTo send a text-based message template, make a **POST** call to **`/{{Phone-Number-ID}}/messages`** and attach a message object with type = `template`. Then, add a template object.\n\n## Prerequisites\n- You need to create a message template through [WhatsApp Manager](https://developers.facebook.com/docs/whatsapp/message-templates/creation) or [BM API](https://developers.facebook.com/docs/whatsapp/business-management-api/message-templates#creating-message-templates)\n- For an unverified account, we have already [pre-created message templates](https://www.facebook.com/business/help/722393685250070) for you to use.\n\n## Message Objects\n\nMessage templates can contain the following `Message Objects`:\n\n* [Template Object](#fb5ad9b7-7991-443a-a1b5-97fdc5731673)\n* [Language Object](#d9272e38-c3db-458c-a23b-07953abc73a40)\n* [Component Object](#8225365a-acb8-48c7-8e57-079dfc532865)\n* [Parameter Object](#fe8db07c-27ad-49f3-bf33-472ee302c136)\n* [Currency Object](#424b70af-ced8-456d-b1e1-1360c5afb9e9)\n* [Date_Time Object](#ec955b05-7bd4-4273-ad87-ae755b580f6e)\n\n## Response\n\nA successful response includes an object with an identifier prefixed with **`wamid`**."
        					},
        					"response": [
        						{
        							"name": "Send Sample Text message",
        							"originalRequest": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"16315555555\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        						}
        					]
        				},
        				{
        					"name": "Send Sample Shipping Confirmation Template",
        					"request": {
        						"method": "POST",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"{{Recipient-Phone-Number}}\",\n    \"type\": \"template\",\n    \"template\": {\n       \"name\": \"sample_shipping_confirmation\",\n       \"language\": {\n           \"code\": \"en_US\",\n           \"policy\": \"deterministic\"\n       },\n       \"components\": [\n         {\n           \"type\": \"body\",\n           \"parameters\": [\n               {\n                   \"type\": \"text\",\n                   \"text\": \"2\"\n               }\n           ]\n         }\n       ]\n    }\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{Phone-Number-ID}}",
        								"messages"
        							]
        						}
        					},
        					"response": [
        						{
        							"name": "Send Sample Shipping Confirmation Template",
        							"originalRequest": {
        								"method": "POST",
        								"header": [
        									{
        										"key": "Authorization",
        										"value": "Bearer {{User-Access-Token}}",
        										"type": "text"
        									},
        									{
        										"key": "Content-Type",
        										"value": "application/json",
        										"type": "text"
        									}
        								],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n    \"messaging_product\": \"whatsapp\",\n    \"recipient_type\": \"individual\",\n    \"to\": \"16315555555\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"template-name\",\n        \"language\": {\n            \"code\": \"language-and-locale-code\"\n        },\n        \"components\": [\n            {\n                \"type\": \"body\",\n                \"parameters\": [\n                    {\n                        \"type\": \"text\",\n                        \"text\": \"text-string\"\n                    },\n                    {\n                        \"type\": \"currency\",\n                        \"currency\": {\n                            \"fallback_value\": \"$100.99\",\n                            \"code\": \"USD\",\n                            \"amount_1000\": 100990\n                        }\n                    },\n                    {\n                        \"type\": \"date_time\",\n                        \"date_time\": {\n                            \"fallback_value\": \"February 25, 1977\",\n                            \"day_of_week\": 5,\n                            \"year\": 1977,\n                            \"month\": 2,\n                            \"day_of_month\": 25,\n                            \"hour\": 15,\n                            \"minute\": 33,\n                            \"calendar\": \"GREGORIAN\"\n                        }\n                    }\n                ]\n            }\n        ]\n    }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "json",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json",
        									"description": "",
        									"type": "text"
        								}
        							],
        							"cookie": [],
        							"body": "{\n    \"messaging_product\": \"whatsapp\",\n    \"contacts\": [\n        {\n            \"input\": \"48XXXXXXXXX\",\n            \"wa_id\": \"48XXXXXXXXX \"\n        }\n    ],\n    \"messages\": [\n        {\n            \"id\": \"wamid.gBGGSFcCNEOPAgkO_KJ55r4w_ww\"\n        }\n    ]\n}"
        						}
        					]
        				},
        				{
        					"name": "Send Sample Issue Resolution Template",
        					"request": {
        						"method": "POST",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n   \"messaging_product\": \"whatsapp\",\n   \"to\": \"{{Recipient-Phone-Number}}\",\n   \"type\": \"template\",\n   \"template\": {\n       \"name\": \"sample_issue_resolution\",\n       \"language\": {\n           \"code\": \"en_US\",\n           \"policy\": \"deterministic\"\n       },\n       \"components\": [\n           {\n               \"type\": \"body\",\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"*Mr. Jones*\"\n                   }\n               ]\n           },\n           {\n               \"type\": \"button\",\n               \"sub_type\": \"quick_reply\",\n               \"index\": 0,\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"Yes\"\n                   }\n               ]\n           },\n           {\n               \"type\": \"button\",\n               \"sub_type\": \"quick_reply\",\n               \"index\": 1,\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"No\"\n                   }\n               ]\n           }\n       ]\n   }\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        							"protocol": "https",
        							"host": [
        								"graph",
        								"facebook",
        								"com"
        							],
        							"path": [
        								"{{Version}}",
        								"{{Phone-Number-ID}}",
        								"messages"
        							]
        						}
        					},
        					"response": [
        						{
        							"name": "Send Sample Issue Resolution Template",
        							"originalRequest": {
        								"method": "POST",
        								"header": [],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n   \"messaging_product\": \"whatsapp\",\n   \"to\": \"{{Recipient-Phone-Number}}\",\n   \"type\": \"template\",\n   \"template\": {\n       \"name\": \"sample_issue_resolution\",\n       \"language\": {\n           \"code\": \"en_US\",\n           \"policy\": \"deterministic\"\n       },\n       \"components\": [\n           {\n               \"type\": \"body\",\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"*Mr. Jones*\"\n                   }\n               ]\n           },\n           {\n               \"type\": \"button\",\n               \"sub_type\": \"quick_reply\",\n               \"index\": 0,\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"Yes\"\n                   }\n               ]\n           },\n           {\n               \"type\": \"button\",\n               \"sub_type\": \"quick_reply\",\n               \"index\": 1,\n               \"parameters\": [\n                   {\n                       \"type\": \"text\",\n                       \"text\": \"No\"\n                   }\n               ]\n           }\n       ]\n   }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": "https://graph.facebook.com/{{Version}}/{{Phone-Number-ID}}/messages",
        									"protocol": "https",
        									"host": [
        										"graph",
        										"facebook",
        										"com"
        									],
        									"path": [
        										"{{Version}}",
        										"{{Phone-Number-ID}}",
        										"messages"
        									]
        								}
        							},
        							"status": "OK",
        							"code": 200,
        							"_postman_previewlanguage": "",
        							"header": [
        								{
        									"key": "Content-Type",
        									"value": "application/json"
        								}
        							],
        							"cookie": [
        								{
        									"expires": "Invalid Date"
        								}
        							],
        							"body": ""
        						}
        					]
        				}
        			]
        		},
        		{
        			"name": "Webhooks",
        			"item": [
        				{
        					"name": "Understanding Webhooks",
        					"item": [],
        					"description": "Whenever a trigger event occurs, the WhatsApp Business API sees the event and sends a notification to a Webhook URL you have previously specified. You can get two types of notifications:\n\n<ul><li><p>Received messages: This alert lets you know when you have received a message. These can also be called <code>\"inbound notifications\"</code> throughout the documentation.</p></li><li><p>Message status and pricing notifications: This alert lets you know when the status of a message has changed. For example, the message has been read or delivered. These can also be called <code>\"outbound notifications\"</code> throughout the documentation.</p></li></ul>\n\nAll Webhooks have the following generic JSON format:\n\n``` json\n{\n  \"object\": \"whatsapp_business_account\",\n  \"entry\": [\n    {\n      \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n      \"changes\": [\n        {\n          \"value\": {\n              \"messaging_product\": \"whatsapp\",\n              \"metadata\": {\n                  \"display_phone_number\": \"PHONE_NUMBER\",\n                  \"phone_number_id\": \"PHONE_NUMBER_ID\"\n              },\n              # specific Webhooks payload            \n          },\n          \"field\": \"messages\"\n        }\n      ]\n    }\n  ]\n}\n\n```\n\nSee [Components](#cefb0b2a-5fc8-43c3-aea6-865ba98f6061) for information on each field.  \nIf you receive a message that is not supported for the Cloud API beta release, you will get an [unknown message Webhook](https://developers.facebook.com/docs/whatsapp/cloud-api/webhooks#received-unknown-messages).\n\n### Limitations\n\nAt any time, each Meta App can have only one endpoint configured. If you need to send your webhook updates to multiple endpoints, you need multiple Meta Apps."
        				},
        				{
        					"name": "Generate Endpoint for Webhook Testing",
        					"item": [],
        					"description": "If you want to test your Webhooks, see [Generate Endpoint for Webhooks Testing](https://developers.facebook.com/docs/whatsapp/api/webhooks/generate-endpoint) for more information."
        				},
        				{
        					"name": "Components",
        					"item": [
        						{
        							"name": "Entry Object",
        							"item": [],
        							"description": "| Field                   | Description                      |\n|--------------------|----------------------------------|\n| **`id`**           | The ID of Whatsapp Business Accounts this Webhook belongs to. |\n| **`changes`**      | Changes that triggered the Webhooks call. This field contains an array of change objects. |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Change Object",
        							"item": [],
        							"description": "| Field                   | Description                      |\n|--------------------|----------------------------------|\n| **`value`**        | A [value object](#54a8a703-18c8-47af-9aaf-a560afd9aa7b). Contains details of the changes related to the specified field. |\n| **`field`**        | Contains the type of notification you are getting on that Webhook. Currently, the only option for this API is `“messages”`. |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Metadata Object",
        							"item": [],
        							"description": "| Name                    | Description                      |\n|-------------------------|----------------------------------|\n| **`display_phone_number`** | The phone number of the business account that is receiving the Webhooks. |\n| **`phone_number_id`** | The ID of the phone number receiving the Webhooks. You can use this **`phone_number_id`** to send messages back to customers. |"
        						},
        						{
        							"name": "Value Object",
        							"item": [],
        							"description": "| Field                   | Description                      |\n|-------------------------|----------------------------------|\n| **`messaging_product`** | The messaging service used for Webhooks. For WhatsApp messages, this value needs to be set to `“whatsapp”`. |\n| **`metadata`**        | The metadata about your phone number. |\n| **`messages`**        | An array of [message objects](#9ad6ff8c-72d6-4b04-b4d5-45fe093976ad). **Added to Webhooks for incoming message notifications**.|\n| **`statuses`**        | An array of [message status objects](#15305365-753b-400a-90ce-de069ee7f909). **Added to Webhooks for message status update**. |\n| **`errors`**          | An array of [message error objects](#a89ecf92-9b51-409a-af27-2c3b9bc0fd7e). |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Contact Object",
        							"item": [],
        							"description": "| Field                   | Description                      |\n|-------------------------|----------------------------------|\n| **`profile`**           | The [profile object](#bedd6ec6-be2f-42ca-9b0e-a6c3f6f4cd70).          |\n| **`wa_id`**             | The WhatsApp ID of the customer. You can send messages using this **`wa_id`**.          |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Profile Object",
        							"item": [],
        							"description": "| Field | Description |\n| --- | --- |\n| **`name`** |  **Optional.**<br/> Specifies the sender's profile name. |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Messages Object",
        							"item": [],
        							"description": "The messages array of objects is nested within the **`Value`** object and is triggered when a customer updates their profile information or a customer sends a message to the business that is subscribed to the Webhook.\n\n| Field                   | Description                      |\n|-------------------------|----------------------------------|\n| **`from`**              | The customer's phone number.       |\n| **`id`**                | The unique identifier of incoming message, you can use messages endpoint to mark it as read.       |\n| **`timestamp`**         | The timestamp when a customer sends a message.       |\n| **`type`**              | The type of message being received.<br/><br/>Supported values are:<ul><li>`text`: for text messages.</li><li>`image`: for image (media) messages.</li><li>`interactive`: for interactive messages.</li><li>`document`: for document (media) messages.</li><li>`audio`: for audio and voice (media) messages.</li><li>`sticker`: for sticker messages.</li><li>`order`: for when a customer has placed an order.</li><li>`video`: for video (media) messages.</li><li>`button`: for responses to [interactive message templates](#b00c9f92-be3e-4511-af38-be72827a7f3a).</li><li>`contacts`: for contact messages.</li><li>`location`: for location messages.</li><li>`unknown`: for unknown messages.</li><li>`system`: for user number change messages.</li></ul>       |\n| **`context`**              |  **Added to Webhook if message is forwarded or an inbound reply.** <br/>A [context object](#57287ed9-e640-44db-b991-9f03749cb645).|\n| **`identity`**            | **Added to Webhook if show_security_notifications is enabled in application settings.** <br/>An [identity object](#a1b69494-a0f5-405c-9dd5-968362c4a30f). |\n| **`text`**              |  **Added to Webhook if type is `text`**. <br/>A [text object](#e2e1d9df-0886-4e29-a8c8-ff93223178bd). |\n| **`audio`**              |  **Added to Webhook if type is `audio` (including voice messages)**. <br/>A [media object](#058689bd-e754-4efc-938d-bec7bec3b1c4) with the audio information. |\n| **`image`**              |  **Added to Webhook if type is `image`**. <br/>A [media object](#058689bd-e754-4efc-938d-bec7bec3b1c4) with the image information. |\n| **`sticker`**              |  **Added to Webhook if type is `sticker`**. <br/>A [media object](#058689bd-e754-4efc-938d-bec7bec3b1c4) with the sticker information. |\n| **`video`**              |  **Added to Webhook if type is `video`**. <br/>A [media object](#058689bd-e754-4efc-938d-bec7bec3b1c4) with the video information. |\n| **`interactive`**              |  **Added to Webhook if type is `interactive`**. <br/>When a customer has interacted with your message, an [interactive object](https://documenter.getpostman.com/view/13382743/UVC5FTHT?fbclid=IwAR083mCseNzJm-JgxlIQbdF30hkAbEOHkbBaw9bA7-isGKU6uwtq1RJKc0o#68fe0550-aba5-4ee3-b79d-0846f3dddef1) is included in the **``Messages`** object. |\n| **`order`** | Included in the **`Messages`** object when a customer has placed an order. The order object can contain the following fields:<ul><li>**`catalog_id`**: ID for the catalog the ordered item belongs to.</li><li>**`text`**: Text message from the user sent along with the order.</li><li>**`product_items`**: Array of product item objects.</li></ul><br/><br/>The **`product_items`** object contains the following fields:<ul><li>**`product_retailer_id`**: The unique identifier of the product in a catalog.</li><li>**`quanitity`**: The number of items.</li><li>**`item_price`**: The price of each item.</li><li>**`currency`**: The price currency.</li></ul>\n| **`document`**              |  **Added to Webhook if type is `document`**. <br/>A [media object](#058689bd-e754-4efc-938d-bec7bec3b1c4) with the document information. |\n| **`errors`**              |  **Added to Webhook if type is `unknown`**. <br/>If displayed, this field contains the following error message:<br/><br/>```[{\"code\":131051,\"details\":\"Message type is not currently supported\",\"title\":\"Unsupported message type\"]```|\n| **`system`**              |  **Added to Webhook if type is `system`**. <br/>A [system message object](#15b395a9-15eb-4610-ba12-3c9f8a5e0528). |\n| **`button`**              |  **Added to Webhook if type is `button`**. <br/>A [button message object](#cffcb0b3-f6c8-45d6-b4a1-5deaa8955a7d). <br/>This field is used when the Webhook notifies you that a user clicked on a quick reply button.|\n| **`referral`**              |  **Added to Webhook if the message is coming from a user that clicked an ad that is `Click To WhatsApp`**.<br/>A [referral object](). This is how the referral object works:<ol><li>A user clicks on an ad with the Click to WhatsApp call-to-action.</li><li>User is redirected to WhatsApp and sends a message to the advertising business.</li><li>User sends a message to the business. Be aware that users may elect to remove their referral data.</li><li>The advertising business gets an inbound message notification including the **`referral`** object, which provides additional context on the ad that triggered the message. Knowing all this information, the business can appropriately reply to the user message.</li></ol>\n|",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Text Object",
        							"item": [],
        							"description": "| Field                 | Description                      |\n|-------------------------|----------------------------------|\n| **`body`**              | The text of the text message.    |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Context Object",
        							"item": [],
        							"description": "| Field                 | Description                      |\n|----------------------------|----------------------------------|\n| **`forwarded`**            | **Added to Webhooks if message was forwarded.**<br/>Set to **`true`** if the received message has been forwarded.         |\n| **`frequently_forwarded`** | **Added to Webhooks if message has been frequently forwarded.**<br/>Set to **`true`** if the received message has been forwarded more than five times.         |\n| **`from`**               | **Added to Webhooks if message is an inbound reply to a sent message.**<br/>The WhatsApp ID of the sender of the sent message.         |\n| **`id`**                | **Optional**<br/>The message ID for the sent message for an inbound reply. |\n| **`referred_product`** | **Required for Product Enquiry Messages**.<br/><br/>Specifies the product the user is requesting information about. For more information, see [Receive Response From Customers](https://developers.facebook.com/docs/whatsapp/on-premises/guides/commerce-guides/receive-responses-from-customers).<br/><br/>The **`referred_product`** object contains the following fields:<ul><li>**`catalog_id`**: Unique identifier of the Meta catalog linked to the WhatsApp Business Account.</li><li>**`product_retailer_id`**: Unique identifier of the product in a catalog.</li></ul> |",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Identity Object",
        							"item": [],
        							"description": "| Field                 | Description                      |\n|----------------------------|----------------------------------|\n| **`acknowledged`**         | State of acknowledgment for latest **`user_identity_changed`** system notification. |\n| **`created_timestamp`**    | The timestamp of when the WhatsApp Business API detected the user potentially changed. |\n| **`hash`**                 | Identifier for the latest **`user_identity_changed`** system notification. |"
        						},
        						{
        							"name": "Media Object",
        							"item": [],
        							"description": "**`Media Object`** is used for audio, images, documents, videos and stickers.\n\n| Field                 | Description                      |\n|----------------------------|----------------------------------|\n| **`caption`**         | **Added to Webhooks if it has been previously specified.**<br/>The caption that describes the media. |\n| **`filename`**        | **Added to Webhooks for document messages.**<br/>The media's filename on the sender's device. |\n| **`id`**              | The ID of the media. |\n| **`mime_type`**       | The mime type of the media. |\n| **`sha256`**          | The checksum of the media. |"
        						},
        						{
        							"name": "Reaction Object",
        							"item": [],
        							"description": "The `Reaction` object contains information about messages that contain a reaction and corresponding emojis. This object contains the following fields:\n\n| Field                 | Description                      |\n|-----------------------|----------------------------------|\n| **`message_id`**      | Specifies the **`wamid`** of the message received that contained the reaction. |\n| **`emoji`**           | The emoji used for the reaction.|"
        						},
        						{
        							"name": "Interactive Object",
        							"item": [
        								{
        									"name": "Button Reply Object",
        									"item": [],
        									"description": "| Field | Description |\n| --- | --- |\n| **`id`** | The unique identifier of the button. |\n| **`title`** | The title of the button. |"
        								},
        								{
        									"name": "List Reply Object",
        									"item": [],
        									"description": "| Field | Description |\n| --- | --- |\n| **`id`** | The unique identifier (ID) of the selected row. |\n| **`title`** | The title of the selected row. |\n| **`description`** | The description of the selected row. |"
        								}
        							],
        							"description": "| Field | Description |\n| --- | --- |\n| **`type`** | Contains the type of interactive object. Supported options are:<ul><li>`button_reply`: for responses of Reply Buttons.</li><li>`list_reply`: for responses to List Messages and other interactive objects.</li></ul>|\n| **`button_reply`** | **Used on Webhooks related to Reply Buttons.**  <br/>Contains a [button reply object](#3530f9ca-704b-4811-a1bc-04472a72e00f). |\n| **`list_reply`** | **Used on Webhooks related to List Messages**  <br/>Contains a [list reply object](#374bd5d7-e6c5-48e6-80f9-5821ad5da7ff). |"
        						},
        						{
        							"name": "System Message Object",
        							"item": [],
        							"description": "This object is added to Webhooks if a user has changed their phone number and if a user’s identity has potentially changed on WhatsApp.\n\n| Field                 | Description                      |\n|-----------------------|----------------------------------|\n| **`body`**            | Describes the system message event. Supported use cases are:<ul><li>**Phone number update:** for when a user changes from an old number to a new number.</li><li>**Identity update:** for when a user identity has changed.</li></ul> |\n| **`new_wa_id`**       | **Added to Webhooks for phone number updates.**<br/>New WhatsApp ID of the customer.|\n| **`identity`**       | **Added to Webhooks for identity updates.**<br/>New WhatsApp ID of the customer.|\n|**`type`**       | Supported types are:<ul><li>**user_changed_number**: for a user changed number notification.</li><li>**user_identity_changed**: for user identity changed notification.</li></ul>|\n| **`user`**       | **Added to Webhooks for identity updates.**<br/>The new WhatsApp user ID of the customer.|",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Statuses Object",
        							"item": [
        								{
        									"name": "Conversation Object",
        									"item": [],
        									"description": "The **`conversation`** object tracks the attributes of your current conversation. The following fields are specified within the conversation object:\n\n| Field                        | Description                      |\n|------------------------------|----------------------------------|\n| **`id`**<br/>type: string    |  The ID of the conversation the given status notification belongs to. |\n| **`origin`**<br/>type: object | Describes where the conversation originated from. See [origin object](#efa78bee-682f-4620-9d97-14ecb3cee0ef) for more information.|\n| **`expiration_timestamp`**<br/>type: [UNIX timestamp](https://en.wikipedia.org/wiki/Unix_time?fbclid=IwAR3joUPmHY6qMICZD4EeLhuRgSR7F28eKavnrVnru3QFMhtgOcCJ3V-QjbQ)    |  The timestamp when the current ongoing conversation expires. This field is not present in all Webhook types. |",
        									"auth": {
        										"type": "noauth"
        									},
        									"event": [
        										{
        											"listen": "prerequest",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										},
        										{
        											"listen": "test",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										}
        									]
        								},
        								{
        									"name": "Origin Object",
        									"item": [],
        									"description": "**This object will become available when [Conversation-Based Pricing](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing) launches in a future update.**\nThe **`origin`** object describes where a conversation has originated from. The following fields are specified within the origin object:\n\n| Field                        | Description                      |\n|------------------------------|----------------------------------|\n| **`type`**<br/>type: string  | Indicates where a conversation has started. This can also be referred to as a conversation entry point. Currently, the available options are:<ul><li>**`business_initiated`**: indicates that the conversation started by a business sending the first message to a user. This applies any time it has been more than 24 hours since the last user message.</li><li>**`user_initiated`**: indicates that the conversation started by a business replying to a user message. This applies only when the business reply is within 24 hours of the last user message.</li><li>**`referral_conversion`**: indicates that the conversation originated from a [free entry point](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points). These conversations are always user-initiated.</li></ul>|",
        									"auth": {
        										"type": "noauth"
        									},
        									"event": [
        										{
        											"listen": "prerequest",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										},
        										{
        											"listen": "test",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										}
        									]
        								},
        								{
        									"name": "Pricing Object",
        									"item": [],
        									"description": "The **`pricing`** object includes your billing attributes. The following fields are specified within the pricing object:\n\n| Field                                  | Description                      |\n|----------------------------------------|----------------------------------|\n| **`pricing_model`**<br/>type: string  |  Type of pricing model being used. Current supported values are:<ul><li>`\"CBP\"` (conversation-based pricing): See [Conversation-Based Pricing](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing) for rates based on recipient country.</li><li>`\"NBP\"` (notification-based pricing): Notifications are also known as Template Messages ([click here for details on pricing](https://developers.facebook.com/docs/whatsapp/pricing)). This pricing model will be deprecated in a future release early 2022.</li></ul>|\n| **`billable`**<br/>type: boolean  | Indicates if the given message or conversation is billable. Value varies according to **`pricing_model`**.<br/><br/>If you are using CBP (conversation-based pricing):<ul><li>This flag is set to **`false`** if the conversation was initiated from [free entry points](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points). Conversations initiated from free entry points are not billable.</li><li>For all other conversations, it’s set to **`true`**.</li><li>This is also set to **`true`** for conversations inside your free tier limit. You are not charged for these conversations, but they are considered billable and are reflected on your invoice.</li></ul><br/>If you are using NBP (notification-based pricing):<ul><li>This flag is **`false`** for user-initiated conversations.</li><li>This flag is set to **`true`** for notification messages (template messages)</li></ul> |\n| **`category`**<br/>type: string | Indicates the conversation pricing category. Currently, available options are:<ul><li>**`business_initiated`**: indicates that the conversation was started by a business sending the first message to a user. This applies any time it has been more than 24 hours since the last user message.</li><li>**`user_initiated`**: indicates that the conversation was initiated by a business replying to a user message. This applies only when the business reply is within 24 hours of the last user message.</li><li>**`referral_conversion`**: indicates that the conversation originated from a [free entry point](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points). These conversations are always user-initiated.</li></ul>|",
        									"auth": {
        										"type": "noauth"
        									},
        									"event": [
        										{
        											"listen": "prerequest",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										},
        										{
        											"listen": "test",
        											"script": {
        												"type": "text/javascript",
        												"exec": [
        													""
        												]
        											}
        										}
        									]
        								}
        							],
        							"description": "The **`statuses`** object informs you of the status of messages between you, users, and/or groups.\n\n| Field                        | Description                      |\n|------------------------------|----------------------------------|\n| **`id`**<br/>type: string    |  The message ID. |\n| **`recipient_id`**<br/>type: string | The WhatsApp ID of the recipient.|\n| **`status`**<br/>type: string          | The status of the message. Valid values are: **`read`**, **`delivered`**, **`sent`**, **`failed`**, or **`deleted`**. <br/><br/>For more information, see [All Possible Message Statuses](#9a302c08-c8d7-42da-8800-1b24bed8adaf).|\n| **`timestamp`**<br/>type: string       | The timestamp of the status message.|\n| **`type`**<br/>type: string            | The type of entity this status object is about. Currently, the only available option is `\"message\"`.<br/>_This object is only available for the On-Premises implementation of the API. Cloud API developers will not receive this field._ |\n| **`conversation`**<br/>type: object    | **This object will be provided by default when [Conversation-Based Pricing](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing) launches in a future update.**<br/>Object containing conversation attributes, including **`id`**. See [conversation object](#7f5a70d6-7302-44d3-a473-74b92c21365b) for more information.<br/><br/>WhatsApp defines a conversation as a 24-hour session of messaging between a person and a business. There is no limit on the number of messages that can be exchanged in the fixed 24-hour window. The 24-hour conversation session begins when:<ul><li>A business-initiated message is delivered to a user</li><li>A business’ reply to a user message is delivered</li></ul><br/>The 24-hour conversation session is different from the 24-hour customer support window. The customer support window is a rolling window that is refreshed when a user-initiated message is delivered to a business. Within the customer support window businesses can send free-form messages. Any business-initiated message sent more than 24 hours after the last customer message must be a template message. |\n| **`pricing`**<br/>type: object       | **This object will be provided by default when [Conversation-Based Pricing](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing) launches in a future update.**<br/>| Object containing billing attributes, including **`pricing_model`**, **`billable`** flag, and **`category`**. See [pricing object](https://documenter.getpostman.com/view/13382743/UVC5FTHT#f72385a4-9ab5-40ec-bd1b-fd0adf0d37e3) for more information.\n| **`errors`**          | **Added to Webhook if status is set to `failed`**. <br/>An array of [error objects](#a89ecf92-9b51-409a-af27-2c3b9bc0fd7e) with information about a message’s delivery failure.|",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Error Object",
        							"item": [],
        							"description": "| Field                 | Description                      |\n|-----------------------|----------------------------------|\n| **`code`**            | The error code. |\n| **`title`**           | The error title.|",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Button Object",
        							"item": [],
        							"description": "| Field                 | Description                      |\n|-----------------------|----------------------------------|\n| **`payload`**         | The developer-defined payload for the button when a business account sends interactive messages. |\n| **`text`**            | The button text.|",
        							"auth": {
        								"type": "noauth"
        							},
        							"event": [
        								{
        									"listen": "prerequest",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								},
        								{
        									"listen": "test",
        									"script": {
        										"type": "text/javascript",
        										"exec": [
        											""
        										]
        									}
        								}
        							]
        						},
        						{
        							"name": "Referral Object",
        							"item": [],
        							"description": "| Field | Description |\n| --- | --- | \n| **`source_url`** | Specifies the URL that leads to the ad or post clicked by the user. Opening this URL takes you to the ad viewed by your user. |\n| **`source_type`** | Specifies the type of the ad's source. Supported values are `\"ad\"` or `\"post\"`. |\n| **`source_id`** | Specifies the Meta ID for an ad or post. |\n| **`headline`** | Specifies the headline used in the ad or post that generated the message. |\n| **`body`** | The description, or body, from the ad or post that generated the message. |\n| **`media_type`** | Media present in the ad or post the user clicked. Supported values are `\"image\"` or `\"video\"`. |\n| **`image_url`** | **Added if media_type is `“image”`**.<br/> Contains a URL to the raw image. |\n| **`video_url`** | **Added if media_type is `“video”`**.<br/> Contains a URL to the video. |\n| **`thumbnail_url`** | **Added if media_type is `“video”`**.<br/> Contains a URL to the thumbnail image of the clicked video. |"
        						}
        					],
        					"description": "The top level Webhooks array contains the following two fields:\n\n| Field                   | Description                      |\n|--------------------|----------------------------------|\n| **`object`**       | All Webhook events for WhatsApp Cloud API belong under the **`whatsapp_business_account`** object. |\n| **`entry`**        | An array of [entry objects](#818e8a3e-37b1-4bb6-8441-3291c02c0258). |"
        				},
        				{
        					"name": "Message Status Types",
        					"item": [],
        					"description": "This table lists all possible options for the status of a message.\n\n| Status | Description | WhatsApp Mobile Equivalent |\n| --- | --- | --- |\n| **`sent`** | Message received by WhatsApp server. | One checkmark |\n| **`delivered`** | Message delivered to the recipient. | Two checkmarks |\n| **`read`** | Message read by recipient. | Two blue checkmarks |\n| **`failed`** | Message failed to send. | Red error triangle |\n| **`deleted`** | Message deleted by the user. | Message is replaced in WhatsApp mobile with the note `\"This message was deleted\"`. |",
        					"auth": {
        						"type": "noauth"
        					},
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "Received Text Message",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16505553333\",\n                            \"phone_number_id\": \"27681414235104944\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"Kerry Fisher\"\n                                },\n                                \"wa_id\": \"16315551234\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"16315551234\",\n                                \"id\": \"wamid.ABGGFlCGg0cvAgo-sJQh43L5Pe4W\",\n                                \"timestamp\": \"1603059201\",\n                                \"text\": {\n                                    \"body\": \"Hello this is an answer\"\n                                },\n                                \"type\": \"text\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following is an example of a text message you received from a customer:"
        					},
        					"response": []
        				},
        				{
        					"name": "Received Text Message with Show Security Notifications",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<PHONE_NUMBER>\",\n                            \"phone_number_id\": \"27681414235104944\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<CONTACT_NAME>\"\n                                },\n                                \"wa_id\": \"<WA_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"<FROM_PHONE_NUMBER>\",\n                                \"id\": \"ABGGFjFVU2AfAgo6V-Hc5eCgK5Gh\",\n                                \"identity\": {\n                                    \"acknowledged\": true,\n                                    \"created_timestamp\": 1602532300000,\n                                    \"hash\": \"Sjvjlx8G6Z0=\"\n                                },\n                                \"text\": {\n                                    \"body\": \"Hi from new number 3601\"\n                                },\n                                \"timestamp\": \"1602532300\",\n                                \"type\": \"text\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following is an example of a text message you receive from a customer, when you have the **`show_security_notifications`** parameter set to **`true`** in the **application settings**."
        					},
        					"response": []
        				},
        				{
        					"name": "Received Message with Reaction",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<PHONE_NUMBER>\",\n                            \"phone_number_id\": \"27681414235104944\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<CONTACT_NAME>\"\n                                },\n                                \"wa_id\": \"<WA_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"sender_wa_id\",\n                                \"id\": \"message_id\",\n                                \"timestamp\": \"message_timestamp\",\n                                \"type\": \"reaction\",\n                                \"reaction\": {\n                                    \"emoji\": \"<emoji>\",\n                                    \"messsage_id\": \"<WAMID>\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": ""
        						},
        						"description": "If the message being reacted to is more than 30 days old the reaction message webhook will not be delivered. Note that for reactions, the **`timestamp`** value indicates when the customer sent the reaction, not when the Webhook was generated."
        					},
        					"response": []
        				},
        				{
        					"name": "Received Media Message with Image",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16505553333\",\n                            \"phone_number_id\": \"<PHONE_NUMBER_ID>\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<CONTACT_NAME>\"\n                                },\n                                \"wa_id\": \"<WA_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"<FROM_PHONE_NUMBER>\",\n                                \"id\": \"wamid.id\",\n                                \"timestamp\": \"<TIMESTAMP>\",\n                                \"type\": \"image\",\n                                \"image\": {\n                                    \"caption\": \"This is a caption\",\n                                    \"mime_type\": \"image/jpeg\",\n                                    \"sha256\": \"81d3bd8a8db4868c9520ed47186e8b7c5789e61ff79f7f834be6950b808a90d3\",\n                                    \"id\": \"2754859441498128\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"url": {
        							"raw": ""
        						},
        						"description": "When a message with media is received, the WhatsApp Business API downloads the media. A notification is sent to your Webhook once the media is downloaded.  \n\nThe Webhook notification contains information that identifies the media object and enables you to find and retrieve the object. [Use the media endpoints to retrieve the media](#39a02bc0-ede1-4848-b24e-4ac3d501aaea)."
        					},
        					"response": []
        				},
        				{
        					"name": "Received Media Message with Sticker",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<DISPLAY_PHONE_NUMBER>\",\n                            \"phone_number_id\": \"<PHONE_NUMBER_ID>\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<CONTACT_NAME>\"\n                                },\n                                \"wa_id\": \"<WA_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"<SENDER_PHONE_NUMBER>\",\n                                \"id\": \"<WHATSAPP_BUSINESS_ACCOUNT_ID>\",\n                                \"timestamp\": \"<TIMESTAMP>\",\n                                \"type\": \"sticker\",\n                                \"sticker\": {\n                                    \"id\": \"<ID>\",\n                                    \"animated\": false,\n                                    \"mime_type\": \"image/webp\",\n                                    \"sha256\": \"<HASH>\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "When you receive a media message containing a sticker, WhatsApp Business API downloads the sticker and a notification is sent to your Webhook once the sticker is downloaded.\n\nThe Webhook notification contains information that identifies the media object and allows you to find and retrieve the object. [Use the media endpoints to retrieve the media](https://desktop.postman.com/?desktopVersion=9.16.0&userId=14291747&teamId=1367031)."
        					},
        					"response": []
        				},
        				{
        					"name": "Received Contact Messages",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"<WHATSAPP_BUSINESS_ACCOUNT_ID>\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<PHONE_NUMBER>\",\n                            \"phone_number_id\": \"<PHONE_NUMBER_ID>\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<NAME>\"\n                                },\n                                \"wa_id\": \"<WHATSAPP_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"<PHONE_NUMBER>\",\n                                \"id\": \"<wamid.ID>\",\n                                \"timestamp\": \"<TIMESTAMP>\",\n                                \"contacts\": [\n                                    {\n                                        \"addresses\": [\n                                            {\n                                                \"city\": \"<ADDRESS_CITY>\",\n                                                \"country\": \"<ADDRESS_COUNTRY>\",\n                                                \"country_code\": \"<ADDRESS_COUNTRY_CODE>\",\n                                                \"state\": \"<ADDRESS_STATE>\",\n                                                \"street\": \"<ADDRESS_STREET>\",\n                                                \"type\": \"<HOME|WORK>\",\n                                                \"zip\": \"<ADDRESS_ZIP>\"\n                                            }\n                                        ],\n                                        \"birthday\": \"<CONTACT_BIRTHDAY>\",\n                                        \"emails\": [\n                                            {\n                                                \"email\": \"<CONTACT_EMAIL>\",\n                                                \"type\": \"<WORK|HOME>\"\n                                            }\n                                        ],\n                                        \"name\": {\n                                            \"formatted_name\": \"<CONTACT_FORMATTED_NAME>\",\n                                            \"first_name\": \"<CONTACT_FIRST_NAME>\",\n                                            \"last_name\": \"<CONTACT_LAST_NAME>\",\n                                            \"middle_name\": \"<CONTACT_MIDDLE_NAME>\",\n                                            \"suffix\": \"<CONTACT_SUFFIX>\",\n                                            \"prefix\": \"<CONTACT_PREFIX>\"\n                                        },\n                                        \"org\": {\n                                            \"company\": \"<CONTACT_ORG_COMPANY>\",\n                                            \"department\": \"<CONTACT_ORG_DEPARTMENT>\",\n                                            \"title\": \"<CONTACT_ORG_TITLE>\"\n                                        },\n                                        \"phones\": [\n                                            {\n                                                \"phone\": \"<CONTACT_PHONE>\",\n                                                \"wa_id\": \"<CONTACT_WA_ID>\",\n                                                \"type\": \"<HOME|WORK>\"\n                                            }\n                                        ],\n                                        \"urls\": [\n                                            {\n                                                \"url\": \"<CONTACT_URL>\",\n                                                \"type\": \"<HOME|WORK>\"\n                                            }\n                                        ]\n                                    }\n                                ]\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following is an example of a contact message you received from a customer:"
        					},
        					"response": []
        				},
        				{
        					"name": "Received Static Location Messages",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"<WHATSAPP_BUSINESS_ACCOUNT_ID>\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<PHONE_NUMBER>\",\n                            \"phone_number_id\": \"<PHONE_NUMBER_ID>\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<NAME>\"\n                                },\n                                \"wa_id\": \"<WHATSAPP_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"PHONE_NUMBER\",\n                                \"id\": \"wamid.ID\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"location\": {\n                                    \"latitude\": \"<LOCATION_LATITUDE>\",\n                                    \"longitude\": \"<LOCATION_LONGITUDE>\",\n                                    \"name\": \"<LOCATION_NAME>\",\n                                    \"address\": \"<LOCATION_ADDRESS>\",\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following is an example of a static location message you received from a customer."
        					},
        					"response": []
        				},
        				{
        					"name": "Received Message Triggered by Click to WhatsApp Ads",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"<PHONE_NUMBER>\",\n                            \"phone_number_id\": \"<PHONE_NUMBER_ID>\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"<PROFILE_NAME>\"\n                                },\n                                \"wa_id\": \"<WA_ID>\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"referral\": {\n                                    \"source_url\": \"<AD_OR_POST_FB_URL>\",\n                                    \"source_id\": \"<AD_ID>\",\n                                    \"source_type\": \"<AD_OR_POST>\",\n                                    \"headline\": \"<AD_TITLE>\",\n                                    \"body\": \"<AD_DESCRIPTION>\",\n                                    \"media_type\": \"<IMAGE_OR_VIDEO>\",\n                                    \"image_url\": \"<RAW_IMAGE_URL>\",\n                                    \"video_url\": \"<RAW_VIDEO_URL>\",\n                                    \"thumbnail_url\": \"<RAW_THUMBNAIL_URL>\"\n                                },\n                                \"from\": \"<SENDER_PHONE_NUMBERID>\",\n                                \"id\": \"wamid.ID\",\n                                \"timestamp\": \"<TIMESTAMP>\",\n                                \"type\": \"text\",\n                                \"text\": {\n                                    \"body\": \"<BODY>\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						}
        					},
        					"response": []
        				},
        				{
        					"name": "Received Unknown Messages",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"1900820329959633\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16315551234\",\n                            \"phone_number_id\": \"16315551234\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"Kerry Fisher\"\n                                },\n                                \"wa_id\": \"16315555555\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"16315555555\",\n                                \"id\": \"wamid.ABGGFlA5FpafAgo6tHcNmNjXmuSf\",\n                                \"timestamp\": \"1602139392\",\n                                \"errors\": [\n                                    {\n                                        \"code\": 130501,\n                                        \"details\": \"Message type is not currently supported\",\n                                        \"title\": \"Unsupported message type\"\n                                    }\n                                ],\n                                \"type\": \"unknown\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "It’s possible to receive an unknown message callback notification. The following is an example of a message you received from a customer that is not supported."
        					},
        					"response": []
        				},
        				{
        					"name": "Message Status Update Notifications",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16505553333\",\n                            \"phone_number_id\": \"27681414235104944\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.gBGGFlCGg0cvAglAxydbAoy-gwNo\",\n                                \"status\": \"sent\",\n                                \"timestamp\": \"1603086313\",\n                                \"recipient_id\": \"16315551234\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The WhatsApp Business API sends notifications to inform you of the status of the messages between you and users. When a message is sent successfully, you receive a notification when the message is sent, delivered, and read. The order of these notifications in your app may not reflect the actual timing of the message status. You can view the timestamp to determine the timing.\n\nThe following is an example of successful message status update Webhooks:"
        					},
        					"response": []
        				},
        				{
        					"name": "Received Callback from a Quick Reply Button Click",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16505553333\",\n                            \"phone_number_id\": \"27681414235104944\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"Kerry Fisher\"\n                                },\n                                \"wa_id\": \"16315551234\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"context\": {\n                                    \"from\": \"16505553333\",\n                                    \"id\": \"wamid.gBGGFlCGg0cvAgkLFm4e9tICiTI\"\n                                },\n                                \"from\": \"16315551234\",\n                                \"id\": \"wamid.ABGGFlCGg0cvAgo-sHWxBA2VFD_S\",\n                                \"timestamp\": \"1603087229\",\n                                \"type\": \"button\",\n                                \"button\": {\n                                    \"text\": \"No\",\n                                    \"payload\": \"No-Button-Payload\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "When your customer clicks on a quick reply button in an [interactive message template](#eed16be1-5e49-4a48-8710-398820b4b5dd), a response is sent. Below is an example of the callback format."
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Sent - User Initiated",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"recipient_id\": \"PHONE_NUMBER\",\n                                \"status\": \"sent\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"origin\": {\n                                        \"type\": \"user_initiated\"\n                                    }\n                                },\n                                \"pricing\": {\n                                    \"pricing_model\": \"CBP\",\n                                    \"billable\": true,\n                                    \"category\": \"user_initiated\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification is received when a business sends a message as part of a [user-initiated conversation](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#how-it-works) (if that conversation did not originate in a free entry point):"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Sent - Business-Initiated",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"recipient_id\": \"PHONE_NUMBER\",\n                                \"status\": \"sent\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"origin\": {\n                                        \"type\": \"business_initated\"\n                                    }\n                                },\n                                \"pricing\": {\n                                    \"pricing_model\": \"CBP\",\n                                    \"billable\": true,\n                                    \"category\": \"business_initated\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification is received when a business sends a message as part of a [business-initiated conversation](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#how-it-works):"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Sent - Business Reply to User",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"status\": \"sent\",\n                                \"timestamp\": TIMESTAMP,\n                                \"recipient_id\": PHONE_NUMBER,\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"origin\": {\n                                        \"type\": \"referral_conversion\"\n                                    }\n                                },\n                                \"pricing\": {\n                                    \"billable\": false,\n                                    \"pricing_model\": \"CBP\",\n                                    \"category\": \"referral_conversion\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						}
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Delivered - User Initiated",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"recipient_id\": \"PHONE_NUMBER\",\n                                \"status\": \"delivered\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"origin\": {\n                                        \"type\": \"business_initiated\"\n                                    }\n                                },\n                                \"pricing\": {\n                                    \"pricing_model\": \"CBP\",\n                                    \"billable\": true\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification is received when a business’ message is delivered and that message is part of a [user-initiated conversation](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#how-it-works) (if that conversation did not originate in a [free entry point](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points)):"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Delivered - Business from User-Initiated",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"status\": \"sent\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"recipient_id\": \"PHONE_NUMBER\",\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"type\": \"referral_conversion\"\n                                },\n                                \"pricing\": {\n                                    \"billable\": false,\n                                    \"pricing_model\": \"CBP\",\n                                    \"category\": \"referral_conversion\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification is received when a business’ message is delivered and that message is part of a [user-initiated conversation](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#how-it-works) (if that conversation did not originate in a [free entry point](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points)):"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Delivered - Business Delivered from User",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"recipient_id\": \"PHONE_NUMBER\",\n                                \"status\": \"delivered\",\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"conversation\": {\n                                    \"id\": \"CONVERSATION_ID\",\n                                    \"expiration_timestamp\": TIMESTAMP,\n                                    \"origin\": {\n                                        \"type\": \"user_initiated\"\n                                    }\n                                },\n                                \"pricing\": {\n                                    \"pricing_model\": \"CBP\",\n                                    \"billable\": true,\n                                    \"category\": \"user_initiated\"\n                                }\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification is received when a business’ message is delivered and that message is part of a [user-initiated conversation](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#how-it-works) originating from a [free entry point](https://developers.facebook.com/docs/whatsapp/pricing/conversationpricing#free-entry-points):"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Deleted",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": PHONE_NUMBER,\n                            \"phone_number_id\": PHONE_NUMBER\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"NAME\"\n                                },\n                                \"wa_id\": PHONE_NUMBER\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": PHONE_NUMBER,\n                                \"id\": \"wamid.ID\",\n                                \"timestamp\": TIMESTAMP,\n                                \"errors\": [\n                                    {\n                                        \"code\": 131051,\n                                        \"details\": \"Message type is not currently supported\",\n                                        \"title\": \"Unsupported message type\"\n                                    }\n                                ],\n                                \"type\": \"unsupported\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "Currently, the Cloud API does not support webhook status updates for deleted messages. If a user deletes a message, you will receive a webhook with an error code for an unsupported message type:"
        					},
        					"response": []
        				},
        				{
        					"name": "Status: Message Failed",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"WHATSAPP_BUSINESS_ACCOUNT_ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": PHONE_NUMBER,\n                            \"phone_number_id\": PHONE_NUMBER_ID\n                        },\n                        \"statuses\": [\n                            {\n                                \"id\": \"wamid.ID\",\n                                \"status\": \"failed\",\n                                \"timestamp\": TIMESTAMP,\n                                \"recipient_id\": PHONE_NUMBER,\n                                \"errors\": [\n                                    {\n                                        \"code\": 131014,\n                                        \"title\": \"Request for url https://URL.jpg failed with error: 404 (Not Found)\"\n                                    }\n                                ]\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "The following notification shows a Webhook with failed status:"
        					},
        					"response": []
        				},
        				{
        					"name": "Received Product Enquiry Message",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"ID\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"PHONE_NUMBER\",\n                            \"phone_number_id\": \"PHONE_NUMBER_ID\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"NAME\"\n                                },\n                                \"wa_id\": \"PHONE_NUMBER_ID\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"PHONE_NUMBER\",\n                                \"id\": \"wamid.ID\",\n                                \"text\": {\n                                    \"body\": \"MESSAGE_TEXT\"\n                                },\n                                \"context\": {\n                                    \"from\": \"PHONE_NUMBER\",\n                                    \"id\": \"wamid.ID\",\n                                    \"referred_product\": {\n                                        \"catalog_id\": \"CATALOG_ID\",\n                                        \"product_retailer_id\": \"PRODUCT_ID\"\n                                    }\n                                },\n                                \"timestamp\": \"TIMESTAMP\",\n                                \"type\": \"text\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						},
        						"description": "A Product Enquiry Message is received when a user is asking for more information about a specific product. These can be received as in two scenarios:<ol><li>When a customer replies to Single or Multi-Product Messages</li><li>When a customer accesses a business’ catalog through another entry point, navigates to a **Product Details Page**, and clicks **Message Business about this Product**.</li></ol>"
        					},
        					"response": []
        				},
        				{
        					"name": "Received Order Messages",
        					"request": {
        						"method": "VIEW",
        						"header": [],
        						"body": {
        							"mode": "raw",
        							"raw": "{\n    \"object\": \"whatsapp_business_account\",\n    \"entry\": [\n        {\n            \"id\": \"8856996819413533\",\n            \"changes\": [\n                {\n                    \"value\": {\n                        \"messaging_product\": \"whatsapp\",\n                        \"metadata\": {\n                            \"display_phone_number\": \"16505553333\",\n                            \"phone_number_id\": \"phone-number-id\"\n                        },\n                        \"contacts\": [\n                            {\n                                \"profile\": {\n                                    \"name\": \"Kerry Fisher\"\n                                },\n                                \"wa_id\": \"16315551234\"\n                            }\n                        ],\n                        \"messages\": [\n                            {\n                                \"from\": \"16315551234\",\n                                \"id\": \"wamid.ABGGFlCGg0cvAgo6cHbBhfK5760V\",\n                                \"order\": {\n                                    \"catalog_id\": \"the-catalog_id\",\n                                    \"product_items\": [\n                                        {\n                                            \"product_retailer_id\": \"the-product-SKU-identifier\",\n                                            \"quantity\": \"number-of-item\",\n                                            \"item_price\": \"unitary-price-of-item\",\n                                            \"currency\": \"price-currency\"\n                                        }\n                                    ],\n                                    \"text\": \"text-message-sent-along-with-the-order\"\n                                },\n                                \"context\": {\n                                    \"from\": \"16315551234\",\n                                    \"id\": \"wamid.gBGGFlaCGg0xcvAdgmZ9plHrf2Mh-o\"\n                                },\n                                \"timestamp\": \"1603069091\",\n                                \"type\": \"order\"\n                            }\n                        ]\n                    },\n                    \"field\": \"messages\"\n                }\n            ]\n        }\n    ]\n}",
        							"options": {
        								"raw": {
        									"language": "json"
        								}
        							}
        						}
        					},
        					"response": []
        				}
        			],
        			"description": "Webhooks are user-defined HTTP callbacks that are triggered by specific events. Whenever that trigger event occurs, the WhatsApp Business API client captures the event, collects the data, and immediately sends a notification (HTTPs request) to the Webhook URL configured in the Webhooks setup step.\n\nFor the purposes of this use case, your Webhooks server must be reachable from facebook and have HTTPs support and a valid SSL certificate. See [Webhooks, Getting Started](https://developers.facebook.com/docs/graph-api/webhooks/getting-started) for requirements on creating a Webhooks endpoint and configuring the Webhooks product.\n\nTo get Webhooks notifications, your application must subscribe to the WABA you want to receive alerts for. For a full guide, see [Webhooks for WhatsApp Business Accounts](https://developers.facebook.com/docs/graph-api/webhooks/getting-started/webhooks-for-whatsapp).",
        			"auth": {
        				"type": "noauth"
        			},
        			"event": [
        				{
        					"listen": "prerequest",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				},
        				{
        					"listen": "test",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				}
        			]
        		},
        		{
        			"name": "Support and Troubleshooting",
        			"item": [
        				{
        					"name": "Support",
        					"item": [],
        					"description": "For support for WhatsApp Business Platform APIs, see [Meta for Developer Support](https://developers.facebook.com/support/).\n\n## WhatsApp Business Platform Status Page\n\nVisit the [WhatsApp Business Platform status page](https://l.facebook.com/l.php?u=https://status.fb.com/whatsapp-business-api&h=AT2r6jHfssraigkl_d4AIwRn-a70dgYIRjgzxIlFIXnvRpKPffREAL48kaXUmb4oHe3v62l80-Vv3Y13gHPknTKxBGoqxTB0CPRYWzyQSiL41P8ciFPlSARp9uurGeo0vU2b6XkEUXyWSG_X8dF8qpVL) for the latest information on whether the API is experiencing any platform outages.",
        					"auth": {
        						"type": "noauth"
        					},
        					"event": [
        						{
        							"listen": "prerequest",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						},
        						{
        							"listen": "test",
        							"script": {
        								"type": "text/javascript",
        								"exec": [
        									""
        								]
        							}
        						}
        					]
        				},
        				{
        					"name": "Errors",
        					"item": [
        						{
        							"name": "Sample Error Response",
        							"request": {
        								"method": "VIEW",
        								"header": [],
        								"body": {
        									"mode": "raw",
        									"raw": "{\n  \"error\": {\n    \"message\": \"(#131006) Resource not found\", // error description\n    \"type\": \"OAuthException\",\n    \"code\": 131006, // please refer to the Error Codes section\n    \"error_data\": {\n        \"messaging_product\": \"whatsapp\", \n        \"details\": \"unknown contact\" // detailed error message to help with debugging\n    },\n    \"error_subcode\": 2494007,\n    \"fbtrace_id\": \"Az8or2yhqkZfEZ-_4Qn_Bam\"\n  }\n}",
        									"options": {
        										"raw": {
        											"language": "json"
        										}
        									}
        								},
        								"url": {
        									"raw": ""
        								},
        								"description": "#### Error Message Fields\n\n| Field         | Description   |\n| ------------- | ------------- |\n| **`error.code`**    | Please refer to [Error Codes section](https://developers.facebook.com/docs/whatsapp/cloud-api/support/error-codes#error-codes) for more information |\n| **`error.type`**    | The type of error\n| **`error.message`** | The title of this error |\n| **`error.error_data.messaging_product`** | The messaging product being used. In this case, it's `\"whatsapp\"`. |\n| **`error.error_data.details`** | Describes the detailed error messages to help you debug the error. <br> If you cannot fix the errors by yourself, please [contact us](https://developers.facebook.com/docs/whatsapp/api/support/) with both request, payload and error response to investigate. |\n| **`error.error_subcode`** | The subcode of the error |\n| **`fbtrace_id`** | Please include this when reaching out to [Support](https://developers.facebook.com/docs/whatsapp/api/support/) |"
        							},
        							"response": []
        						}
        					],
        					"description": "This section contains information relating to error messages. This includes a list of all error codes and an [example error message response in JSON](#ab9e0327-0c81-4f6a-b068-0e6102363f23).\n\nIf an error is blocking you and you cannot fix it, contact us with both the request payload and error response so we can investigate and provide support.\n\n[Click here to see a list of all error codes](https://developers.facebook.com/docs/whatsapp/cloud-api/support/error-codes#error-codes)."
        				},
        				{
        					"name": "General FAQs",
        					"item": [],
        					"description": "**Q: Will all features from the On-Premises API be available on the Cloud API?**\n\nA: We aim to have parity between the on-premises and Cloud APIs starting in 2022. At the beginning of the beta in October 2021, we will have parity with on-premises version 2.33 with the exception of audio, video, List Messages & Stickers. Audio and video messages will become available in November 2021, and List Messages & Stickers will not be available for the beta.\n\n* * *\n\n**Q: Which company will be providing Cloud API?**\n\nA: The Cloud API will be operated by Meta which provides the [Graph API](https://developers.facebook.com/docs/graph-api/), though which developers will access the WhatsApp Business API. WhatsApp will continue to provide the underlying business messaging routing service that it provides for the On-Premise solution.\n\n* * *\n\n**Q: How will Meta use the WhatsApp messages that are shared with it by a business?**\n\nA: Meta will not use the messages to inform the ads that a person sees. However, as is always the case, businesses can use chats they receive for their own marketing purposes, which may include advertising on Meta or other channels, like email or TV.\n\n* * *\n\n**Q: Does introduction of a cloud-Cloud API mean WhatsApp is deprecating the existing On-Premise API?**\n\nA: No, we will continue to provide the On-Premise API for now.\n\n* * *\n\n**Q: Are there any additional costs for the Cloud API?**\n\nA: No, there is no difference in prices between the Cloud API and the On-Premise API. We expect the Cloud API to generate cost savings for developers. The two types of cost savings for the Cloud API are:\n\n1.  Set up cost (including servers or AWS cost)\n2.  Ongoing cost of maintenance (including engineering time for API upgrades).\n    \n\n* * *\n\n**Q: How can a Business Solutions Provider decide whether a given client should use the On-Premise or Cloud API? Do clients get a choice?**\n\nA: A Business Solutions Provider can select which setup a given client should use. We recommend that the majority of clients use the Cloud API for ease of implementation and maintenance.\n\n* * *\n\n**Q: Will consumers know whether a business is using the Cloud API or the On-Premise API?**\n\nA: Across all our services, WhatsApp makes the end-to-end encryption status of a chat clear. To ensure people know how a business chooses to store and manage their messages with customers, we plan to indicate in our in-app system messages whether businesses have chosen Meta or another company to help store and manage their chats. This will be true for both businesses using the Cloud API, and those working with a BSP using the on-premise API. Conversations in end-to-end encrypted chats will continue to be clearly labeled with a gold system message."
        				},
        				{
        					"name": "Technical Implementation FAQs",
        					"item": [],
        					"description": "**Q: What is the architecture of the Cloud API?**\n\nA: The Cloud API architecture significantly simplifies the BSP’s operational and infrastructure requirements to integrate with WhatsApp Business API. First, it removes the infrastructure requirements to run Business API docker containers (CAPEX savings). Second, it obviates the need of operational responsibilities to manage the deployment (OPEX savings). For details, refer to the architecture diagram comparing the on-premise and Cloud API deployments.\n\n* * *\n\n**Q: Will the BSP/direct client need containers and what data will be in those containers?**\n\nA: BSPs and direct clients will not need the WebApp and CoreApp containers that are used in the on-premise product. Meta will manage all database data and media data on behalf of the BSP or direct client.\n\n* * *\n\n**Q: What will disaster recovery look like: if a region is unavailable, how much time does it take to move messages to another region?**\n\nA: We will have disaster recovery and data replication across multiple regions. The expected downtime would be within our SLA and usually in the order of less than a minute to less than five minutes.\n\n* * *\n\n**Q: How does WhatsApp recommend I conduct load tests for the Cloud API?**\n\nA: As your on-premises performance depends heavily on your hardware, software, and connectivity to WhatsApp servers, if you wish to understand these differences, you can perform your own load tests on Cloud API as you might have done for your own on-premises installation. You can also refer to our performance comparison below to understand more details around how the on-premise and Cloud APIs compare."
        				},
        				{
        					"name": "Data Privacy & Security FAQs",
        					"item": [],
        					"description": "**Q: Where are the servers for the Cloud API?**\n\nA: Meta has global infrastructure. At this time, the Meta servers operating the API will be located in North America.\n\n* * *\n\n**Q: Is the Cloud API end-to-end encrypted? What is the encryption model?**\n\nA: For information relating to encryption, see [Cloud API Overview, Encryption](#69be62af-c7c0-4c18-ace9-5b80e655c7ba).\n\n* * *\n\n**Q: What happens to data at rest? How is it handled? How long is it stored?**  \nA: Messages at rest are encrypted. They are automatically deleted after 30 days.\n\n* * *\n\n**Q: Does Meta have access to encryption keys?**\n\nA: In order to send and receive messages through Cloud APIs, Cloud API software manages the encryption/decryption keys on behalf of the business. For Cloud API Alpha, WhatsApp will be operating the Cloud API as it is still under development and therefore WhatsApp will have access to message keys for phone numbers using Cloud API. For the Cloud API Beta and GA launches, Meta will operate Cloud API (instead of WhatsApp) and therefore, WhatsApp will not have access to keys nor messages.\n\n* * *\n\n**Q: What is the best way to complete vendor/supplier due diligence on the WhatsApp Business API?**\n\nA: If you need to document WhatsApp’s security and compliance positions, we recommend that you refer to our CSA Consensus Assessments Initiative Questionnaire, which we will share via email in early December. It offers an industry-accepted way to document what security controls exist in the Cloud API, providing security control transparency. It provides a set of questions that the Cloud Security Alliance anticipates a cloud consumer or auditor would ask a cloud provider. We document WhatsApp’s answers to the questionnaire, which should provide a basis for your security, control, and process review.\n\n* * *\n\n**Q: What is the security model? Which certifications does the WhatsApp Business API have?**\n\nA: We have obtained SOC 2 Type I certification and are pursuing SOC 2 Type II. You can refer to the Consensus Assessments Initiative Questionnaire to understand the security model."
        				},
        				{
        					"name": "Regulatory Compliance FAQs",
        					"item": [],
        					"description": "**Q: Will the Cloud API be compliant with regional data protection laws such as GDPR, LGPD, PDPB etc.?**\n\nA: We care deeply about protecting your data and we are building the Cloud API with security in mind. We plan to share the specific details about Meta's data practices in the terms that will accompany the Cloud API when it launches in beta next year. \n\n----------\n\n**Q: What does WhatsApp plan to do to make sure the transfer of data from the E.U. to the US is compliant?**\n\nA: On July 16 2020, the Court of Justice of the European Union (CJEU) decided that the EU-US Privacy Shield is invalid because of concerns about the adequacy of data protection in the US. Our new Business Terms of Service now incorporates Standard Contractual Clauses (replacing the US-EU and US-Swiss Privacy Shield).\n\nWe welcome the decision of the Court of Justice of the European Union to confirm the validity of Standard Contractual Clauses for transfers of data to non-EU countries. Like many businesses, WhatsApp uses Standard Contractual Clauses as a data transfer mechanism from the European Economic Area to the United States in the context of the services we provide to businesses - the WhatsApp Business App and the WhatsApp Business API.\n\nWe will ensure that our businesses and partners can continue to enjoy WhatsApp’s business solutions while keeping their data safe and secure.\n\n<!-- ----------\n\n**Q: Does WhatsApp disclose data to governmental authorities and agencies?**\n\nA: No, we don't give governments or anyone else a \"backdoor\" into our systems. -->"
        				}
        			],
        			"description": "This section describes how to troubleshoot errors and contains FAQs relating to general questions, technical implementation, data privacy & security, and regulatory compliance.",
        			"auth": {
        				"type": "noauth"
        			},
        			"event": [
        				{
        					"listen": "prerequest",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				},
        				{
        					"listen": "test",
        					"script": {
        						"type": "text/javascript",
        						"exec": [
        							""
        						]
        					}
        				}
        			]
        		}
        	],
        	"auth": {
        		"type": "bearer",
        		"bearer": [
        			{
        				"key": "password",
        				"value": "{{User-Access-Token}}",
        				"type": "string"
        			},
        			{
        				"key": "token",
        				"value": "{{User-Access-Token}}",
        				"type": "string"
        			}
        		]
        	},
        	"event": [
        		{
        			"listen": "prerequest",
        			"script": {
        				"type": "text/javascript",
        				"exec": [
        					""
        				]
        			}
        		},
        		{
        			"listen": "test",
        			"script": {
        				"type": "text/javascript",
        				"exec": [
        					""
        				]
        			}
        		}
        	],
        	"variable": [
        		{
        			"key": "Version",
        			"value": "v15.0"
        		},
        		{
        			"key": "Phone-Number-ID",
        			"value": "113037821608895"
        		},
        		{
        			"key": "User-Access-Token",
        			"value": "EAALuL1ZAZBrfMBAKDEw5M7jplWwBK7yphuaaQVntcoiQMyr8NZBwRwv76wea5RFvLNZAWFYE66oJ47wdkECD0Njw91yM8uXGVHqyvRsPVOJMmpXOBcfcAKSHbYRdhGei3HrrCZA6yVMy4UiSa4TWMFEragyucBpbr1paMFTWZAHcTcBnYPAZCZBDKLNchltPlAxhba7PbGgWlhA8w5AkUZBZAQ"
        		}
        	]
        }
        """";
}
