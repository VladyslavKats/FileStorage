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
        public MapperConfig()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
                
        }
    }
}
