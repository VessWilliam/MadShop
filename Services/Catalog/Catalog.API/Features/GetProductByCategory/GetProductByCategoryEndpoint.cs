
namespace Catalog.API.Products.GetProductByCategory;


public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;


public record GetProductByCategoryResult(IEnumerable<Product> Products);


public class GetProductByCategoryEndpoint(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().Where(i => i.Category.Contains(query.category)).ToListAsync(cancellationToken);

        return product is null ? throw new ProductNotFoundException(query.category) : new GetProductByCategoryResult(product);
    }
}
