using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Application.Dto.Product
{
    /// <summary>
    /// Model tạo mới một tài sản
    /// </summary>
    /// DDTung 08.08.2023
    public class ProductCreateDto
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [MaxLength(100)]
        public string fixed_asset_code { get; set; }

        ///// <summary>
        ///// Tên tài sản
        ///// </summary>
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [Required]
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        ///id tài sản
        /// </summary>
        [Required]
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        ///mã loại tài sản
        /// </summary>
        public string fixed_asset_category_code { get; set; }

        /// <summary>
        /// tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// ngay mua
        /// </summary>
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// ngay su dung
        /// </summary>
        public DateTime? used_date { get; set; }

        ///// <summary>
        ///// nguyên giá
        ///// </summary>
        [Range(0, 100000000000)]
        public decimal? cost { get; set; }

        ///// <summary>
        ///// số lượng
        ///// </summary>
        [Range(0, 100000000)]
        public int? quantity { get; set; }

        ///// <summary>
        ///// tỉ lệ hao mòn 
        ///// </summary>
        public float? depreciation_rate { get; set; }

        ///// <summary>
        ///// giá trị hao mòn 
        ///// </summary>
        [Range(0, 10000000000)]
        public decimal? depreciation_value { get; set; }

        ///// <summary>
        ///// năm bắt đầu  theo dõi
        ///// </summary>
        public DateTime? tracked_year { get; set; }


        public int active { get; set; }

        ///// <summary>
        /////số năm sử dụng
        ///// </summary>
        public int? life_time { get; set; }


        ///// <summary>
        /////  năm sd (2023)
        ///// </summary>
        public int? production_year { get; set; }



    }
}
