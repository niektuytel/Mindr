using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.WebUI.Components;
using Mindr.WebUI.Models;
using System;

namespace Mindr.WebUI.Pages
{
    public partial class ConnectorPage: FluentComponentBase
    {

        [Parameter]
        public string? ConnectorId { get; set; }

        [Parameter]
        public string? ConnectorNav { get; set; }

        
        public List<Page> GetNavigation => new List<Page>()
        {
            new Page(){
                Name = "Overview",
                Icon = @FluentIcons.Home,
                Href = $"/connectors/{ConnectorId}/",
            },
            new Page(){
                Name = "Pipeline",
                Icon = @FluentIcons.Pipeline,
                Href = $"/connectors/{ConnectorId}/pipeline",
            },
            new Page(){
                Name = "Output",
                Icon = @FluentIcons.StreamOutput,
                Href = $"/connectors/{ConnectorId}/output",
                UseBreadcrumb = false,
                Disabled = true
            },
            new Page(){
                Name = "History",
                Icon = @FluentIcons.History,
                Href = $"/connectors/{ConnectorId}/history",
                UseBreadcrumb = false,
                Disabled = true
            },
            new Page(){
                Name = "Events",
                Icon = @FluentIcons.ArrowStepOver,
                Href = $"/connectors/{ConnectorId}/events",
                UseBreadcrumb = false,
                Disabled = true
            }
        };

        public IQueryable<Connector> Connectors = new[]
        {
            new Connector(1, 10, "Connector 1", "description 1", "pending", "some json response 1"),
            new Connector(2, 20, "Connector 2", "description 2", "running", "some json response 2"),
            new Connector(3, 30, "Connector 3", "description 3", "waiting", "some json response 3"),
            new Connector(4, 40, "Connector 4", "description 4", "canceled", "some json response 4")
        }.AsQueryable();

        public IQueryable<SwaggerCollection> Pipelines = new[]
        {
            new SwaggerCollection(),
            //new SwaggerItem(2, 20, "Connector 2", "description 2", "running", "some json response 2"),
            //new SwaggerItem(3, 30, "Connector 3", "description 3", "waiting", "some json response 3"),
            //new SwaggerItem(4, 40, "Connector 4", "description 4", "canceled", "some json response 4")
        }.AsQueryable();


        public async Task OnCreateHandler()
        {
            Console.WriteLine();
        }

        public async Task OnUpdateHandler()
        {
            Console.WriteLine();
        }

        public async Task OnDeleteHandler()
        {
            Console.WriteLine();
        }


        public void OnClick(MouseEventArgs e)
        {
            Console.WriteLine();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {

            }
            base.OnAfterRender(firstRender);
        }

    }
}
