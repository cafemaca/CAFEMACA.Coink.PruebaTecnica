// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-18-2024
//  ****************************************************************
//  <copyright file="PlayerErrors.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors
{
    /// <summary>
    /// Player Domain Errors
    /// </summary>
    public static class PlayerErrors
    {
        #region Bussines Validator Errors
        public static readonly DomainError RequiredName = new("Player.RequiredName", "Required Name");
        public static readonly DomainError RequiredPassword = new("Player.RequiredPassword", "Required Password");
        public static readonly DomainError RequiredEmail = new("Player.RequiredPassword", "Required Email");
        public static readonly DomainError ValidName = new("Player.ValidName", "Name length should between 3 to 100");
        public static readonly DomainError ValueCannotBeZero = new("Player.ValueCannotBeZero", "Name length should between 3 to 100");
        public static readonly DomainError PasswordMinimumLength = new("Player.PasswordMinimumLength", "Password length should be minimum 8");
        public static readonly DomainError PasswordCapitalLetter = new("Player.PasswordCapitalLetter", "Password must be have almost a one capital letter");
        public static readonly DomainError InvalidEmail = new("Player.InvalidEmail", "Invalid Email Address");
        #endregion

        #region Bussines Errors
        public static DomainError NotFound(int id) => new("Player.NotFound", $"The Player with Id '{id}' was not found");
        #endregion
    }
}
