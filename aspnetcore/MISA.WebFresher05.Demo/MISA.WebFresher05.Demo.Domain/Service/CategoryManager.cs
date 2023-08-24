using MISA.WebFresher05.Demo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain
{
    public class CategoryManager : ICategoryManager
    {
        #region Fields
        private readonly ICategoryRepository _categoryRepository;
        #endregion

        #region Constructor
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check trùng code
        /// </summary>
        /// <param name="code">Mã loại tài sản</param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// Created by: DDTung (19/07/2023)
        public async Task CheckCategoryExistByCode(string code)
        {
            var categoryExist = await _categoryRepository.FindByCodeAsync(code);
            if (categoryExist != null)
            {
                throw new ConflictException("Mã tài sản bị trùng");
            }
        }
        #endregion
    }
}
