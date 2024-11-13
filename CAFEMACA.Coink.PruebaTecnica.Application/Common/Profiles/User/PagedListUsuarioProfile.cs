// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PagedListUsuarioProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Usaer
{
    public class PagedListUsuarioProfile : Profile
    {
        public PagedListUsuarioProfile()
        {
            CreateMap<PagedList<Usuario>, PagedList<UsuarioResponse>>()
                       .ForMember(dest => dest.PageSize, src => src.MapFrom(x => x.PageSize))
                       .ForMember(dest => dest.CurrentPage, src => src.MapFrom(x => x.CurrentPage))
                       .ForMember(dest => dest.TotalItemCount, src => src.MapFrom(x => x.TotalItemCount))
                       .ForMember(dest => dest.TotalPageCount, src => src.MapFrom(x => x.TotalPageCount))
                       .ForMember(dest => dest.Items, src => src.MapFrom(x => x.Items));

        }
    }
}
