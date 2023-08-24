using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Application.Dto.Department
{
    public class DepartmentDto
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
      


    }
}
