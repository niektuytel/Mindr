using AutoMapper;
using Mindr.Domain.Models.DTO.Connector;
using Newtonsoft.Json.Linq;

namespace Mindr.Api.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Connector, ConnectorBriefDTO>();
        CreateMap<Connector, ConnectorOverviewDTO>();
    }
}
