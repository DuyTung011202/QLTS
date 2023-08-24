using Microsoft.AspNetCore.Mvc;
using MISA.Fresher05.Demo.Application;
using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Application.Dto.Product;
using MISA.WebFresher05.Demo.Domain.Entity.Product;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MISA.WebFresher05.Demo.Controllers.Base
{
    public class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        private readonly IReadOnlyService<TEntityDto> _readOnlyService;
        public BaseReadOnlyController(IReadOnlyService<TEntityDto> readOnlyService)
        {
            _readOnlyService = readOnlyService;
        }

      
        /// <summary>
        /// Lấy tất cả  
        /// </summary>
        /// <returns></returns>
        /// Created by: DDTung (12/07/2023)
        [HttpGet]
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var result = await _readOnlyService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Lấy chi tiết 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: DDTung (12/07/2023)
        /// 
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDto>> Get(Guid id)
        {
            var result = await _readOnlyService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }



        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự bao nhiêu</param>
        /// <param name="searchText">Chuỗi lọc</param>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: ddtung (24/07/2023) 
        [HttpGet("filter")]
        public async Task<IActionResult> FiltersAsync([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? searchText, [FromQuery] Guid department_id, [FromQuery] Guid fixed_asset_category_id)
        {
            var filterData = await _readOnlyService.FilterAsync(pageSize, pageNumber, searchText, department_id, fixed_asset_category_id);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }


      

    }
}
