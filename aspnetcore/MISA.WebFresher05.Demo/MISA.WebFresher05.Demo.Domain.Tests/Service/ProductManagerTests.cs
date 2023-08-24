using MISA.WebFresher05.Demo.Domain.Entity.Product;
using MISA.WebFresher05.Demo.Domain.Interface;
using MISA.WebFresher05.Demo.Domain;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher05.Demo.Domain.Entity.Product;

namespace MISA.WebFresher05.Domain
{
    [TestFixture]
    public class ProductManagerTests
    {
        /// <summary>
        /// Test với đầu vào là mã phòng ban đã tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ddtung (23/07/2023) 
        [Test]
        public async Task CheckProductExitsByCodeAsync_ExistProduct_ConflictException()
        {
            // Arrange
            var code = "Hello";

            var productRepository = Substitute.For<IProductRepository>();
            productRepository.FindByCodeAsync(code).Returns(new Product());
            var productManager = new ProductManager(productRepository);

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await productManager.CheckProductExistByCode(code));

            await productRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test với đầu vào là mã phòng ban chưa tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created by: ddtung (23/07/2023) 
        [Test]
        public async Task CheckProductExitsByCodeAsync_NotExistProduct_Success()
        {
            // Arrange
            var code = "Hello";

            var productRepository = Substitute.For<IProductRepository>();
            var productManager = new ProductManager(productRepository);
            productRepository.FindByCodeAsync(code).ReturnsNull();

            // Act
            await productManager.CheckProductExistByCode(code);

            // Assert 
            await productRepository.Received(1).FindByCodeAsync(code);
        }
    }
}