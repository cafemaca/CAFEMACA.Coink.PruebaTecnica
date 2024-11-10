// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-12-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="PlayerRequestValidator.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors;
using FluentValidation;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators
{
    public class PlayerRequestValidator : AbstractValidator<PlayerRequest>
    {
        public PlayerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(PlayerErrors.RequiredName.ErrorMessage).WithErrorCode(PlayerErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(PlayerErrors.ValidName.ErrorMessage).WithErrorCode(PlayerErrors.ValidName.ErrorCode);

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(PlayerErrors.RequiredPassword.ErrorMessage).WithErrorCode(PlayerErrors.RequiredPassword.ErrorCode)
                .MinimumLength(8).WithMessage(PlayerErrors.PasswordMinimumLength.ErrorMessage).WithErrorCode(PlayerErrors.PasswordMinimumLength.ErrorCode)
                .Matches("[A-Z]").WithMessage(PlayerErrors.PasswordCapitalLetter.ErrorMessage).WithErrorCode(PlayerErrors.PasswordCapitalLetter.ErrorCode);

            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(PlayerErrors.RequiredEmail.ErrorMessage).WithErrorCode(PlayerErrors.RequiredEmail.ErrorCode)
                .EmailAddress().WithMessage(PlayerErrors.InvalidEmail.ErrorMessage).WithErrorCode(PlayerErrors.InvalidEmail.ErrorCode);

        }
    }
}
