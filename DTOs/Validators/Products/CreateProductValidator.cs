using FluentValidation;
using InternetShop.DTOs.Products;

namespace InternetShop.DTOs.Validators.Products
{
    public class CreateProductValidator
        : AbstractValidator<CreateProductDto>,
        IValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");

            RuleFor(x => x.Stock)
                .GreaterThan(0)
                .WithMessage("Stock must be greater than zero");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category is required");
        }
    }
}
