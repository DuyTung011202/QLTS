using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Entity.Base
{
    public abstract class BaseAuditEntity
    {
        ///// <summary>
        ///// người tạo
        ///// </summary>
        public string? created_by { get; set; }
        ///// <summary>
        ///// ngày tạo
        ///// </summary>
        public DateTime? created_date { get; set; }
        ///// <summary>
        ///// người sửa
        ///// </summary>
        public string? modified_by { get; set; }
        ///// <summary>
        ///// ngày sửa
        ///// </summary>
        public DateTime? modified_date { get; set; }
    }
}
