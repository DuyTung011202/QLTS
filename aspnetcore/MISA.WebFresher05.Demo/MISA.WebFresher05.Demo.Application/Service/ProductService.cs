using AutoMapper;
using MISA.Fresher05.Demo.Application.Interface;
using MISA.Fresher05.Demo.Application.Service.Base;
using MISA.WebFresher05.Demo.Application.Dto.Product;
using MISA.WebFresher05.Demo.Domain;
using MISA.WebFresher05.Demo.Domain.Entity;
using MISA.WebFresher05.Demo.Domain.Entity.Product;
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
    public class ProductService : BaseService<Product, ProductDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        #endregion
        #region Constructors
        public ProductService(IProductRepository productRepository, IMapper mapper, IProductManager productManager, IUnitOfWork unitOfWork) : base(productRepository, mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        #endregion

        #region Methods


        /// <summary>
        /// Sinh code mới cho tài sản
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Author: DDTung 22/07/2023
        public async Task<string> GetNewCodeAsync()
        {
            string newCode = "";
            var maxCode = await _productRepository.GetMaxCodeAsync();
           

            if (maxCode == null) return $"TS00001";

            // lấy vị trí xuất hiện của số đuôi
            int indexOfNumber = 0;
            for (int i = maxCode.Length - 1; i >= 0; i--)
            {
                if (maxCode[i] < '0' || maxCode[i] > '9')
                {
                    indexOfNumber = i + 1;
                    break;
                }
            }

            // nếu tồn tài số đuôi thì thực hiện cộng 1 không có thì thực hiện thêm đuôi là 0
            if (indexOfNumber < maxCode.Length)
            {
                // phần số thay đổi và độ dài của số đuôi
                var autoNumber = maxCode.Substring(indexOfNumber);
                int lengthOfAutoNumber = autoNumber.Length;

                string newNumber = (Int64.Parse(autoNumber) + 1).ToString();

                // nếu độ dài số đuôi nhỏ hơn số đuôi trước đó thì thực hiện nối '0' vào trước 
                if (newNumber.Length < lengthOfAutoNumber)
                {
                    for (int i = newNumber.Length; i < lengthOfAutoNumber; i++)
                    {
                        newNumber = '0' + newNumber;
                    }
                }

                newCode = maxCode.Substring(0, indexOfNumber) + newNumber;
            }
            else
            {
                newCode = maxCode + '0';
            }
            return newCode;
        }

        /// <summary>
        /// Map từ CreatDto sang Entity
        /// </summary>
        /// <param name="id">Id truyền vào</param>
        /// <param name="entityCreateDto">>Dto cần map</param>
        /// <returns>Entity</returns>
        /// Author: DDTung 22/07/2023
        public override async Task<Product> MapCreateDtoToEntity(ProductCreateDto entityCreateDto)
        {

            // Validate nghiệp vụ
            await _productManager.CheckProductExistByCode(entityCreateDto.fixed_asset_code);
            Product product = _mapper.Map<Product>(entityCreateDto);
            return product;

        }

        /// <summary>
        /// Map từ UpdateDto sang Entity
        /// </summary>
        /// <param name="id">Id truyền vào</param>
        /// <param name="entityUpdateDto">Dto cần map</param>
        /// <returns>Entity</returns>
        /// Author: DDTung 22/07/2023
        public override async Task<Product> MapUpdateDtoToEntity(Guid id, ProductUpdateDto entityUpdateDto)
        {
            Product existingProduct = await _baseRepository.GetAsync(id);

            string previousFixedAssetCode = existingProduct.fixed_asset_code;

            _mapper.Map(entityUpdateDto, existingProduct);

            if (previousFixedAssetCode != entityUpdateDto.fixed_asset_code)
            {
                // Kiểm tra sự tồn tại của sản phẩm mới
                await _productManager.CheckProductExistByCode(entityUpdateDto.fixed_asset_code);
            }


            return existingProduct;
        }
        #endregion
    }
}
