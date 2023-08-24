using MISA.WebFresher05.Demo.Domain.Entity.Category;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

        /// <summary>
        /// Tìm kiếm tài sản theo code
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản</returns>
        /// Created by: ddtung (19/07/2023)
        Task<Category?> FindByCodeAsync(string code);

    }
}
