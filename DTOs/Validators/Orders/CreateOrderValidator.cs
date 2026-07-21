using FluentValidation;
using InternetShop.DTOs.Orders;
using InternetShop.DTOs.Validators.OrderItem;

namespace InternetShop.DTOs.Validators.Orders
{
    public class CreateOrderValidator
        : AbstractValidator<CreateOrderDto>,
        IValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contain items");
            RuleForEach(x => x.Items)
                .SetValidator(new CreateOrderItemValidator());
        }
    }
}
