using AutoMapper;
using MISA.Fresher05.Demo.Application.Interface;
using MISA.Fresher05.Demo.Application.Service.Base;
using MISA.WebFresher05.Demo.Application.Dto.Department;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity.Department;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher05.Demo.Application.Service
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Field
        private readonly IDepartmentManager _departmentManager;
        private readonly IMapper _mapper;

        #endregion

        #region Contructors

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IDepartmentManager departmentManager) : base(departmentRepository, mapper)
        {
            _departmentManager = departmentManager;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Map từ CreatDto sang Entity
        /// </summary>
        /// <param name="id">Id truyền vào</param>
        /// <param name="entityCreateDto">>Dto cần map</param>
        /// <returns>Entity</returns>
        /// Author: DDTung 22/07/2023
        // Validate nghiệp vụ
        public override async Task<Department> MapCreateDtoToEntity(DepartmentCreateDto entityCreateDto)
        {

         
            await _departmentManager.CheckDepartmentExistByCode(entityCreateDto.department_code);
            Department department = _mapper.Map<Department>(entityCreateDto);
            return department;

        }

        /// <summary>
        /// Map từ CreatDto sang Entity
        /// </summary>
        /// <param name="id">Id truyền vào</param>
        /// <param name="entityCreateDto">>Dto cần map</param>
        /// <returns>Entity</returns>
        /// Author: DDTung 22/07/2023

        public override async Task<Department> MapUpdateDtoToEntity(Guid id, DepartmentUpdateDto entityUpdateDto)
        {
            await _departmentManager.CheckDepartmentExistByCode(entityUpdateDto.department_code);
            Department existingDepartment = await _baseRepository.GetAsync(id);
          

            _mapper.Map(entityUpdateDto, existingDepartment);

            return existingDepartment;
        } 
        #endregion
    }
}
