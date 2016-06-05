
// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************
// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016

// ***********************************************************************
using System.Threading.Tasks;

namespace NALOrder.Model
{
    public interface IDelete<TEntity> where TEntity : class
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        SaveResult Delete(TEntity entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<SaveResult> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the by.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        SaveResult DeleteBy(int Id);

        /// <summary>
        /// Deletes the by asynchronous.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        Task<SaveResult> DeleteByAsync(int Id);
    }
}
