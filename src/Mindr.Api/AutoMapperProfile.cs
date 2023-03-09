using AutoMapper;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json.Linq;

namespace Mindr.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Connector, ConnectorBriefDTO>();

    }
}
