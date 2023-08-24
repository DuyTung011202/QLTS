using Dapper;
using MISA.WebFresher05.Demo.Domain.Entity.Base;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using MISA.WebFresher05.Demo.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Infrastructure.Repository.Base
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHasKey
    {
        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng trong db bị ảnh hưởng</returns>
        /// Created by: DDTung (19/07/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            {
                var procedure = $"Proc_{TableName}_Insert";

                var param = new DynamicParameters();

                var type = typeof(TEntity);
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = "@" + property.Name;
                    var propertyValue = property.GetValue(entity);
                    param.Add(propertyName, propertyValue);
                }

                var result = await _uow.Connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);

                return result;
            }
        }


        /// <summary>
        /// Hàm sửa bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng trong db bị ảnh hưởng</returns>
        /// Created by: DDTung (19/07/2023)
        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var procedure = $"Proc_{TableName}_Update";

            var param = new DynamicParameters();
            param.Add("@id", id);

            var type = typeof(TEntity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "@" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            var result = await _uow.Connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);

            return result;
        }



        /// <summary>
        /// Hàm xoá bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng trong db bị ảnh hưởng</returns>
        /// Created by: DDTung (19/07/2023)
        public async Task<int> DeleteAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            var sql = $"DELETE FROM {TableName} WHERE {TableId} = @id";
            param.Add("@id", entity.GetKey());
            var result = await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);
            return result;
        }



        /// <summary>
        /// Hàm xoá nhiều bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng trong db bị ảnh hưởng</returns>
        /// Created by: DDTung (19/07/2023)
        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {
           
            var sql = $"DELETE FROM {TableName} WHERE {TableId} IN @ids;";

            var param = new DynamicParameters();
            param.Add("ids", entities.Select(x => x.GetKey()));

            var result =  await _uow.Connection.ExecuteAsync(sql, param);

            return result;

           
        }


    }
}
