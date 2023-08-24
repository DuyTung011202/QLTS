using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Application.Dto.Category
{
    public class CategoryCreateDto
    {
       
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
    }
}
