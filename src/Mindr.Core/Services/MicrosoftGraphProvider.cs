using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Mindr.Core.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using Mindr.Core.Interfaces;
using Microsoft.Graph;

namespace Mindr.Core.Services
{
    public class MicrosoftGraphProvider: IMicrosoftGraphProvider
    {
        // Get Access token 
        //private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly HttpClient _httpClient;
        private readonly GraphServiceClient _graphClient;

        private const string BASE_URL = "https://graph.microsoft.com/v1.0/me/events";

        public MicrosoftGraphProvider(GraphServiceClient graphClient, IHttpClientFactory HttpClientFactory)
        {
            _graphClient = graphClient;
            //_accessTokenProvider = accessTokenProvider;
            _httpClient = HttpClientFactory.CreateClient("Default");
        }

        public async Task<IEnumerable<AgendaEvent>> GetEventsAsync(string objectId, int year, int month)
        {
            var items = new List<AgendaEvent>();

            //var request = await _graphClient.Users//[objectId]
            //    .Request()
            //    .WithAppOnly()
            //    //.Filter($"objectid eq '{objectId}'")
            //    .GetAsync();

            //Console.WriteLine(  );
            //// Get the AadGuestPft token
            //var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "Calendars.Read" });

            //// Create a GraphServiceClient instance
            //var graphClient = new GraphServiceClient(
            //    new DelegateAuthenticationProvider(async requestMessage =>
            //    {
            //        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //    })
            //);


            //// 1- Get Token 
            //var accessToken = await GetAccessTokenAsync();
            //if(accessToken == null)
            //    return null;

            //// 2- Set the access token in the authorization header 
            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            //// 3-  Send the request 
            //var url = ConstructGraphUrl(year, month);
            //var response = await _httpClient.GetAsync(url);

            //if(!response.IsSuccessStatusCode)
            //{
            //    return null;
            //}

            //// 4- Read the content 
            //var contentAsString = await response.Content.ReadAsStringAsync(); 

            //var microsoftEvents = JsonConvert.DeserializeObject<GraphEventsResponse>(contentAsString);

            //// Convert the Microsoft Event object into CalendarEvent object
            //var events = microsoftEvents.Value;
            //return events;
            return items;
        }

        private async Task<string> GetAccessTokenAsync()
        {

            //try
            //{
            //    var tokenRequest = await _accessTokenProvider.RequestAccessToken(new AccessTokenRequestOptions
            //    {
            //        Scopes = new[] { "https://graph.microsoft.com/Calendars.ReadWrite" }
            //    });

            //    // Try to fetch the token 
            //    if (tokenRequest.TryGetToken(out var token))
            //    {
            //        await Console.Out.WriteLineAsync(token.Value);
            //        if (token != null)
            //        {
            //            return token.Value;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}


            return null; 
        }

        private string ConstructGraphUrl(int year, int month)
        {
            var lastDayInMonth = DateTime.DaysInMonth(year, month);
            return $"{BASE_URL}?$filter=start/datetime ge '{year}-{month}-01T00:00' and end/dateTime le '{year}-{month}-{lastDayInMonth}T00:00'&$select=subject,start,end";
        }
    }
}