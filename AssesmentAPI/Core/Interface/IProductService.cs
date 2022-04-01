using AssesmentAPI.Core.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Interface
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDTO>> ListAllProductAsync();
        Task<bool> AddProductAsync(ProductDTO productDTO);
        Task<bool> UpdateProductAsync(ProductDTO  productDTO, Guid Id);
        Task<bool> DeleteProductAsync(Guid Id);
    }
}
