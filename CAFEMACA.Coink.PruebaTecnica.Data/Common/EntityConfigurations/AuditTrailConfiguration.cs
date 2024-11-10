// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 09-19-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-19-2024
//  ****************************************************************
//  <copyright file="ApplicationDbContext.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.EntityConfigurations
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.EntityName);

            builder.Property(e => e.Id);

            builder.Property(e => e.UserId);
            builder.Property(e => e.EntityName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.DateUtc).IsRequired();
            builder.Property(e => e.PrimaryKey).HasMaxLength(100);

            builder.Property(e => e.TrailType).HasConversion<string>();

            //builder.Property(e => e.ChangedColumns).HasColumnType("jsonb");
            builder.Property(e => e.ChangedColumns)
            .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), // Convert to string for persistence
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) // Convert to List<String> for use
            );

            //builder.Property(e => e.OldValues).HasColumnType("jsonb");
            //builder.Property(e => e.OldValues);
            builder.Property(e => e.OldValues)
            .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), // Convert to string for persistence
                    v => JsonSerializer.Deserialize<Dictionary<string, Object?>>(v, (JsonSerializerOptions)null) // Convert to List<String> for use
            );

            //builder.Property(e => e.NewValues).HasColumnType("jsonb");
            //builder.Property(e => e.NewValues);
            builder.Property(e => e.NewValues)
            .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), // Convert to string for persistence
                    v => JsonSerializer.Deserialize<Dictionary<string, Object?>>(v, (JsonSerializerOptions)null) // Convert to List<String> for use
            );

            //TODO: Security.
            //Here we have a reference to a User entity. Depending on your application needs, you may have this reference or not.
            //public User? User { get; set; }
            /*
            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            */
        }
    }
}
