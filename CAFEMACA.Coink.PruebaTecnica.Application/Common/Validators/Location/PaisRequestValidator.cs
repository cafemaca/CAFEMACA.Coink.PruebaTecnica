// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-140-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PaisRequestValidator.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location;
using FluentValidation;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.Location
{
    public class PaisRequestValidator : AbstractValidator<PaisRequest>
    {
        public PaisRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(PaisErrors.RequiredId.ErrorMessage).WithErrorCode(PaisErrors.RequiredId.ErrorCode)
                .Length(0, 100).WithMessage(PaisErrors.ValidId.ErrorMessage).WithErrorCode(PaisErrors.ValidId.ErrorCode);

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(PaisErrors.RequiredName.ErrorMessage).WithErrorCode(PaisErrors.RequiredName.ErrorCode)
                .Length(0, 100).WithMessage(PaisErrors.ValidName.ErrorMessage).WithErrorCode(PaisErrors.ValidName.ErrorCode);
        }
    }
}
