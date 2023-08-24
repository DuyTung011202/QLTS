using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Interface.Base
{
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity);
        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Guid id,TEntity entity);
        /// <summary>
        /// Xoá 1
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// Xoá list
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteManyAsync(List<TEntity> entities);
    }
}
