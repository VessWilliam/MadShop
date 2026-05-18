using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {

    }

    public ProductNotFoundException(string category) : base("Product Category", category)
    {


    }
}
