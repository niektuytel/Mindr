using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindr.Core.Models;

namespace Mindr.Core.Interfaces
{
    public interface IMicrosoftGraphProvider
    {
        Task<IEnumerable<AgendaEvent>> GetEventsAsync(string objectId, int year, int month);
    }
}