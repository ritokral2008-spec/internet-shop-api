using FluentValidation;
using InternetShop.DTOs.OrderItems;

namespace InternetShop.DTOs.Validators.OrderItem
{
    public class UpdateOrderItemValidator
        :AbstractValidator<UpdateOrderItemDto>,
        IValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator()
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
