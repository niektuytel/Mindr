using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Mindr.Core.Models;

namespace Mindr.WebUI.Services;

public interface IMicrosoftCalendarEventsProvider
{
    Task<IEnumerable<Event>> GetEventsInMonthAsync(int year, int month);
}