// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Usaer
{
    public class DireccionProfile : Profile
    {
        public DireccionProfile()
        {
            CreateMap<UsuarioCreateRequest, Usuario>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Nombre, src => src.MapFrom(x => x.Nombre))
                       .ForMember(dest => dest.Telefono, src => src.MapFrom(x => x.Telefono))
                       .ForMember(dest => dest.Direccion, src => src.MapFrom(x => x.Direccion));

            CreateMap<UsuarioUpdateRequest, Usuario>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Nombre, src => src.MapFrom(x => x.Nombre))
                       .ForMember(dest => dest.Telefono, src => src.MapFrom(x => x.Telefono))
                       .ForMember(dest => dest.Direccion, src => src.MapFrom(x => x.Direccion));

            CreateMap<Usuario, UsuarioResponse>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Nombre, src => src.MapFrom(x => x.Nombre))
                       .ForMember(dest => dest.Telefono, src => src.MapFrom(x => x.Telefono))
                       .ForMember(dest => dest.Direccion, src => src.MapFrom(x => x.Direccion));

        }
    }
}
