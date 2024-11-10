// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 09-20-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-20-2024
//  ****************************************************************
//  <copyright file="AuditTrailResponse.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Audit
{
    public record AuditTrailResponse
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        //TODO: Security.
        //Here we have a reference to a User entity. Depending on your application needs, you may have this reference or not.
        //public User? User { get; set; }

        public string TrailType { get; set; } = string.Empty;

        public DateTime DateUtc { get; set; } = DateTime.MinValue;

        public string EntityName { get; set; } = string.Empty;

        public string? PrimaryKey { get; set; } = null;

        public Dictionary<string, object?> OldValues { get; set; } = [];

        public Dictionary<string, object?> NewValues { get; set; } = [];

        public List<string> ChangedColumns { get; set; } = [];
    }
}
