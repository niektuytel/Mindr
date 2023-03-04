using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Interfaces;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.WebUI.Components
{
    public partial class HttpPipelineItem: FluentComponentBase
    {
        [Parameter, EditorRequired]
        public HttpItem Data { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnHandleSelect { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnHandleRemove { get; set; } = default!;

        [Parameter]
        public bool IsSelected { get; set; } = false;

        private bool HasEmptyVariable()
        {
            if (Data.Request.Variables == null) return false;

            var emptyVars = Data.Request.Variables.Where(value => string.IsNullOrEmpty(value.Value));
            return emptyVars.Any();
        }

        private string GetMethodStyle()
        {
            var style = "border-right: ridge;padding-right:5px;margin-right:5px;font-weight: bold;";
            var value = Data.Request.Method.ToLower();
            if (value == "get")
            {
                return $"{style}color: green;";
            }

            if (value == "post")
            {
                return $"{style}color: orange;";
            }

            if (value == "delete")
            {
                return $"{style}color: red;";
            }

            return style;
        }
        
        private string GetStatusCodeStyle()
        {
            if (Data.Result == null) return "";

            return Data.Result.IsSuccessStatusCode ? "highlight" : "lowlight";
        }
    
     }
}
