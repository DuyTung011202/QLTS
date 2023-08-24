using AutoMapper;
using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity.Base;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Service.Base
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TEntityDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> where TEntity : IHasKey
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IUnitOfWork _uow;
        #endregion

        #region Constructors
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm thêm
        /// </summary>
        /// <param name="entityCreateDto">Dto</param>
        /// <returns>Bản ghi đc thêm</returns>
        /// Created by: DDTung (20/07/2023)
        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            /// Validate nghiệp vụ
            var entity = await MapCreateDtoToEntity(entityCreateDto);

            entity.SetKey(Guid.NewGuid());

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.created_date = DateTime.Now;
                baseAuditEntity.created_by = "Duy Tùng";
            }

            var result = await _baseRepository.InsertAsync(entity);

            return result;
        }

        /// <summary>
        /// Hàm sửa
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: DDTung (20/07/2023)
        public async virtual Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            /// Validate nghiệp vụ
            var entity = await MapUpdateDtoToEntity(id, entityUpdateDto);

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "Duy Tùng";
            }

            /// Insert vào db
            var result = await _baseRepository.UpdateAsync(id, entity);
            return result;
        }

        /// <summary>
        /// Hàm xoá theo id
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: DDTung (20/07/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            var result = await _baseRepository.DeleteAsync(entity);
            return result;
        }


        /// <summary>
        /// Hàm xoá theo list
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: DDTung (20/07/2023)
        public virtual async Task<int> DeleteManyAsync(List<Guid> ids)
        {

            if (ids.Count == 0)
            {
                throw new Exception("Không được truyền danh sách rỗng.");
            }

            var entities = await _baseRepository.GetListByIdsAsync(ids);


            if (entities.ToList().Count < ids.Count)
            {
                throw new Exception("Không thể xoá.");
            }
            var result = await _baseRepository.DeleteManyAsync(entities.ToList());
            return result;



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        public abstract Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityUpdateDto"></param>
        /// <returns></returns>
        public abstract Task<TEntity> MapUpdateDtoToEntity(Guid id, TEntityUpdateDto entityUpdateDto);
        #endregion;
    }
}
