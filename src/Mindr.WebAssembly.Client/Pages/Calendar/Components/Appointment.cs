using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Mindr.Domain.Models.DTO.Calendar;
using System;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class Appointment : ComponentBase, IDisposable
    {
        [CascadingParameter] public Scheduler Scheduler { get; set; } = null!;

        [Parameter] public RenderFragment<AppointmentContext>? ChildContent { get; set; }

        [Parameter] public Func<Task>? OnClick { get; set; }
        [Parameter] public Func<DateTime, DateTime, Task>? OnReschedule { get; set; }
        [Parameter, EditorRequired] public CalendarAppointment Data { get; set; } = new();

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            Scheduler.AddAppointment(this);
            Data.Color ??= Scheduler.ThemeColor;

            base.OnInitialized();
        }

        public RenderFragment? RenderChildContent() => ChildContent?.Invoke(new AppointmentContext(this));

        public void Click(MouseEventArgs _)
        {
            OnClick?.Invoke();
        }

        public void Dispose()
        {
            Scheduler.RemoveAppointment(this);
            GC.SuppressFinalize(this);
        }
    }
}
