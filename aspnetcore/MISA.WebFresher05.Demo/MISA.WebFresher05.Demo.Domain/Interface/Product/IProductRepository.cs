using MISA.WebFresher05.Demo.Domain.Entity.Product;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {


        /// <summary>
        /// Lấy mã code lớn nhất
        /// </summary>
        /// <param></param>
        /// <returns>Mã code lớn nhất</returns>
        /// Author: DDTung 22/07/2023
        Task<string> GetMaxCodeAsync();

        /// <summary>
        /// Tìm kiếm tài sản theo code
        /// </summary>
        /// <param name="id">Định danh tài sản</param>
        /// <returns>Tài sản</returns>
        /// Created by: ddtung (19/07/2023)
        Task<Product?> FindByCodeAsync(string code);
        
    }
}
