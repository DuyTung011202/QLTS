using Dapper;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using MISA.WebFresher05.Demo.Infrastructure.UnitOfWork;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Infrastructure.Repository.Base
{
    public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        #region Fields
        protected readonly IUnitOfWork _uow;

        public virtual string TableName { get; protected set; } = typeof(TEntity).Name;
        public virtual string TableId { get; protected set; } = typeof(TEntity).Name;
        #endregion

        #region Constructors
        protected BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Hàm đếm số bản ghi trong db
        /// </summary>
        /// <returns>Số bản ghi trong db</returns>
        /// Created by: ddtung (24/07/2023)
        public async Task<int> CountAsync()
        {
            var sql = $"SELECT COUNT(*) FROM {TableName};";

            var countEntities = await _uow.Connection.ExecuteScalarAsync<int>(sql, null);

            return countEntities;
        }
        /// <summary>
        /// Hàm lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}";
            var result = await _uow.Connection.QueryAsync<TEntity>(sql, transaction: _uow.Transaction);
            return result;

        }

        /// <summary>
        /// Hàm lấy dữ liệu theo id
        /// </summary>
        /// <param name="id">Id của t</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Không tìm thấy theo id " + id);
            }
            return entity;

        }
        /// <summary>
        /// Hàm tìm dữ liệu theo id
        /// </summary>
        /// <param name="id">Id của dữ liệu truyền vào</param>
        /// <returns>Tài sản</returns>
        /// Created by: DDTung (24/07/2023)
        public async Task<TEntity?> FindAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableId} = @id";
            DynamicParameters? param = new DynamicParameters();
            param.Add("@id", id);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction: _uow.Transaction);
            return result;
        }


        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: DDTung (24/07/2023)
        public async Task<IEnumerable<TEntity>> FilterAsync(int? pageSize, int? pageNumber, string? searchText, Guid department_id, Guid fixed_asset_category_id)
        {
            var procedure = $"Proc_{TableName}_Filter";

            var param = new DynamicParameters();
            param.Add("@pageSize", pageSize);
            param.Add("@pageNumber", pageNumber);
            param.Add("@searchText", searchText);
            if (department_id != Guid.Empty)
            {
                param.Add("@department_id", department_id);
            }
            else
            {
                param.Add("@department_id", null);
            }


            if (fixed_asset_category_id != Guid.Empty)
            {
                param.Add("@fixed_asset_category_id", fixed_asset_category_id);
            }
            else
            {
                param.Add("@fixed_asset_category_id", null);
            }
            var entities = await _uow.Connection.QueryAsync<TEntity>(procedure, param, commandType: CommandType.StoredProcedure);

            return entities;
        }
        /// <summary>
        /// Hàm lấy list dữ liệu dựa trên id
        /// </summary>
        /// <param name="ids">List id truyền vào</param>
        /// <returns>List</returns>
        /// Created by: DDTung (24/07/2023)
        public async Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            List<TEntity> entities = new List<TEntity>();

            foreach (var id in ids)
            {
                var entity = await GetAsync(id);

                entities.Add(entity);
            }

            return entities;
        }


        #endregion
    }
}
