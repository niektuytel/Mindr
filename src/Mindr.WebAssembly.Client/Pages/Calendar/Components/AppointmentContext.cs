namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public class AppointmentContext
    {
        private readonly Appointment _appointment;

        public bool IsTimed => _appointment.Data.StartDate.DateTime.Date == _appointment.Data.EndDate.DateTime.Date && _appointment.Data.StartDate.DateTime != _appointment.Data.EndDate.DateTime;

        public AppointmentContext(Appointment appointment)
        {
            _appointment = appointment;
        }
    }
}
