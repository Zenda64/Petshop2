using System;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    static ProductLogic productLogic = new ProductLogic();

    static void Main()
    {
        Console.WriteLine("Press 1 to add a product");
        Console.WriteLine("Press 2 to get a DogLeash by name");
        Console.WriteLine("Type 'exit' to quit");

        string userInput = Console.ReadLine();

        while (userInput.ToLower() != "exit")
        {
            if (userInput == "1")
            {
                Console.WriteLine("Enter product type (1 for CatFood, 2 for DogLeash):");
                string productType = Console.ReadLine();

                switch (productType)
                {
                    case "1":
                        CatFood catFood = new CatFood();
                        Console.Write("Enter Name: ");
                        catFood.Name = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        catFood.Price = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        catFood.Quantity = int.Parse(Console.ReadLine());
                        Console.Write("Enter Description: ");
                        catFood.Description = Console.ReadLine();
                        Console.Write("Enter Weight in Pounds: ");
                        catFood.WeightPounds = double.Parse(Console.ReadLine());
                        Console.Write("Is it Kitten Food? (true/false): ");
                        catFood.KittenFood = bool.Parse(Console.ReadLine());

                        productLogic.AddProduct(catFood);
                        Console.WriteLine("CatFood added successfully!");
                        break;

                    case "2":
                        Console.Write("Enter DogLeash name to retrieve: ");
                        string leashName = Console.ReadLine();
                        DogLeash retrievedLeash = productLogic.GetDogLeashByName(leashName);

                        if (retrievedLeash != null)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(retrievedLeash));
                        }
                        else
                        {
                            Console.WriteLine($"DogLeash with name '{leashName}' not found.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }

            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to get a DogLeash by name");
            Console.WriteLine("Type 'exit' to quit");
            userInput = Console.ReadLine();
        }
    }
}

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}

class CatFood : Product
{
    public double WeightPounds { get; set; }
    public bool KittenFood { get; set; }
}

class DogLeash : Product
{
    public int LengthInches { get; set; }
    public string Material { get; set; }
}

class ProductLogic
{
    private List<Product> _products;
    private Dictionary<string, DogLeash> _dogLeashDictionary;
    private Dictionary<string, CatFood> _catFoodDictionary;

    public ProductLogic()
    {
        _products = new List<Product>();
        _dogLeashDictionary = new Dictionary<string, DogLeash>();
        _catFoodDictionary = new Dictionary<string, CatFood>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);

        if (product is DogLeash leash)
        {
            _dogLeashDictionary.Add(leash.Name, leash);
        }
        else if (product is CatFood catFood)
        {
            _catFoodDictionary.Add(catFood.Name, catFood);
        }
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public DogLeash GetDogLeashByName(string name)
    {
        if (_dogLeashDictionary.ContainsKey(name))
        {
            return _dogLeashDictionary[name];
        }
        else
        {
            return null;
        }
    }
}
