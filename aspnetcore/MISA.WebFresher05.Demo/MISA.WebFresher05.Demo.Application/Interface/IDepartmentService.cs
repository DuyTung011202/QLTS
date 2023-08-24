using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Application.Dto.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Interface
{
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {

    }
}
