using AssesmentAPI.Core.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Interface
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<CategoryDTO>> ListAllCategoryAsync();
        Task<bool> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO, Guid Id);
        Task<Tuple<bool, string>> DeleteCategoryAsync(Guid Id);
    }
}
