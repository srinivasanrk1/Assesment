using AssesmentAPI.Core.Interface;
using AssesmentAPI.Core.Modal;
using AssesmentAPI.Data;
using AssesmentAPI.Data.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Service
{
    public class InventoryService : Interface.IInventoryService
    {
        private readonly IAsyncRepository<Product> _productRepository;

        public InventoryService(IAsyncRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public Dictionary<string, int> ProductCountByStatus()
        {
            return _productRepository.FindAll().AsEnumerable().GroupBy(o => o.Status).
                 ToDictionary(o => o.Key, o => o.Count());
        }

        public async Task<Tuple<bool, string>> SellProduct(Guid productId)
        {
            var entity = await _productRepository.GetByIdAsync(productId);
            if (entity == null)
                return new Tuple<bool, string>(false, "Product not exists");

            if (entity.Status == StatusEnum.Damaged.ToString())
                return new Tuple<bool, string>(false, "Product is damaged cannot be sold");
            else if (entity.Status == StatusEnum.Sold.ToString())
                return new Tuple<bool, string>(false, "Product is already sold, check with id");

            var result = await UpdateProductStatusById(productId, StatusEnum.Sold);
            return new Tuple<bool, string>(true, "Product sold!");

        }

        public async Task<Tuple<bool, string>> UpdateProductStatus(Guid productId, StatusEnum statusEnum)
        {
            var entity = await _productRepository.GetByIdAsync(productId);
            if (entity == null)
                return new Tuple<bool, string>(false, "Product not exists");

            if (entity.Status == StatusEnum.Sold.ToString())
                return new Tuple<bool, string>(false, "Product is already sold, cannot update any status");

            var result = await UpdateProductStatusById(productId, statusEnum);
            return new Tuple<bool, string>(true, "Product status updated!");

        }

        private async Task<bool> UpdateProductStatusById(Guid productId, StatusEnum statusEnum)
        {
            var entity = await _productRepository.GetByIdAsync(productId);
            if (entity == null)
                return false;
            entity.Status = statusEnum.ToString();
            await _productRepository.UpdateAsync(entity);
            return true;
        }
    }
}
