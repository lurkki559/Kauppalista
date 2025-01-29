namespace Kauppalista.Data;

public class ShoppingList
{
    private List<Product> _shoppingList { get; set; }

    public ShoppingList()
    {
        _shoppingList = new List<Product>();
    }

    public void Add(Product product)
    {
        if (!_shoppingList.Contains(product))
        {
            _shoppingList.Add(product);
        }
        
    }

    public void Remove(Product product)
    {
        if (_shoppingList.Contains(product))
        {
            product.Remove(product);
        }
    }

    public void EmptyList()
    {
        _shoppingList.Clear();
    }

    public string GetListElements()//Getteri, jolla saadaan ostoslistan sisältö.
    {
        foreach (var item in _shoppingList)
        {
            return $"{item.Name} {item.Price} {item.Amount}";
        }
    }
}