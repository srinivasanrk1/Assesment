using AssesmentAPI.Core.Interface;
using AssesmentAPI.Core.Modal;
using AssesmentAPI.Data;
using AssesmentAPI.Data.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            await _categoryRepository.AddAsync(_mapper.Map<Category>(categoryDTO));
            return true;
        }

        public async Task<Tuple<bool, string>> DeleteCategoryAsync(Guid Id)
        {
            try
            {
                var entity = await _categoryRepository.GetByIdAsync(Id);
                if (entity == null)
                    return new Tuple<bool, string>(false, "Category not exists");
                await _categoryRepository.DeleteAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                return new Tuple<bool, string>(false, "Category already mapped to product unable to delete");
            }
            return new Tuple<bool, string>(true, "Deleted");
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task<IEnumerable<CategoryDTO>> ListAllCategoryAsync()
        {
            var entity = await _categoryRepository.ListAllAsync();
            return _mapper.Map<List<CategoryDTO>>(entity);
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO, Guid Id)
        {
            var entity = await _categoryRepository.GetByIdAsync(Id);
            if (entity == null)
                return false;
            _mapper.Map(categoryDTO, entity);
            await _categoryRepository.UpdateAsync(entity);
            return true;
        }
    }
}
