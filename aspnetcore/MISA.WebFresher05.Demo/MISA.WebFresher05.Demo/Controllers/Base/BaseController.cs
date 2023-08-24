using Microsoft.AspNetCore.Mvc;
using MISA.Fresher05.Demo.Application.Interface.Base;
using MISA.WebFresher05.Demo.Application.Dto.Product;
using MISA.WebFresher05.Demo.Domain.Entity.Product;
using OfficeOpenXml;
using System.Collections.Generic;

namespace MISA.WebFresher05.Demo.Controllers.Base
{
    public class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyController<TEntityDto>
    {
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
       
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }


        /// <summary>
        /// Hàm thêm
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// Created by: DDTung (20/07/2023)
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await _baseService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

       
        /// <summary>
        /// Hàm sửa
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// Created by: DDTung (20/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await _baseService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        /// <summary>
        /// Xoá dữ liệu dựa trên id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: DDTung (12/07/2023)
        /// 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);

        }
        /// <summary>
        /// Xoá list dữ liệu dựa trên id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: DDTung (12/07/2023)
        /// 
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK);

        }

       

    }
}
