// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-102024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DepartamentoProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Location
{
    public class DepartamentoProfile : Profile
    {
        public DepartamentoProfile()
        {
            CreateMap<DepartamentoRequest, Departamento>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                       .ForMember(dest => dest.IdPais, src => src.MapFrom(x => x.IdPais));

            CreateMap<Departamento, DepartamentoResponse>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                       .ForMember(dest => dest.PaisAsoc, src => src.MapFrom(x => x.PaisAsoc));

        }
    }
}
