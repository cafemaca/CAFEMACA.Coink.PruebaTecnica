// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="PlayerServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Players;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Players
{
    public class PlayerServices : IPlayerServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlayerRepository _playerRepository;
        private readonly IValidator<PlayerRequest> _validator;

        public PlayerServices(ILogger<PlayerServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IPlayerRepository playerRepository
            , IValidator<PlayerRequest> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _playerRepository = playerRepository;
            _validator = validator;
        }

        public async Task<Result<PlayerResponse?, IEnumerable<DomainError>>> CreatePlayerAsync(PlayerRequest playerRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(playerRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Player player = _mapper.Map<Player>(playerRequest);

                await _playerRepository.InsertAsync(player, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<PlayerResponse>(player);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeletePlayerAsync(int id, CancellationToken cancellationToken)
        {
            Player? player = await _playerRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (player != null)
            {
                await _playerRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            else
            {
                return PlayerErrors.NotFound(id);
            }
        }

        public async Task<Result<IEnumerable<PlayerResponse>, DomainError>> SelectAllPlayers(CancellationToken cancellationToken)
        {
            var players = (await _playerRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<PlayerResponse>>(players);
        }

        public async Task<Result<PagedList<PlayerResponse>, DomainError>> SelectAllPlayers(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<Player>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<Player>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<Player, bool>> filters = null;
            List<SpecificationSort<Player>> sorts = null;
            //First, we are checking our SearchTerm. If it contains information we are creating a filter.
            var searchTerm = "";
            if (!string.IsNullOrEmpty(searchQueryParameters.SearchTerm))
            {
                searchTerm = searchQueryParameters.SearchTerm.Trim().ToLower();
                filters = x => x.Name.ToLower().Contains(searchTerm);
            }
            // Then we are overwriting a filter if columnFilters has data.
            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<Player>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<Player>.CustomSort(columnSorting);
            }

            PagedList<Player> players = (await _playerRepository.GetAllAsync(new PlayerSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<PlayerResponse>>(players);
        }

        public async Task<Result<PlayerResponse?, DomainError>> SelectPlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            Player? player = await _playerRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (player != null)
            {
                return _mapper.Map<PlayerResponse>(player);
            }
            else
            {
                return PlayerErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(int id, PlayerRequest playerRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(playerRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Player? player = await _playerRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (player != null)
                {
                    _mapper.Map<PlayerRequest, Player>(playerRequest, player);
                    player.Id = id;

                    await _playerRepository.UpdateAsync(player, cancellationToken).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                else
                {
                    List<DomainError> errors = new List<DomainError>();
                    errors.Add(PlayerErrors.NotFound(id));
                    return errors;
                }
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }
    }
}
