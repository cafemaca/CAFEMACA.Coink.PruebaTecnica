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
    public class DepartamentoUpdateRequestValidator : AbstractValidator<DepartamentoUpdateRequest>
    {
        public DepartamentoUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(DepartamentoErrors.RequiredName.ErrorMessage).WithErrorCode(DepartamentoErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(DepartamentoErrors.ValidName.ErrorMessage).WithErrorCode(DepartamentoErrors.ValidName.ErrorCode);
        }
    }
}
