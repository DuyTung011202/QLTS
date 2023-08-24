using MISA.Fresher05.Demo.Application.Service;
using MISA.WebFresher05.Demo.Domain.Interface;
using NUnit.Framework;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher05.Demo.Domain.Entity.Product;

namespace MISA.WebFresher05.Infrastructure
{
    [TestFixture]
    public class ProductServiceTests
    {
        public IProductRepository productRepository { get; set; }
        public IMapper mapper { get; set; }
        public IProductManager productManager { get; set; }
        public IUnitOfWork unitOfWork { get; set; }
        public ProductService productService { get; set; }

        [SetUp]
        public void SetUp()
        {
            productRepository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
            productManager = Substitute.For<IProductManager>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            productService = new ProductService(productRepository, mapper, productManager, unitOfWork);
        }

        /// <summary>
        /// Test với đầu vào là chuỗi rỗng
        /// </summary>
        /// <returns></returns>
        /// Created by: ddtung (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_EmptyList_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();

            var expectedMessage = "Không được truyền danh sách rỗng.";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await productService.DeleteManyAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).RollbackAsync();
        }

        /// <summary>
        /// Test với đầu vào có một hoặc nhiều id không hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: ddtung (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_List10Items_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();
            // Tạo 10 id 
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var expectedMessage = "Không thể xoá.";

            var products = new List<Product>();


            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await productService.DeleteManyAsync(ids));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
           await productRepository.Received(1).GetListByIdsAsync(ids);
            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).RollbackAsync();
        }

        /// <summary>
        /// Test với đầu vào là chuỗi hợp lệ
        /// </summary>
        /// <returns></returns>
        /// Created by: ddtung (23/07/2023) 
        [Test]
        public async Task DeleteManyAsync_List10Items_Success()
        {
            // Arrange
            var ids = new List<Guid>();
            // Tạo 10 id 
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            await unitOfWork.Received(1).BeginTransactionAsync();
            await unitOfWork.Received(1).CommitAsync();
        }
    }
}
