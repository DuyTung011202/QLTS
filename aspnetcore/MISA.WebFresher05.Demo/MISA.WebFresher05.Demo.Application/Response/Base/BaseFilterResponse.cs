namespace MISA.WebFresher05.Demo.Application
{
    /// <summary>
    /// Class để format dữ liệu trả về khi filter
    /// </summary>
    /// CreatedBy: DDTung (25/07/2023)
    public class BaseFilterResponse<TEntityDto>
    {
        #region Properties
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? total_page { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int? total_record { get; set; }

        /// <summary>
        /// Vị trí page hiện tại
        /// </summary>
        public int? current_page { get; set; }

        /// <summary>
        /// Tổng số bản ghi trên page hiện tại
        /// </summary>
        public int? current_page_records { get; set; }

        /// <summary>
        /// Dữ liệu trả về theo trang
        /// </summary>
        public List<TEntityDto>? data { get; set; }
        #endregion

        #region Constructor
        public BaseFilterResponse(int? totalPage, int? totalRecord, int? currentPage, int? currentPageRecords, List<TEntityDto>? listData)
        {
            total_page = totalPage;
            total_record = totalRecord;
            current_page = currentPage;
            current_page_records = currentPageRecords;
            data = listData;
        }
        #endregion
    }
}