using AutoMapper;
using MISA.Fresher05.Demo.Application.Interface;
using MISA.Fresher05.Demo.Application.Service.Base;
using MISA.WebFresher05.Demo.Application.Dto.Category;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity.Category;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Service
{
    public class CategoryService : BaseService<Category, CategoryDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryService
    {
        #region Field
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        #endregion

        #region Contructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="categoryManager"></param>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ICategoryManager categoryManager) : base(categoryRepository, mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        public override async Task<Category> MapCreateDtoToEntity(CategoryCreateDto entityCreateDto)
        {

            // Validate nghiệp vụ
            await _categoryManager.CheckCategoryExistByCode(entityCreateDto.fixed_asset_category_code);
            Category category = _mapper.Map<Category>(entityCreateDto);
            return category;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityUpdateDto"></param>
        /// <returns></returns>
        public override async Task<Category> MapUpdateDtoToEntity(Guid id, CategoryUpdateDto entityUpdateDto)
        {
            await _categoryManager.CheckCategoryExistByCode(entityUpdateDto.fixed_asset_category_code);
            Category existingCategory = await _baseRepository.GetAsync(id);
          
            _mapper.Map(entityUpdateDto, existingCategory);

            return existingCategory;
        }
        #endregion
    }
}
