using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class ShoppingListService
{
    private List<ShoppingItem> items = new List<ShoppingItem>();
    private const int MaxLists = 5;
    private const string ListsDirectory = "Lists"; // Päivitetty kansio

    public void AddItem(ShoppingItem item)
    {
        items.Add(item);
    }

    public List<ShoppingItem> GetItems()
    {
        return items;
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var item in items)
        {
            total += item.Price * item.Quantity;
        }
        return total;
    }

    public void UpdateItem(ShoppingItem item)
    {
        var existingItem = items.FirstOrDefault(i => i.Name == item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity = item.Quantity;
            existingItem.Price = item.Price;
        }
    }

    public void RemoveItem(string itemName)
    {
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item != null)
        {
            items.Remove(item);
        }
    }

    public void ClearList()
    {
        items.Clear();
    }

    public void SaveList(string listName)
    {
        if (!Directory.Exists(ListsDirectory))
        {
            Directory.CreateDirectory(ListsDirectory);
        }

        var files = Directory.GetFiles(ListsDirectory);
        if (files.Length >= MaxLists)
        {
            throw new InvalidOperationException("Maximum number of saved lists reached. Please delete an existing list before saving a new one.");
        }

        var filePath = Path.Combine(ListsDirectory, $"{listName}.json");
        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(filePath, json);
    }

    public void LoadList(string listName)
    {
        var filePath = Path.Combine(ListsDirectory, $"{listName}.json");
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            items = JsonSerializer.Deserialize<List<ShoppingItem>>(json);
        }
    }

    public void DeleteList(string listName)
    {
        var filePath = Path.Combine(ListsDirectory, $"{listName}.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public List<string> GetSavedLists()
    {
        if (!Directory.Exists(ListsDirectory))
        {
            return new List<string>();
        }

        return Directory.GetFiles(ListsDirectory).Select(Path.GetFileNameWithoutExtension).ToList();
    }
}