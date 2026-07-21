using FluentValidation;
using InternetShop.DTOs.Categories;

namespace InternetShop.DTOs.Validators.Categories
{
    public class CreateCategoryValidator
        :AbstractValidator<CreateCategoryDto>,
        IValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required")
                .MaximumLength(100)
                .WithMessage("category name cannot exceed 100 characters");
        }
    }
}
