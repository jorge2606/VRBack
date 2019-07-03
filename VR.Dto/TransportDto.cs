using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto.User;

namespace VR.Dto
{

    public class TransportValidator : AbstractValidator<TransportBaseDto>
    {
        public TransportValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Marca");

            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Tipo");

            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Modelo");

            RuleFor(x => x.CarPlate)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Patente");

        }
    }
    public class TransportBaseDto
    {
        public Guid Id { set; get; }
        public string Type { set; get; }
        public string Model { set; get; }
        public string Brand { set; get; }
        public string CarPlate { set; get; }
        public Boolean IsDeleted { set; get; }
    }

    public class TransportCreateAccountForDto
    {
        public Guid Id { set; get; }
        public string Type { set; get; }
        public string Model { set; get; }
        public string Brand { set; get; }
        public string CarPlate { set; get; }
        public string Description { set; get; }
    }

    public class CreateTransportDto : TransportBaseDto { }

    public class UpdateTransportDto : TransportBaseDto { }

    public class DeleteTransportDto : TransportBaseDto { }

    public class FindByIdTransportDto : TransportBaseDto { }

    public class GetAllTransportDto : TransportBaseDto { }

    public class ListTransports
    {
        public Guid Id { set; get; }
        public string Model { set; get; }
        public string Brand { set; get; }
        public string CarPlate { set; get; }
        public string Description { set; get; }
        public Boolean IsDeleted { set; get; }
    }

    public class CarIsBeingUsedByOtherSolicitation
    {
        public Guid Id { set; get; }
        public DateDto StartDate { set; get; }
        public int Days { set; get; }
        public Guid UserId { set; get; }
    }

    public class FilterTransportDto : FilterBaseDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CarPlate { get; set; }
    }

}
