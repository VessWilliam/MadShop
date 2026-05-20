


namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Descriptions,
    string ImageFiles, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(c => c.Category)
            .NotEmpty().WithMessage("At least one category is required.");
        RuleFor(c => c.Descriptions)
            .NotEmpty().WithMessage("Product description is required.");
        RuleFor(c => c.ImageFiles)
            .NotEmpty().WithMessage("Image file is required.");
        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Descriptions = command.Descriptions,
            ImageFiles = command.ImageFiles,
            Price = command.Price
        };


        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
