using Mindr.WebAssembly.Client.Pages.Calendar.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Mindr.WebAssembly.Client.Pages.Calendar.Services;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class Scheduler : IAsyncDisposable
    {
        [Parameter, EditorRequired] public Func<Task> OnHamburgerClick { get; set; } = default!;

        [Inject]
        public CalendarService CalendarService { get; set; } = default!;

        [Inject]
        public CalendarViewTypeService CalendarViewTypeService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; } = default!;



        [Parameter] public RenderFragment Appointments { get; set; } = null!;
        [Parameter] public RenderFragment<DateTime>? DayTemplate { get; set; }

        [Parameter] public Func<DateTime, DateTime, Task>? OnRequestNewData { get; set; }
        [Parameter] public Func<DateTime, DateTime, Task>? OnAddingNewAppointment { get; set; }
        [Parameter] public Func<DateTime, Task>? OnOverflowAppointmentClick { get; set; }

        #region Config
        [Parameter] public bool AlwaysShowYear { get; set; } = true;
        [Parameter] public int MaxVisibleAppointmentsPerDay { get; set; } = 5;
        [Parameter] public bool EnableDragging { get; set; } = true;
        [Parameter] public bool EnableAppointmentsCreationFromScheduler { get; set; } = true;
        [Parameter] public bool EnableRescheduling { get; set; }
        [Parameter] public string ThemeColor { get; set; } = "aqua";
        [Parameter] public DayOfWeek StartDayOfWeek { get; set; } = DayOfWeek.Sunday;
        [Parameter] public string TodayButtonText { get; set; } = "Today";
        [Parameter] public string PlusOthersText { get; set; } = "+ {n} others";
        [Parameter] public string NewAppointmentText { get; set; } = "New Appointment";

        public Appointment? DraggingAppointment { get; private set; }
        //public CalendarMenu Menu { get; set; } = default!;

        #endregion

        public DateTime CurrentDate { get; private set; } = DateTime.Now;

        public (DateTime Start, DateTime End) CurrentMonthRange
        {
            get
            {
                var startDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1).GetPrevious(StartDayOfWeek);
                var endDate = new DateTime(CurrentDate.Year, CurrentDate.Month, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month))
                    .GetNext((DayOfWeek)((int)(StartDayOfWeek - 1 + 7) % 7));

                return (startDate, endDate);
            }
        }

        public (DateTime Start, DateTime End) CurrentWeekRange
        {
            get
            {
                var startDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day).GetPrevious(StartDayOfWeek);
                var endDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day).GetNext((DayOfWeek)((int)(StartDayOfWeek - 1 + 7) % 7));

                return (startDate, endDate);
            }
        }

        public (DateTime Start, DateTime End) CurrentDayRange
        {
            get
            {
                var startDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day);
                var endDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day).AddDays(1);

                return (startDate, endDate);
            }
        }

        private string _dateDisplay
        {
            get
            {
                if (CalendarViewTypeService.IsDay())
                {
                    var res = CurrentDate.Day.ToString();
                    return res += CurrentDate.ToString(" MMMM");
                }
                else if (CalendarViewTypeService.IsWeek())
                {
                    var res = $"{CurrentWeekRange.Start.Day}-{CurrentWeekRange.End.Day}";
                    return res += CurrentDate.ToString(" MMMM");
                }
                else if (CalendarViewTypeService.IsMonth())
                {
                    var res = CurrentDate.ToString("MMMM");
                    if (CurrentDate.Year != DateTime.Today.Year)
                    {
                        return res += CurrentDate.ToString(" yyyy");
                    }

                    return res;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }


        private readonly ObservableCollection<Appointment> _appointments = new();
        private DotNetObjectReference<Scheduler> _objReference = null!;
        private bool _loading = false;

        public bool _showNewAppointment;
        private DateTime? _draggingAppointmentAnchor;
        private DateTime? _draggingStart, _draggingEnd;

        protected override async Task OnInitializedAsync()
        {
            CalendarService.OnChange += CalendarStateChanged;
            CalendarViewTypeService.OnChange += CalendarStateChanged;
            _objReference = DotNetObjectReference.Create(this);

            base.StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AttachMouseHandler();
                await LoadDateRangeData();
            }

            base.OnAfterRender(firstRender);
        }

        private async void CalendarStateChanged()
        {
            // This will trigger LoadDateRangeData
            await LoadDateRangeData();

            // And then trigger a re-render of the UI
            StateHasChanged();
        }

        internal void AddAppointment(Appointment appointment)
        {
            _appointments.Add(appointment);
            StateHasChanged();
        }

        internal void RemoveAppointment(Appointment appointment)
        {
            _appointments.Remove(appointment);
            StateHasChanged();
        }

        public async Task SetCurrentMonth(DateTime today, bool skipJsInvoke = false)
        {
            CurrentDate = today;
            if (!skipJsInvoke)
            {
                await AttachMouseHandler();
            }

            var (start, end) = CurrentMonthRange;
            if (OnRequestNewData != null)
            {
                _loading = true;
                StateHasChanged();
                await OnRequestNewData(start, end);
                _loading = false;
            }
            StateHasChanged();
        }

        private async Task SetCurrentWeek(DateTime today, bool skipJsInvoke = false)
        {
            CurrentDate = today;
            if (!skipJsInvoke)
            {
                await AttachMouseHandler();
            }

            var (start, end) = CurrentWeekRange;
            if (OnRequestNewData != null)
            {
                _loading = true;
                StateHasChanged();
                await OnRequestNewData(start, end);
                _loading = false;
            }
            StateHasChanged();
        }

        private async Task SetCurrentDay(DateTime today, bool skipJsInvoke = false)
        {
            CurrentDate = today;
            if (!skipJsInvoke)
            {
                await AttachMouseHandler();
            }

            var (start, end) = CurrentDayRange;
            if (OnRequestNewData != null)
            {
                _loading = true;
                StateHasChanged();
                await OnRequestNewData(start, end);
                _loading = false;
            }
            StateHasChanged();
        }

        private async Task AttachMouseHandler()
        {
            await jsRuntime.InvokeVoidAsync("BlazorScheduler.attachSchedulerMouseEventsHandler", _objReference);
        }

        private async Task DestroyMouseHandler()
        {
            await jsRuntime.InvokeVoidAsync("BlazorScheduler.destroySchedulerMouseEventsHandler");
        }

        private async Task LoadDateRangeData(int change = 0)
        {
            if (CalendarViewTypeService.IsDay())
            {
                await SetCurrentDay(change == 0 ? DateTime.Today : CurrentDate.AddDays(change), true);
            }
            else if (CalendarViewTypeService.IsWeek())
            {
                await SetCurrentWeek(change == 0 ? DateTime.Today : CurrentDate.AddDays(change * 7), true);
            }
            else if (CalendarViewTypeService.IsMonth())
            {
                await SetCurrentMonth(change == 0 ? DateTime.Today : CurrentDate.AddMonths(change), true);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private IEnumerable<DateTime> GetDaysInRange()
        {

            if (CalendarViewTypeService.IsDay())
            {
                var (start, end) = CurrentDayRange;
                return Enumerable
                    .Range(0, 1 + end.Subtract(start).Days)
                    .Select(offset => start.AddDays(offset));
            }
            else if (CalendarViewTypeService.IsWeek())
            {
                var (start, end) = CurrentWeekRange;
                return Enumerable
                    .Range(0, 1 + end.Subtract(start).Days)
                    .Select(offset => start.AddDays(offset));
            }
            else if (CalendarViewTypeService.IsMonth())
            {
                var (start, end) = CurrentMonthRange;
                return Enumerable
                    .Range(0, 1 + end.Subtract(start).Days)
                    .Select(offset => start.AddDays(offset));
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        private IEnumerable<Appointment> GetAppointmentsInRange(DateTime start, DateTime end)
        {
            var appointmentsInTimeframe = _appointments
                .Where(x => x.IsVisible)
                .Where(x => (start, end).Overlaps((x.Data.StartDate.DateTime.Date, x.Data.EndDate.DateTime.Date)));

            return appointmentsInTimeframe
                .OrderBy(x => x.Data.StartDate.DateTime)
                .ThenByDescending(x => (x.Data.EndDate.DateTime - x.Data.StartDate.DateTime).Days);
        }

        private Appointment? _reschedulingAppointment;
        public void BeginDrag(Appointment appointment)
        {
            if (!EnableRescheduling || _reschedulingAppointment is not null || _showNewAppointment)
                return;

            appointment.IsVisible = false;

            _reschedulingAppointment = appointment;
            _draggingStart = appointment.Data.StartDate.DateTime;
            _draggingEnd = appointment.Data.EndDate.DateTime;
            _draggingAppointmentAnchor = null;

            StateHasChanged();
        }

        public void BeginDrag(SchedulerDay day)
        {
            if (!EnableAppointmentsCreationFromScheduler)
                return;

            _draggingStart = _draggingEnd = day.Day;
            _showNewAppointment = true;

            _draggingAppointmentAnchor = _draggingStart;
            StateHasChanged();
        }

        public bool IsDayBeingScheduled(Appointment appointment)
            => ReferenceEquals(appointment, DraggingAppointment) && _reschedulingAppointment is not null;

        [JSInvokable]
        public async Task OnMouseUp(int button)
        {
            if (button == 0 && _draggingStart is not null && _draggingEnd is not null)
            {
                if (_showNewAppointment)
                {
                    _showNewAppointment = false;
                    if (OnAddingNewAppointment is not null)
                        await OnAddingNewAppointment.Invoke(_draggingStart.Value, _draggingEnd.Value);

                    StateHasChanged();
                }

                if (_reschedulingAppointment is not null)
                {
                    var tempApp = _reschedulingAppointment;
                    _reschedulingAppointment = null;

                    if (tempApp.OnReschedule is not null)
                        await tempApp.OnReschedule.Invoke(_draggingStart.Value, _draggingEnd.Value);
                    else
                        throw new ArgumentNullException(nameof(Appointment.OnReschedule), $"{nameof(Appointment.OnReschedule)} must be defined on your Appointment component");

                    tempApp.IsVisible = true;

                    StateHasChanged();
                }
            }
        }

        [JSInvokable]
        public void OnMouseMove(string date)
        {
            if (_showNewAppointment && EnableDragging)
            {
                var day = DateTime.ParseExact(date, "yyyyMMdd", null);
                var anchor = _draggingAppointmentAnchor!.Value;
                (_draggingStart, _draggingEnd) = day < anchor ? (day, anchor) : (anchor, day);
                StateHasChanged();
            }

            if (_reschedulingAppointment is not null)
            {
                var day = DateTime.ParseExact(date, "yyyyMMdd", null);
                _draggingAppointmentAnchor ??= day;

                var diff = (day - _draggingAppointmentAnchor.Value).Days;

                _draggingStart = _reschedulingAppointment.Data.StartDate.DateTime.AddDays(diff);
                _draggingEnd = _reschedulingAppointment.Data.EndDate.DateTime.AddDays(diff);

                StateHasChanged();
            }
        }

        public async ValueTask DisposeAsync()
        {
            CalendarService.OnChange -= StateHasChanged;
            CalendarViewTypeService.OnChange -= StateHasChanged;
            await DestroyMouseHandler();
            _objReference.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
