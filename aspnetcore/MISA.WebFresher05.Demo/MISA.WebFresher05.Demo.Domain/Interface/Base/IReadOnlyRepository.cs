using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Interface.Base
{
    public interface IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả 
        /// </summary>
        /// <returns>Tất cả tài sản</returns>
        /// Created by: ddtung (14/07/2023)
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản</returns>
        /// Created by: ddtung (14/07/2023)
        Task<TEntity> GetAsync(Guid id);


        /// <summary>
        /// Lấy list theo id
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản</returns>
        /// Created by: ddtung (14/07/2023)
        Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids);


        /// <summary>
        /// Hàm đếm số bản ghi trong db
        /// </summary>
        /// <returns>Số bản ghi trong db</returns>
        /// Created by: ddtung (18/07/2023)
        Task<int> CountAsync();
        /// <summary>
        /// Tìm kiếm theo id
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản</returns>
        /// Created by: ddtung (14/07/2023)
        Task<TEntity?> FindAsync(Guid id);


        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: ddtung (24/07/2023)
        Task<IEnumerable<TEntity>> FilterAsync(int? pageSize,  int? pageNumber, string? searchText, Guid department_id, Guid fixed_asset_category_id);
    }
}
