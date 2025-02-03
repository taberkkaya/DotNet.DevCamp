using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models;

public class Card
{
    public List<CardItem> Items { get; set; } = new List<CardItem>();
    public void AddItem(Product product, int quantity)
    {
        var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();

        if (item == null)
        {
            Items.Add(new CardItem { Product = product, Quantity = quantity });
        }
        else
        {
            item.Quantity += quantity;
        }

    }
    public void RemoveItem(Product product)
    {
        Items.RemoveAll(i => i.Product.Id == product.Id);
    }
    public decimal CalculateTotal()
    {
        return Items.Sum(i => i.Product.Price * i.Quantity);
    }

    public void Clear()
    {
        Items.Clear();
    }
}

public class CardItem
{
    public int CardItemId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}
