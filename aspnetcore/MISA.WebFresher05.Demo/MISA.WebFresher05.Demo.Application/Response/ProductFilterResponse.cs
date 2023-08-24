using MISA.WebFresher05.Demo.Application.Dto.Product;

namespace MISA.WebFresher05.Demo.Application
{
    public class ProductFilterResponse : BaseFilterResponse<ProductDto>
    {
        public ProductFilterResponse(int? totalPage, int? totalRecord, int? currentPage, int? currentPageRecords, List<ProductDto>? listData) : base(totalPage, totalRecord, currentPage, currentPageRecords, listData)
        {
        }
    }
}