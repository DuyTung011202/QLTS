using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;
using MISA.Fresher05.Demo.Application.Interface;
using MISA.WebFresher05.Demo.Controllers.Base;
using MISA.WebFresher05.Demo.Application.Dto.Product;
using MISA.Fresher05.Demo.Application.Interface.Base;
using OfficeOpenXml;
using System.Globalization;
using OfficeOpenXml.Style;

namespace MISA.WebFresher05.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<ProductDto, ProductCreateDto, ProductUpdateDto>
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Lấy mã code mới
        /// </summary>
        /// <returns></returns>
        /// createdby: ddtung 19/07/2023
        [HttpGet("code")]
        public async Task<IActionResult> GetNewCode()
        {
            var newCode = await _productService.GetNewCodeAsync();
            return Ok(newCode);
        }


        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // Fetch data from the database using the service
            var listProduct = await _productService.GetAllAsync(); // Replace GetAllAsync with the appropriate method to fetch data from the database

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
                var workSheet = package.Workbook.Worksheets.Add("Danh sách tài sản");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Value = "STT";
                workSheet.Cells[1, 2].Value = " Mã tài sản ";
                workSheet.Cells[1, 3].Value = " Tên tài sản ";
                workSheet.Cells[1, 4].Value = " Tên loại tài sản ";
                workSheet.Cells[1, 5].Value = " Tên bộ phận sử dụng ";
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[1, 6].Value = " Số lượng ";
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[1, 7].Value = " Nguyên giá ";
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[1, 8].Value = " KH/HM lũy kế ";
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[1, 9].Value = " Giá trị còn lại ";
                int recordIndex = 2;
                foreach (var product in listProduct)
                {
                    workSheet.Cells[recordIndex, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 2].Value = product.fixed_asset_code;
                    workSheet.Cells[recordIndex, 3].Value = product.fixed_asset_name;
                    workSheet.Cells[recordIndex, 4].Value = product.fixed_asset_category_name;
                    workSheet.Cells[recordIndex, 5].Value = product.department_name;
                    workSheet.Cells[recordIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[recordIndex, 6].Value = product.quantity;
                    workSheet.Cells[recordIndex, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[recordIndex, 7].Value = double.Parse(product.cost.ToString()).ToString("#,###", cul.NumberFormat);
                    workSheet.Cells[recordIndex, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[recordIndex, 8].Value = double.Parse(product.depreciation_value.ToString()).ToString("#,###", cul.NumberFormat);
                    var residualValue = product.cost - product.depreciation_value;
                    workSheet.Cells[recordIndex, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[recordIndex, 9].Value = double.Parse(residualValue.ToString()).ToString("#,###", cul.NumberFormat);
                    recordIndex++;

                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"DataExport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}
