using AssesmentAPI.Core.Modal;
using AssesmentAPI.Core.Service;
using AssesmentAPI.Data;
using AssesmentAPI.Data.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class InventoryTest
    {
        private readonly DbContextOptions<InventoryDbContext> _options;
        public InventoryTest()
        {
            _options = RespositoryHelperTest.DbContextOptionsLocalDb();
            RespositoryHelperTest.CreateData(_options);

        }

        [Fact]
        public void Verify_By_Status_Count()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);

            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var expectedSoldCount = context.Products.Count(o => o.Status == StatusEnum.Sold.ToString());
            var expectedInStockCount = context.Products.Count(o => o.Status == StatusEnum.InStock.ToString());
            var expectedDamagedCount = context.Products.Count(o => o.Status == StatusEnum.Damaged.ToString());

            var response = inventoryService.ProductCountByStatus();

            Assert.Equal(expectedSoldCount, response[StatusEnum.Sold.ToString()]);
            Assert.Equal(expectedInStockCount, response[StatusEnum.InStock.ToString()]);
            Assert.Equal(expectedDamagedCount, response[StatusEnum.Damaged.ToString()]);
        }

        [Fact]
        public async Task Verify_By_Status_Count_If_Update_Status_And_GroupBy_Change()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);
            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var currentInstockCount = context.Products.Count(o => o.Status == StatusEnum.InStock.ToString());

            var entity = context.Products.Where(o => o.Status == StatusEnum.InStock.ToString()).First();
            entity.Status = StatusEnum.Damaged.ToString();
            await productTestRepo.UpdateAsync(entity);
            var response = inventoryService.ProductCountByStatus();

            //Assert
            Assert.NotEqual(currentInstockCount, response[StatusEnum.InStock.ToString()]);
        }

        [Fact]
        public async Task Update_Product_Status_Id_By_Status_Async()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);
            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var currentStock= context.Products.Where(o => o.Status != StatusEnum.Sold.ToString()).First();            
            var response = await inventoryService.UpdateProductStatus(currentStock.Id,StatusEnum.InStock);
            var updatedProduct = context.Products.Where(o => o.Id == currentStock.Id).First();

            //Assert
            Assert.Equal(StatusEnum.InStock.ToString(), updatedProduct.Status);
        }

        [Fact]
        public async Task Update_Product_Status_ShouldNot_If_Sold_Status_Async()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);
            var inventoryService = new InventoryService(productTestRepo);

            //Arrange
            var currentSoldStock = context.Products.Where(o => o.Status == StatusEnum.Sold.ToString()).First();
            var response = await inventoryService.UpdateProductStatus(currentSoldStock.Id, StatusEnum.InStock);
            var updatedProduct = context.Products.Where(o => o.Id == currentSoldStock.Id).First();

            //Assert
            Assert.Equal(StatusEnum.Sold.ToString(), updatedProduct.Status);
            Assert.False(response.Item1);
        }

        [Fact]
        public async Task Sell_Product_Change_Sold_Status_Async()
        {
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);
            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var currentInstock = context.Products.Where(o => o.Status == StatusEnum.InStock.ToString()).First();
            var response = await inventoryService.SellProduct(currentInstock.Id);
            var updatedProduct = context.Products.Where(o => o.Id == currentInstock.Id).First();

            //Assert
            Assert.Equal(StatusEnum.Sold.ToString(), updatedProduct.Status);
            Assert.True(response.Item1);
        }

        [Fact]
        public async Task Sell_Product_DoNotChange_Sold_Status_If_Damaged_Async()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);
            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var currentInstock = context.Products.Where(o => o.Status == StatusEnum.Damaged.ToString()).First();
            var response = await inventoryService.SellProduct(currentInstock.Id);
            var updatedProduct = context.Products.Where(o => o.Id == currentInstock.Id).First();

            //Assert
            Assert.Equal(StatusEnum.Damaged.ToString(), updatedProduct.Status);
            Assert.False(response.Item1);
            Assert.Equal("Product is damaged cannot be sold",response.Item2);

        }

        [Fact]
        public async Task Sell_Product_DoNotChange_Sold_Status_If_Solid_Async()
        {
            //Arrange
            //Initialize context
            using InventoryDbContext context = new InventoryDbContext(_options);
            //Intialize generic repo
            var productTestRepo = new EfRepository<Product>(context);

            var inventoryService = new InventoryService(productTestRepo);

            //Act
            var currentInstock = context.Products.Where(o => o.Status == StatusEnum.Sold.ToString()).First();
            var response = await inventoryService.SellProduct(currentInstock.Id);

            var updatedProduct = context.Products.Where(o => o.Id == currentInstock.Id).First();

            //Assert
            Assert.Equal(StatusEnum.Sold.ToString(), updatedProduct.Status);
            Assert.False(response.Item1);
            Assert.Equal("Product is already sold, check with id", response.Item2);

        }
    }
}
