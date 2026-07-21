using FluentValidation;
using InternetShop.DTOs.Categories;
using InternetShop.DTOs.Products;

namespace InternetShop.DTOs.Validators.Categories
{
    public class UpdateCategoryValidator
        :AbstractValidator<UpdateCategoryDto>,
        IValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required")
                .MaximumLength(100)
                .WithMessage("Category name cannot excced 100 characters");
        }
    }
}
