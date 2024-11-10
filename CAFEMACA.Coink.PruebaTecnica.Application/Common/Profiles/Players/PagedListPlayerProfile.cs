// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 09-20-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-20-2024
//  ****************************************************************
//  <copyright file="PagedListPlayerProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Players
{
    /// <summary>
    /// 
    /// </summary>
    public class PagedListPlayerProfile : Profile
    {
        public PagedListPlayerProfile()
        {
            CreateMap<PagedList<Player>, PagedList<PlayerResponse>>()
                       .ForMember(dest => dest.PageSize, src => src.MapFrom(x => x.PageSize))
                       .ForMember(dest => dest.CurrentPage, src => src.MapFrom(x => x.CurrentPage))
                       .ForMember(dest => dest.TotalItemCount, src => src.MapFrom(x => x.TotalItemCount))
                       .ForMember(dest => dest.TotalPageCount, src => src.MapFrom(x => x.TotalPageCount))
                       .ForMember(dest => dest.Items, src => src.MapFrom(x => x.Items));

        }
    }
}
