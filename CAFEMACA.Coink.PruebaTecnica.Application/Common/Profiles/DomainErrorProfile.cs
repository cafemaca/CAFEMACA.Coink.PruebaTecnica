// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 06-14-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-14-2024
//  ****************************************************************
//  <copyright file="DomainErrorProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using FluentValidation.Results;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles
{
    public class DomainErrorProfile : Profile
    {
        public DomainErrorProfile()
        {
            CreateMap<ValidationFailure, DomainError>()
                       .ForMember(dest => dest.ErrorCode, src => src.MapFrom(x => x.ErrorCode))
                       .ForMember(dest => dest.ErrorMessage, src => src.MapFrom(x => x.ErrorMessage));
        }
    }
}
