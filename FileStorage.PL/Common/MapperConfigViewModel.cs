using AutoMapper;
using FileStorage.BLL.Models;
using FileStorage.DAL.Models;
using FileStorage.PL.Models;

namespace FileStorage.PL.Common
{
    /// <summary>
    /// Class for mapper configuration
    /// </summary>
    public class MapperConfigViewModel : Profile
    {
        public MapperConfigViewModel()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(uvm => uvm.Id, u => u.MapFrom(u => u.Id))
                .ForMember(uvm => uvm.Name, u => u.MapFrom(u => u.UserName))
                .ForMember(uvm => uvm.Email, u => u.MapFrom(u => u.Email));
            CreateMap<DocumentDto, DocumentViewModel>();
            CreateMap<DocumentUpdateModel, DocumentDto>();
        }
    }
}
