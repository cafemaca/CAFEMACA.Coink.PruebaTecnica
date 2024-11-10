// ****************************************************************
//  Assembly         : Assembly Name
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-18-2024
//  ****************************************************************
//  <copyright file="PlayerProfile.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles.Players
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerRequest, Player>()
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                       .ForMember(dest => dest.Password, src => src.MapFrom(x => x.Password))
                       .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email));

            CreateMap<Player, PlayerResponse>()
                       .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                       .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                       .ForMember(dest => dest.Password, src => src.MapFrom(x => x.Password))
                       .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email));

        }
    }
}
