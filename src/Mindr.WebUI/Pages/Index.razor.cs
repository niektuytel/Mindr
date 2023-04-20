
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebUI.Extensions;

namespace Mindr.WebUI.Pages
{
    public partial class Index : FluentComponentBase
    {
        private string? _code { get; set; }

        private string? _scope { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _code = NavigationManager.ExtractQueryStringByKey<string>("code");
            _scope = NavigationManager.ExtractQueryStringByKey<string>("scope");

            if(!string.IsNullOrEmpty(_scope))
            {
                var clientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
                var clientSecret = "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";
                var redirectUrl = "https://localhost:7247";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.google.com/o/oauth2/token");
                //request.Headers.Add("Cookie", "__Host-GAPS=1:rOOUtDlZSZJ9b9EI8yPM8tZu50YdJg:AIg8s92xqiIblbhp");
                var content = new MultipartFormDataContent
                {
                    { new StringContent("authorization_code"), "grant_type" },
                    { new StringContent(_code), "code" },
                    { new StringContent(clientId), "client_id" },
                    { new StringContent(clientSecret), "client_secret" },
                    { new StringContent(redirectUrl), "redirect_uri" }
                };

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }

        }

        public string GetGoogleCalendarConsent()
        {
            //?code=4%2F0AVHEtk6AjhTY5PTwvGAf2JQTYmfCgipI2akZRaPNlhQXgChMvbJSx2XzZtyipse3NtlWrA&scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fcalendar.readonly

            var scopes = "https://www.googleapis.com/auth/calendar.readonly";
            var redirectUrl = "https://localhost:7247";
            var clientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
            var url = $"https://accounts.google.com/o/oauth2/v2/auth?scope={scopes}&response_type=code&access_type=offline&redirect_uri={redirectUrl}&client_id={clientId}";

            return url;
        }

    }
}
