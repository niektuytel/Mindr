using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Mindr.Core.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Identity.Client;
using Microsoft.Graph;

namespace Mindr.WebUI.Services;

public class MicrosoftCalendarEventsProvider: IMicrosoftCalendarEventsProvider
{
    // Get Access token clientFactory;
    private readonly HttpClient _httpClient;
    private readonly GraphClientFactory _graphClientFactory;

    private const string BASE_URL = "https://graph.microsoft.com/v1.0/me/events";

    public MicrosoftCalendarEventsProvider(GraphClientFactory clientFactory, IHttpClientFactory HttpClientFactory)
    {
        _graphClientFactory = clientFactory;
        _httpClient = HttpClientFactory.CreateClient("Default");
    }

    public async Task<IEnumerable<Event>> GetEventsInMonthAsync(int year, int month)
    {
        //// 1- Get Token 
        //var accessToken = await GetAccessTokenAsync();
        //if(accessToken == null)
        //    return null;

        //// 2- Set the access token in the authorization header 
        //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);


        var _graphclient = _graphClientFactory.GetAuthenticatedClient();
        //var _graphclient = clientFactory.GetAuthenticatedClient();
        var events = await _graphclient.Me.Events.Request().GetAsync();


        // 3-  Send the request 
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
        return events.CurrentPage;
    }

    //private async Task<string> GetAccessTokenAsync()
    //{
    //    try
    //    {


    //        //// Create a new PublicClientApplication instance
    //        //var pca = PublicClientApplicationBuilder
    //        //    .Create(msalProviderOptions.ClientId)
    //        //    .WithAuthority(msalProviderOptions.DefaultAuthority)
    //        //    .Build();

    //        //// Get the account of the currently logged in user
    //        //var accounts = await pca.GetAccountsAsync();
    //        //var account = accounts.FirstOrDefault();

    //        //if (account != null)
    //        //{
    //        //    // Create a new AuthenticationResult instance with the new scopes
    //        //    var result = await pca.AcquireTokenSilent(msalProviderOptions.DefaultScopes.Concat(new[] { "Mail.Read" }).ToArray(), account)
    //        //        .ExecuteAsync();

    //        //    // Use the new access token
    //        //    // ...
    //        //}
    //        //else
    //        //{
    //        //    // The user is not logged in, redirect to the login page
    //        //    navigationManager.NavigateTo("authentication/login");
    //        //}




    //        var tokenRequest = await _graphclient.RequestAccessToken(new AccessTokenRequestOptions
    //        {
    //            Scopes = new[] { "https://graph.microsoft.com/Calendars.Read" }
    //        });

    //        // Try to fetch the token 
    //        if(tokenRequest.TryGetToken(out var token))
    //        {
    //            Console.WriteLine(token.Value);
    //            if (token != null)
    //            {
    //                return token.Value;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }


    //    return null; 
    //}

    private string ConstructGraphUrl(int year, int month)
    {
        var lastDayInMonth = DateTime.DaysInMonth(year, month);
        return $"{BASE_URL}?$filter=start/datetime ge '{year}-{month}-01T00:00' and end/dateTime le '{year}-{month}-{lastDayInMonth}T00:00'&$select=subject,start,end";
    }
}