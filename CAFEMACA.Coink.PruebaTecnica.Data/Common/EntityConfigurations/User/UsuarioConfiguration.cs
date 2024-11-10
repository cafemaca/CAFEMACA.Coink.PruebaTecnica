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

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.EntityConfigurations.User
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.Property(x => x.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Direccion.DireccionName)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.Direccion.IdMunicipio)
                .HasMaxLength(6)
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
