// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioRequestValidator.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.User;
using FluentValidation;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.User
{
    public class UsuarioRequestValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(UsuarioErrors.RequiredId.ErrorMessage).WithErrorCode(UsuarioErrors.RequiredId.ErrorCode)
                .Length(0, 100).WithMessage(UsuarioErrors.ValidId.ErrorMessage).WithErrorCode(UsuarioErrors.ValidId.ErrorCode);

            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage(UsuarioErrors.RequiredName.ErrorMessage).WithErrorCode(UsuarioErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(UsuarioErrors.ValidName.ErrorMessage).WithErrorCode(UsuarioErrors.ValidName.ErrorCode);
        }
    }
}
