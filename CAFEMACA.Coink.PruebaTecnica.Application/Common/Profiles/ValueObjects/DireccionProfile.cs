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
using CAFEMACA.Coink.PruebaTecnica.Domain.ValueObjects.User;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.User
{
    public class DireccionProfile : Profile
    {
        public DireccionProfile()
        {
            CreateMap<DireccionRequest, Direccion>()
                       .ForMember(dest => dest.DireccionName, src => src.MapFrom(x => x.DireccionName))
                       .ForMember(dest => dest.Municipioid, src => src.MapFrom(x => x.MunicipioId));

            CreateMap<Direccion, DireccionResponse>()
                       .ForMember(dest => dest.DireccionName, src => src.MapFrom(x => x.DireccionName))
                       .ForMember(dest => dest.Municipio, src => src.MapFrom(x => x.Municipio));

        }
    }
}
