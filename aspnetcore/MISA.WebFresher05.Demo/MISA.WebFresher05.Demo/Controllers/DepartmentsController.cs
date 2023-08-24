using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;
using MISA.Fresher05.Demo.Application.Interface;
using MISA.Fresher05.Demo.Application;
using MISA.WebFresher05.Demo.Controllers.Base;
using MISA.WebFresher05.Demo.Application.Dto.Department;

namespace MISA.WebFresher05.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {


        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
        }



    }
}
