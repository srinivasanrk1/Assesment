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
    public class ProductService : IProductService
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IAsyncRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddProductAsync(ProductDTO productDTO)
        {
            await _productRepository.AddAsync(_mapper.Map<Product>(productDTO));
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid Id)
        {
            var entity = await _productRepository.GetByIdAsync(Id);
            if (entity == null)
                return false;
            await _productRepository.DeleteAsync(entity);
            return true;
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var entity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(entity);
        }

        public async Task<IEnumerable<ProductDTO>> ListAllProductAsync()
        {
            var entity = await _productRepository.ListAllAsync();
            return _mapper.Map<List<ProductDTO>>(entity);
        }

        public async Task<bool> UpdateProductAsync(ProductDTO productDTO, Guid Id)
        {
            var entity = await _productRepository.GetByIdAsync(Id);
            if (entity == null)
                return false;
            _mapper.Map(productDTO, entity);
            await _productRepository.UpdateAsync(entity);
            return true;
        }
    }
}
