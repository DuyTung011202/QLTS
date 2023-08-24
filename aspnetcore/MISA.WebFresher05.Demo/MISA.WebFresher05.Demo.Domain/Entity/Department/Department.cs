using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher05.Demo.Domain.Entity.Base;

namespace MISA.WebFresher05.Demo.Domain.Entity.Department
{
    public class Department : BaseAuditEntity, IHasKey
    {
        /// <summary>
        /// Id phòng ban
        /// </summary>
        [Required]
        public Guid department_id { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? department_code { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? department_name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid GetKey()
        {
            return department_id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void SetKey(Guid id)
        {
            department_id = id;
        }
    }
}