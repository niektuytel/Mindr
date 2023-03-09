using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    /// <summary>
    /// Used by hangfire to call connectors on time schedule 
    /// </summary>
    public class ConnectorHook
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EventId { get; set; }
        public Guid ConnectorId { get; set; }

        // optional time schedule?
        // optional retry?
        // optional retry?
    }
}
