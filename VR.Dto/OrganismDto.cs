using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace VR.Dto
{
    public class OrganismValidator : AbstractValidator<OrganismBaseDto>
    {
        public OrganismValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithName("Descripción");
            RuleFor(x => x.Name).NotEmpty().WithName("Nombre");
            RuleFor(x => x.Name).MaximumLength(100).WithName("Nombre");
            RuleFor(x => x.Description).MaximumLength(100).WithName("Descripción");
        }
    }

    public class OrganismBaseDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Boolean IsDeleted { set; get; }
    }

    public class CreateOrganismDto : OrganismBaseDto { }

    public class UpdateOrganismDto : OrganismBaseDto { }

    public class DeleteOrganismDto : OrganismBaseDto { }

    public class FindByIdOrganismDto : OrganismBaseDto { }

    public class GetallOrganismDto : OrganismBaseDto { }

    public class FilterOrganismDto
    {
        public int? Page { set; get; }
        public string Name { set; get; }
    }



}
