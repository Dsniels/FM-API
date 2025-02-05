using System;
using AutoMapper;
using Core.Entities;

namespace apisiase.Dto;

public class MappingProfile :Profile
{
     public MappingProfile()
        {

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<RentaDto, Renta>().ReverseMap();

        }

}
