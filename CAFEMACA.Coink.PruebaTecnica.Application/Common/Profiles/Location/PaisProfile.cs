// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PaisProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Location
{
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            CreateMap<PaisCreateRequest, Pais>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            CreateMap<PaisUpdateRequest, Pais>()
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            CreateMap<Pais, PaisResponse>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

        }
    }
}
