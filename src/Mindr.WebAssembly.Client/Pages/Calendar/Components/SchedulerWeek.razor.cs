using Mindr.WebAssembly.Client.Pages.Calendar.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Mindr.WebAssembly.Client.Pages.Calendar.Services;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
	public partial class SchedulerWeek
    {
        [CascadingParameter] public Scheduler Scheduler { get; set; } = null!;

        [Inject]
        public CalendarViewTypeService CalendarViewTypeService { get; set; } = default!;
        
        [Parameter] public DateTime Start { get; set; }
        [Parameter] public DateTime End { get; set; }
        [Parameter] public IEnumerable<Appointment> Appointments { get; set; } = null!;

		private int MaxNumOfAppointmentsPerDay => Scheduler.MaxVisibleAppointmentsPerDay;
        private int MaxVisibleAppointmentsThisWeek
        {
            get
            {
                int max = 0;
                for(var dt = Start; dt <= End; dt = dt.AddDays(1))
                {
                    var appCount = Appointments.Where(x => dt.Between(x.Data.StartDate.GetDateTime().Date, x.Data.EndDate.GetDateTime().Date)).Count();
                    max = Math.Max(max, appCount);
                }
                return Math.Min(max, MaxNumOfAppointmentsPerDay);
            }
        }

        private readonly Dictionary<Appointment, int> _orderings = new();
        private readonly Dictionary<Appointment, (int, int)> _startsAndEnds = new();

        protected override void OnInitialized()
        {
            foreach (var app in Appointments)
            {
                _startsAndEnds[app] = GetStartAndEndDayForAppointment(app);
            }
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            _orderings.Clear();
            foreach (var app in Appointments)
            {
                _orderings[app] = GetBestOrderingForAppointment(app);
            }

            base.OnParametersSet();
        }

        private (int, int) GetStartAndEndDayForAppointment(Appointment appointment)
        {
            DayOfWeek schedStart = Scheduler.StartDayOfWeek;
            DayOfWeek start = schedStart, end = schedStart + 6;

            if (!(appointment.Data.StartDate.GetDateTime().Date, appointment.Data.EndDate.GetDateTime().Date).Overlaps((Start, End)))
                return ((int)appointment.Data.StartDate.GetDateTime().DayOfWeek, (int)appointment.Data.EndDate.GetDateTime().DayOfWeek);

            if (appointment.Data.StartDate.GetDateTime().Date.Between(Start, End))
            {
                start = appointment.Data.StartDate.GetDateTime().DayOfWeek;
                end = appointment.Data.EndDate.GetDateTime().Date.Between(Start, End) ? appointment.Data.EndDate.GetDateTime().DayOfWeek : schedStart - 1;
            }
            else if (appointment.Data.EndDate.GetDateTime().Date.Between(Start, End))
            {
                start = schedStart;
                end = appointment.Data.EndDate.GetDateTime().DayOfWeek;
            }

            return ((start - schedStart + 7) % 7, (end - schedStart + 7) % 7);
        }

        private int GetBestOrderingForAppointment(Appointment appointment)
        {
            return _orderings
                .Where(x => x.Key != Scheduler.DraggingAppointment)
                .Where(x => _startsAndEnds[appointment].Overlaps(_startsAndEnds[x.Key]))
                .OrderBy(x => x.Value)
                .TakeWhile((x, i) => x.Value == ++i)
                .LastOrDefault().Value + 1;
        }
    }
}
