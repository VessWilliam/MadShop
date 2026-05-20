
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Descriptions,
    string ImageFiles, decimal Price) : ICommand<UpdateProductResult>;


public record UpdateProductResult(bool IsSuccess);



public class UpdateProdutCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProdutCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(c => c.Id).NotEmpty().WithMessage("Product Id is required.");

        RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException(command.Id);

        product.Name = command.Name;
        product.Category = command.Category;
        product.Descriptions = command.Descriptions;
        product.ImageFiles = command.ImageFiles;
        product.Price = command.Price;

        session.Update(product);

        await session.SaveChangesAsync();

        return new UpdateProductResult(true);
    }
}
