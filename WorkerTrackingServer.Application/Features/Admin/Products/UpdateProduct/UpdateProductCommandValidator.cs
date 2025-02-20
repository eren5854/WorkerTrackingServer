using FluentValidation;

namespace WorkerTrackingServer.Application.Features.Admin.Products.UpdateProduct;
public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Product Id is required");
        RuleFor(r => r.ProductName)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Product name max 200 character");

        RuleFor(r => r.ProductCode)
            .MaximumLength(200).WithMessage("Product code max 200 character");

        RuleFor(r => r.ProductDescription)
            .MaximumLength(2000).WithMessage("Product description max 2000 character");
    }
}
