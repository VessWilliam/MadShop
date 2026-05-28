namespace Discount.GRPC.Models;

public class Coupons
{
    public int Id { get; set; } = default;

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Amount { get; set; } = default;
}
