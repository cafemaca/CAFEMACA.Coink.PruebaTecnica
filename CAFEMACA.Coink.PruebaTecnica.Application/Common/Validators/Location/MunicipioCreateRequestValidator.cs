// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="MunicipioRequestValidator.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location;
using FluentValidation;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.Location
{
    public class MunicipioCreateRequestValidator : AbstractValidator<MunicipioCreateRequest>
    {
        public MunicipioCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(MunicipioErrors.RequiredId.ErrorMessage).WithErrorCode(MunicipioErrors.RequiredId.ErrorCode)
                .Length(0, 100).WithMessage(MunicipioErrors.ValidId.ErrorMessage).WithErrorCode(MunicipioErrors.ValidId.ErrorCode);

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(MunicipioErrors.RequiredName.ErrorMessage).WithErrorCode(MunicipioErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(MunicipioErrors.ValidName.ErrorMessage).WithErrorCode(MunicipioErrors.ValidName.ErrorCode);

            RuleFor(x => x.DepartamentoId).NotNull().NotEmpty().WithMessage(MunicipioErrors.RequiredIdDepartamento.ErrorMessage).WithErrorCode(MunicipioErrors.RequiredIdDepartamento.ErrorCode)
                .Length(0, 100).WithMessage(MunicipioErrors.ValidIdDepartamento.ErrorMessage).WithErrorCode(MunicipioErrors.ValidIdDepartamento.ErrorCode);
        }
    }
}
