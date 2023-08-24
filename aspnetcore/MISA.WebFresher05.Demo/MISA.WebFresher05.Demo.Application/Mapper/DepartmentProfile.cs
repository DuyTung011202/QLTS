using AutoMapper;
using MISA.WebFresher05.Demo.Application.Dto.Department;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity.Department;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
