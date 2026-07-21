using FluentValidation;
using InternetShop.DTOs.OrderItems;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.DTOs.Validators.OrderItem
{
    public class CreateOrderItemValidator
        :AbstractValidator<CreateOrderItemDto>,
        IValidator<CreateOrderItemDto>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Item must have productId more than zero");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity cannot be zero or lower");
        }
    }
}
