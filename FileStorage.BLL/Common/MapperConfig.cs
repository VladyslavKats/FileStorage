using AutoMapper;
using FileStorage.BLL.Models;
using FileStorage.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace FileStorage.BLL.Common
{
    public class MapperConfig : Profile
    {
        public MapperConfig(IConfiguration configuration)
        {
            CreateMap<Document, DocumentDto>().ReverseMap();

            CreateMap<Account, StatisticModel>()
                .ForMember(sm => sm.UsedSpace, a => a.MapFrom(a => a.UsedSpace))
                .ForMember(sm => sm.Files, a => a.MapFrom(a => a.Files))
                .ForMember(sm => sm.UserName, a => a.MapFrom(a => a.User.UserName))
                .ForMember(sm => sm.MaxSpace, a => a.MapFrom(a => long.Parse(configuration.GetSection("Files")["MaxSizeSpace"])));
                
        }
    }
}
