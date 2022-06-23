using AutoMapper;
using FileStorage.BLL.Models;
using FileStorage.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FileStorage.BLL.Common
{
    /// <summary>
    /// Defines configuration for mapper
    /// </summary>
    public class MapperConfig : Profile
    {
        public MapperConfig(FilesOptions options)
        {
            CreateMap<Document, DocumentDto>().ReverseMap();

            CreateMap<Account, StatisticModel>()
                .ForMember(sm => sm.UsedSpace, a => a.MapFrom(a => a.UsedSpace))
                .ForMember(sm => sm.Files, a => a.MapFrom(a => a.Files))
                .ForMember(sm => sm.UserName, a => a.MapFrom(a => a.User.UserName))
                .ForMember(sm => sm.MaxSpace, a => a.MapFrom(a => options.MaxSizeSpace));
                
        }
    }
}
