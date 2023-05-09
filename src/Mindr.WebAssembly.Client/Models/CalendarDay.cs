using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mindr.WebAssembly.Client.Models;

public class CalendarDay {
    public CalendarDay()
    {
        Events = new ConcurrentBag<AgendaEvent>();
    }
    public int DayNumber { get; set; }
    public DateTime Date { get; set; }
    public bool IsEmpty {get; set;}

    public ConcurrentBag<AgendaEvent> Events {get; set;}
}

