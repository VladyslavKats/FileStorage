﻿using AutoMapper;
using FileStorage.BLL.Models;
using FileStorage.DAL.Models;

namespace FileStorage.BLL.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
        }
    }
}
