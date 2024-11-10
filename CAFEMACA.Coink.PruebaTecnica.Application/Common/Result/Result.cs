// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-18-2024
//  ****************************************************************
//  <copyright file="Result.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue">Correct Answer</typeparam>
    /// <typeparam name="TError">Errors</typeparam>
    /// <see cref="https://www.youtube.com/watch?v=YbuSuSpzee4"/>
    public readonly struct Result<TValue, TError>
    {
        private readonly TValue? _value;
        private readonly TError? _error;

        public bool IsError { get; }

        public bool IsSuccess => !IsError;


        private Result(TValue value)
        {
            IsError = false;
            _value = value;
            _error = default;
        }

        private Result(TError error)
        {
            IsError = true;
            _value = default;
            _error = error;
        }

        //happy path
        public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);

        //error path
        public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

        public TResult Match<TResult>(Func<TValue, TResult> success
            , Func<TError, TResult> failure)
        {
            return IsSuccess ? success(_value) : failure(_error);
        }
    }
}
