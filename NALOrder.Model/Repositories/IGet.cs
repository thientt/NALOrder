// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NALOrder.Model
{
    public interface IGet<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
       
    }
}
