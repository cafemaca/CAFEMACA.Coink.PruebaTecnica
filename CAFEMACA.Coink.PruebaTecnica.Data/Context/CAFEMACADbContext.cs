// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 03-20-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-19-2024
//  ****************************************************************
//  <copyright file="ApplicationDbContext.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.EntityConfigurations;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Enums;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Context
{
    /// <summary>
    /// Clase que define el DbContext de la aplicacion.
    /// </summary>
    /// <param name="options">Opciones de la configuracion.</param>
    public class CafemacaDbContext : DbContext
    {
        private readonly ICurrentSessionProvider _currentSessionProvider;

        #region Definicion de los diferentes DbSet
        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<AuditTrail> AuditTrails { get; set; } = default!;
        #endregion

        public CafemacaDbContext(DbContextOptions<CafemacaDbContext> options,
            ICurrentSessionProvider currentSessionProvider) : base(options)
        {
            _currentSessionProvider = currentSessionProvider;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new AuditTrailConfiguration());

            // Seed database with 100 items.
            var items = Enumerable.Range(1, 100).Select(index => new Player
            {
                Id = index,
                Name = $"Name{index}",
                Password = $"Password{index}$",
                Email = $"Name{index}@hotmail.com",

                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = "Carlos Fernando Malagón Cano"
            }).ToList();
            modelBuilder.Entity<Player>().HasData(items);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var userId = _currentSessionProvider.GetUserId();

            SetAuditableProperties(userId);

            var auditEntries = HandleAuditingBeforeSaveChanges(userId).ToList();
            if (auditEntries.Count > 0)
            {
                await AuditTrails.AddRangeAsync(auditEntries, cancellationToken);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        private List<AuditTrail> HandleAuditingBeforeSaveChanges(Guid? userId)
        {
            var auditableEntries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(x => x.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
                .Select(x => CreateTrailEntry(userId, x))
                .ToList();

            return auditableEntries;
        }

        private static AuditTrail CreateTrailEntry(Guid? userId, EntityEntry<IAuditableEntity> entry)
        {
            var trailEntry = new AuditTrail
            {
                Id = Guid.NewGuid(),
                EntityName = entry.Entity.GetType().Name,
                UserId = userId,
                DateUtc = DateTime.UtcNow
            };

            SetAuditTrailPropertyValues(entry, trailEntry);
            SetAuditTrailNavigationValues(entry, trailEntry);
            SetAuditTrailReferenceValues(entry, trailEntry);

            return trailEntry;
        }

        /// <summary>
        /// Sets auditable properties for entities that are inherited from <see cref="IAuditableEntity"/>
        /// </summary>
        /// <param name="userId">User identifier that performed an action</param>
        /// <returns>Collection of auditable entities</returns>
        private void SetAuditableProperties(Guid? userId)
        {
            const string systemSource = "system";
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId?.ToString() ?? systemSource;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId?.ToString() ?? systemSource;
                        break;
                }
            }
        }

        /// <summary>
        /// Sets column values to audit trail entity
        /// </summary>
        /// <param name="entry">Current entity entry ef core model</param>
        /// <param name="trailEntry">Audit trail entity</param>
        private static void SetAuditTrailPropertyValues(EntityEntry entry, AuditTrail trailEntry)
        {
            // Skip temp fields (that will be assigned automatically by ef core engine, for example: when inserting an entity
            foreach (var property in entry.Properties.Where(x => !x.IsTemporary))
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    trailEntry.PrimaryKey = property.CurrentValue?.ToString();
                    continue;
                }

                // Filter properties that should not appear in the audit list
                if (property.Metadata.Name.Equals("PasswordHash"))
                {
                    continue;
                }

                SetAuditTrailPropertyValue(entry, trailEntry, property);
            }
        }

        /// <summary>
        /// Sets a property value to the audit trail entity
        /// </summary>
        /// <param name="entry">Current entity entry ef core model</param>
        /// <param name="trailEntry">Audit trail entity</param>
        /// <param name="property">Entity property ef core model</param>
        private static void SetAuditTrailPropertyValue(EntityEntry entry, AuditTrail trailEntry, PropertyEntry property)
        {
            var propertyName = property.Metadata.Name;

            switch (entry.State)
            {
                case EntityState.Added:
                    trailEntry.TrailType = TrailType.Create;
                    trailEntry.NewValues[propertyName] = property.CurrentValue;

                    break;

                case EntityState.Deleted:
                    trailEntry.TrailType = TrailType.Delete;
                    trailEntry.OldValues[propertyName] = property.OriginalValue;

                    break;

                case EntityState.Modified:
                    if (property.IsModified && (property.OriginalValue is null || !property.OriginalValue.Equals(property.CurrentValue)))
                    {
                        trailEntry.ChangedColumns.Add(propertyName);
                        trailEntry.TrailType = TrailType.Update;
                        trailEntry.OldValues[propertyName] = property.OriginalValue;
                        trailEntry.NewValues[propertyName] = property.CurrentValue;
                    }

                    break;
            }

            if (trailEntry.ChangedColumns.Count > 0)
            {
                trailEntry.TrailType = TrailType.Update;
            }
        }

        private static void SetAuditTrailNavigationValues(EntityEntry entry, AuditTrail trailEntry)
        {
            foreach (var navigation in entry.Navigations.Where(x => x.Metadata.IsCollection && x.IsModified))
            {
                if (navigation.CurrentValue is not IEnumerable<object> enumerable)
                {
                    continue;
                }

                var collection = enumerable.ToList();
                if (collection.Count == 0)
                {
                    continue;
                }

                var navigationName = collection.First().GetType().Name;
                trailEntry.ChangedColumns.Add(navigationName);
            }
        }

        private static void SetAuditTrailReferenceValues(EntityEntry entry, AuditTrail trailEntry)
        {
            foreach (var reference in entry.References.Where(x => x.IsModified))
            {
                var referenceName = reference.EntityEntry.Entity.GetType().Name;
                trailEntry.ChangedColumns.Add(referenceName);
            }
        }
    }
}
