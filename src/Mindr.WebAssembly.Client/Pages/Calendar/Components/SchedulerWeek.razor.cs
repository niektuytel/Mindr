using Mindr.WebAssembly.Client.Pages.Calendar.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
	public partial class SchedulerWeek
    {
        [CascadingParameter] public Scheduler Scheduler { get; set; } = null!;

        [Parameter] public bool FullHeight { get; set; } = true;
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
                    var appCount = Appointments.Where(x => dt.Between(x.Data.StartDate.DateTime.Date, x.Data.EndDate.DateTime.Date)).Count();
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

            if (!(appointment.Data.StartDate.DateTime.Date, appointment.Data.EndDate.DateTime.Date).Overlaps((Start, End)))
                return ((int)appointment.Data.StartDate.DateTime.DayOfWeek, (int)appointment.Data.EndDate.DateTime.DayOfWeek);

            if (appointment.Data.StartDate.DateTime.Date.Between(Start, End))
            {
                start = appointment.Data.StartDate.DateTime.DayOfWeek;
                end = appointment.Data.EndDate.DateTime.Date.Between(Start, End) ? appointment.Data.EndDate.DateTime.DayOfWeek : schedStart - 1;
            }
            else if (appointment.Data.EndDate.DateTime.Date.Between(Start, End))
            {
                start = schedStart;
                end = appointment.Data.EndDate.DateTime.DayOfWeek;
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
