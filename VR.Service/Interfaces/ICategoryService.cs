using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ICategoryService
    {
        ServiceResult<CreateCategoryDto> CreateCategory(CreateCategoryDto categoryDto);
        ServiceResult<UpdateCategoryDto> UpdateCategory(UpdateCategoryDto categoryDto);
        ServiceResult<DeleteCategoryDto> DeleteCategory(Guid id);
        ServiceResult<FindByIdCategoryDto> FindByIdCategory(Guid id);
        ServiceResult<List<AllCategoryDto>> GetAllCategories();
    }
}
