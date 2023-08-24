using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Application.Dto.Department
{
    public class DepartmentCreateDto
    {
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
