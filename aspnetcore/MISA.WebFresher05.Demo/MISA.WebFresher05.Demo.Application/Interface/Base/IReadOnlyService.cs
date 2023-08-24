using MISA.WebFresher05.Demo.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Interface.Base
{
    public interface IReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>Tất cả tài nguyên</returns>
        /// Created by: ddtung (14/07/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();
        /// <summary>
        /// Lấy tài nguyên theo id
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản theo id</returns>
        /// Created by: ddtung (14/07/2023)
        Task<TEntityDto> GetAsync(Guid id);


        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: ddtung (24/07/2023)
        Task<BaseFilterResponse<TEntityDto>> FilterAsync(int? pageSize, int? pageNumber, string? searchText, Guid department_id, Guid fixed_asset_category_id);

       
    }
}
