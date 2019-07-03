using System;
using FluentValidation;

namespace VR.Dto
{
    public class CategoryValidator : AbstractValidator<CategoryBaseDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithName("Descripción");
            RuleFor(x => x.Name).NotEmpty().WithName("Nombre");
            RuleFor(x => x.Name).MaximumLength(100).WithName("Nombre");
            RuleFor(x => x.Description).MaximumLength(100).WithName("Descripción");
            RuleFor(x => x.Advance).NotEmpty().WithName("Anticipo");

        }
    }
    public class CategoryBaseDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Boolean IsDeleted  { set; get; }
        public Decimal Advance { set; get; }
        public int Order { set; get; }
    }

    public class CreateCategoryDto : CategoryBaseDto { }

    public class UpdateCategoryDto : CategoryBaseDto { }

    public class AllCategoryDto : CategoryBaseDto { }

    public class DeleteCategoryDto : CategoryBaseDto { }

    public class FindByIdCategoryDto : CategoryBaseDto { }

    public class FilterCategoryDto
    {
        public int? Page { set; get; }
        public string Name { set; get; }
    }
}
