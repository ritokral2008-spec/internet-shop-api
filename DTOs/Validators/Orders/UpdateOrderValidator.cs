using FluentValidation;
using InternetShop.DTOs.Orders;
using InternetShop.DTOs.Validators.OrderItem;

namespace InternetShop.DTOs.Validators.Orders
{
    public class UpdateOrderValidator
        : AbstractValidator<UpdateOrderDto>,
        IValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contain items");
            RuleForEach(x => x.Items)
                .SetValidator(new UpdateOrderItemValidator());
        }
    }
}
