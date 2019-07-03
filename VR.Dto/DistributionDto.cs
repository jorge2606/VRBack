using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using VR.Data.Interfaces;
using VR.Data.Model;
using VR.Dto.User;

namespace VR.Dto
{
    public class DistributionValidator : AbstractValidator<DistributionBaseDto>
    {
        public DistributionValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithName("Descripción");
            RuleFor(x => x.Name).NotEmpty().WithName("Nombre");
            RuleFor(x => x.Name).MaximumLength(100).WithName("Nombre");
            RuleFor(x => x.Description).MaximumLength(100).WithName("Descripción");
        }
    }

    public class DistributionBaseDto : IsDeletedInterface
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid OrganismId { set; get; }
        public OrganismBaseDto Organism { set; get; }
        public List<UserDto> Users { set; get; }
        public Boolean IsDeleted { set; get; }

    }

    public class CreateDistributionDto : DistributionBaseDto { }

    public class UpdateDistributionDto : DistributionBaseDto { }

    public class AllDistributionDto : DistributionBaseDto
    {
        public new List<UserDistribution> Users { set; get; }
    }

    public class DeleteDistributionDto : DistributionBaseDto { }

    public class FindByIdDistributionDto : DistributionBaseDto { }

    public class FindByIdOrganismIdDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Boolean IsDeleted { set; get; }
        public Guid OrganismId { set; get; }
    }

    public class FilterDistributionDto
    {
        public int? Page { set; get; }
        public string Name { set; get; }
        public Guid? OrganismId { set; get; }
    }

}
