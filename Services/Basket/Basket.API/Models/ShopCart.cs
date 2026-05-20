namespace Basket.API.Models;

public class ShopCart
{
    public string UserName { get; set; } = default!;

    public List<ShopCartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    public ShopCart(string username) => UserName = username;

    public ShopCart() { }

}
