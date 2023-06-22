namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public class AppointmentContext
    {
        private readonly Appointment _appointment;

        public bool IsTimed => _appointment.Data.StartDate.GetDateTime().Date == _appointment.Data.EndDate.GetDateTime().Date && _appointment.Data.StartDate.GetDateTime() != _appointment.Data.EndDate.GetDateTime();

        public AppointmentContext(Appointment appointment)
        {
            _appointment = appointment;
        }
    }
}
