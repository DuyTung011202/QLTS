

using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity.Category;
using MISA.WebFresher05.Demo.Domain.Entity.Department;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Infrastructure.Repository.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher05.Demo.Infrastructure
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        #region Fields

        public override string TableName { get; protected set; } = "fixed_asset_category";
        public override string TableId { get; protected set; } = "fixed_asset_category_id";
        #endregion

        #region Constructors
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion


        #region Methods
        /// <summary>
        /// Tìm loại tài sản qua code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Loại tài sản</returns>
        /// Author: DDTung (26/07/2023)
        public async Task<Category?> FindByCodeAsync(string code)
        {
            var sql = $"Proc_{TableName}_GetByCode";
            DynamicParameters? param = new DynamicParameters();
            param.Add("@code", code);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<Category>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion



    }
}
