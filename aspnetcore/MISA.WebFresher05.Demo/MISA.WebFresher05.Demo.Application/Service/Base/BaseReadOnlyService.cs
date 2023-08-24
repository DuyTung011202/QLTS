using AutoMapper;
using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Application;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Service.Base
{
    public abstract class BaseReadOnlyService<TEntity,TEntityDto> : IReadOnlyService<TEntityDto>
    {
        #region Fields
        protected readonly IReadOnlyRepository<TEntity> _readOnlyRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructors
        protected BaseReadOnlyService(IReadOnlyRepository<TEntity> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

       

        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>list </returns>
        /// Created by: DDTung(19/07/2023)
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _readOnlyRepository.GetAllAsync();

            var entitiesDto = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entitiesDto;
        }


        /// <summary>
        /// Lấy tài sản theo id
        /// </summary>
        /// <returns>Tài sản</returns>
        /// Created by: DDTung(19/07/2023)
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetAsync(id);

           
            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }



        /// <summary>
        /// Hàm lọc dữ liệu và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchText">Tìm kiếm</param>
        /// <param name="department_id">Id phòng ban</param>
        /// <param name="fixed_asset_category_id">Id loại tài sản</param>
        /// <returns>Dữ liệu sau khi lọc </returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: DDTung(28/07/2023)
        public async Task<BaseFilterResponse<TEntityDto>> FilterAsync(int? pageSize, int? pageNumber, string? searchText, Guid department_id, Guid fixed_asset_category_id)
       {
            if(pageSize == null) { pageSize = 100000; }

            if (pageNumber == null || pageNumber < 1) { pageNumber = 1; }

            var entitiesNotPagging = await _readOnlyRepository.FilterAsync(100000, 1, searchText, department_id, fixed_asset_category_id);

            var totalRecord = entitiesNotPagging.Count();
            var totalPage = Convert.ToInt32(Math.Ceiling((double)totalRecord / (double)pageSize));

            if (pageNumber > totalPage)
            {
                pageNumber = totalPage;
            }

            var entities = await _readOnlyRepository.FilterAsync(pageSize, pageNumber, searchText, department_id, fixed_asset_category_id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            var currentPage = pageNumber;
            var currentPageRecords = entities.Count();

            var entitiesDto = _mapper.Map<List<TEntityDto>>(entities);

            var filterData = new BaseFilterResponse<TEntityDto>(totalPage, totalRecord, currentPage, currentPageRecords, entitiesDto);

            return filterData;
        }
        #endregion

    }
}
