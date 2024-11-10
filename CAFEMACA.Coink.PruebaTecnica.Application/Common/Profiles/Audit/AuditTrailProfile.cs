// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 09-20-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-20-2024
//  ****************************************************************
//  <copyright file="AuditTrailProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Audit;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Audit
{
    public class AuditTrailProfile : Profile
    {
        public AuditTrailProfile()
        {
            CreateMap<AuditTrail, AuditTrailResponse>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.TrailType, src => src.MapFrom(x => x.TrailType.ToString()))
                       .ForMember(dest => dest.DateUtc, src => src.MapFrom(x => x.DateUtc))
                       .ForMember(dest => dest.EntityName, src => src.MapFrom(x => x.EntityName))
                       .ForMember(dest => dest.PrimaryKey, src => src.MapFrom(x => x.PrimaryKey))
                       .ForMember(dest => dest.OldValues, src => src.MapFrom(x => x.OldValues))
                       .ForMember(dest => dest.OldValues, src => src.MapFrom(x => x.OldValues))
                       .ForMember(dest => dest.ChangedColumns, src => src.MapFrom(x => x.ChangedColumns))
                       ;

        }
    }
}
