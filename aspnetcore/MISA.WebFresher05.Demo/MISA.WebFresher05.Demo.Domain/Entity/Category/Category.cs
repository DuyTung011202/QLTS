using MISA.WebFresher05.Demo.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Entity.Category
{
    public class Category : BaseAuditEntity, IHasKey
    {
        /// <summary>
        /// Id loại lài sản
        /// </summary>
        [Required]
        public Guid fixed_asset_category_id { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? fixed_asset_category_code { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? fixed_asset_category_name { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public float? depreciation_rate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid GetKey()
        {
            return fixed_asset_category_id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void SetKey(Guid id)
        {
            fixed_asset_category_id = id;
        }
    }
}
