using AutoMapper;
using Mindr.Api.Models.Connectors;
using Mindr.Core.Models.Connectors;
using Newtonsoft.Json.Linq;

namespace Mindr.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Connector, ConnectorBriefDTO>();
        CreateMap<Connector, ConnectorOverviewDTO>();
    }
}
