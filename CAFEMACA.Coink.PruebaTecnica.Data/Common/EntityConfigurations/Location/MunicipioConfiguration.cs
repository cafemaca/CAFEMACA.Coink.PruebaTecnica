// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="ApplicationDbContext.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.EntityConfigurations.Location
{
    public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {

            builder.Property(x => x.Id)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.IdDepartamento)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(p => p.CreatedAtUtc)
                .IsRequired();

            builder.Property(p => p.UpdatedAtUtc);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.UpdatedBy)
                .HasMaxLength(500);

            builder.HasKey(p => p.Id);
        }
    }
}
