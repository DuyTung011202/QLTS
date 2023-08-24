using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Application.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Interface
{
    public interface IProductService : IBaseService<ProductDto, ProductCreateDto, ProductUpdateDto>
    {
        /// <summary>
        /// Sinh code mới cho tài sản
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Author: DDTung (22/07/2023)
        Task<string> GetNewCodeAsync();
    }
}
