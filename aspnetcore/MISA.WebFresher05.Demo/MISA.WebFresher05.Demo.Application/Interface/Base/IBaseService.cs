using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Interface.Base
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Hàm thêm 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: ddtung (19/07/2023)
        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// Hàm sửa theo id
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: ddtung (19/07/2023)
        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);
        /// <summary>
        /// Hàm xoá theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: ddtung (19/07/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xoá nhiều theo id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created by: ddtung (19/07/2023)
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
