// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DepartamentoRequestValidator.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location;
using FluentValidation;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.Location
{
    public class DepartamentoRequestValidator : AbstractValidator<DepartamentoRequest>
    {
        public DepartamentoRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(DepartamentoErrors.RequiredId.ErrorMessage).WithErrorCode(DepartamentoErrors.RequiredId.ErrorCode)
                .Length(0, 100).WithMessage(DepartamentoErrors.ValidId.ErrorMessage).WithErrorCode(DepartamentoErrors.ValidId.ErrorCode);

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(DepartamentoErrors.RequiredName.ErrorMessage).WithErrorCode(DepartamentoErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(DepartamentoErrors.ValidName.ErrorMessage).WithErrorCode(DepartamentoErrors.ValidName.ErrorCode);

            RuleFor(x => x.IdPais).NotNull().NotEmpty().WithMessage(DepartamentoErrors.RequiredIdPais.ErrorMessage).WithErrorCode(DepartamentoErrors.RequiredIdPais.ErrorCode)
                .Length(0, 100).WithMessage(DepartamentoErrors.ValidIdPais.ErrorMessage).WithErrorCode(DepartamentoErrors.ValidIdPais.ErrorCode);
        }
    }
}
