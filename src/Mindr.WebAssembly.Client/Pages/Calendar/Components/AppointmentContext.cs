﻿namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public class AppointmentContext
    {
        private readonly Appointment _appointment;

        public bool IsTimed => _appointment.Start.Date == _appointment.End.Date && _appointment.Start != _appointment.End;

        public AppointmentContext(Appointment appointment)
        {
            _appointment = appointment;
        }
    }
}
