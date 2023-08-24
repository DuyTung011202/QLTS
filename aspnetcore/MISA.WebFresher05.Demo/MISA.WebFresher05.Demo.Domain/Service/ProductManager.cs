using MISA.WebFresher05.Demo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain
{
    public class ProductManager : IProductManager
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructors
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check trùng code
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns>Exception</returns>
        /// <exception cref="ConflictException">Lỗi mã trùng</exception>
        /// Created by: DDTung (19/07/2023)
        public async Task CheckProductExistByCode(string code)
        {
            var productExist = await _productRepository.FindByCodeAsync(code);
            if (productExist != null)
            {
                throw new ConflictException("Mã tài sản bị trùng");
            }
        } 
        #endregion
    }
}
