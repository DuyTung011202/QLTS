using MISA.WebFresher05.Demo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain
{
    public class DepartmentManager : IDepartmentManager
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check trùng code
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns>Exception</returns>
        /// <exception cref="ConflictException">Lỗi mã trùng</exception>
        /// Created by: DDTung (19/07/2023)
        public async Task CheckDepartmentExistByCode(string code)
        {
            var departmentExist = await _departmentRepository.FindByCodeAsync(code);
            if (departmentExist != null)
            {
                throw new ConflictException("Mã phòng ban bị trùng");
            }
        } 
        #endregion
    }
}
