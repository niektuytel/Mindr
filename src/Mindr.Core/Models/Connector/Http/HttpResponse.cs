﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpResponse : PostmanResponse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<HttpVariable> Variables { get; set; } = null;
    }
}

