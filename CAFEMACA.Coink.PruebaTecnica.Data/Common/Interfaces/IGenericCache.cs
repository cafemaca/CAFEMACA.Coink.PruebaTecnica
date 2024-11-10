// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 10-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 10-23-2024
//  ****************************************************************
//  <copyright file="IGenericCache.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.Interfaces
{
    public interface IGenericCache
    {
        T GetData<T>(string cacheKey);
        void SetData<T>(string cacheKey, T instance);
        void Remove(string cacheKey);
    }
}
