using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models
{
    public class Connector
    {

        public Connector(int id, int pipelineId, string name, string description, string status, string response)
        {
            Id = id;
            PipelineId = pipelineId;
            Name = name;
            Description = description;
            Status = status;
            Response = response;
        }

        public int Id { get; set; }
        public int PipelineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }

    }
}
