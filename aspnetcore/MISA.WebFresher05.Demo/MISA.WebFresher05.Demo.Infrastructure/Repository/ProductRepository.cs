using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Demo.Domain.Entity.Product;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Infrastructure.Repository.Base;
using MySqlConnector;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher05.Demo.Infrastructure
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {


        #region Fields
        public override string TableName { get; protected set; } = "fixed_asset";
        public override string TableId { get; protected set; } = "fixed_asset_id";
        #endregion


        #region Constructor
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion


        /// <summary>
        /// Tìm tài sản qua code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Created by: DDTung (20/07/2023)
        #region Methods
        public async Task<Product?> FindByCodeAsync(string code)
        {
            var sql = $"Proc_{TableName}_GetByCode";
            DynamicParameters? param = new DynamicParameters();
            param.Add("@code", code);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<Product>(sql, param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Tìm mã code lớn nhất
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Max code</returns>
        /// Created by: DDTung (20/07/2023)
        public async Task<string> GetMaxCodeAsync()
        {
            var sql = $"Proc_{TableName}_GetMaxCode";
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<string>(sql, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion


       

    }
}
