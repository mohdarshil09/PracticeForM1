// Base product interface
using System;

public interface IProduct
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    Category Category { get; }
}

public enum Category { Electronics, Clothing, Books, Groceries }

// 1. Create a generic repository for products
public class ProductRepository<T> where T : class, IProduct
{
    private List<T> _products = new List<T>();

    // TODO: Implement method to add product with validation
    public void AddProduct(T product)
    {
        // Rule: Product ID must be unique
        // Rule: Price must be positive
        // Rule: Name cannot be null or empty
        // Add to collection if validation passes
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be null or empty.");

        if (product.Price <= 0)
            throw new ArgumentException("Price must be positive.");

        if (_products.Any(p => p.Id == product.Id))
            throw new InvalidOperationException("Product ID must be unique.");

        _products.Add(product);
    }

    // TODO: Create method to find products by predicate
    public IEnumerable<T> FindProducts(Func<T, bool> predicate)
    {
        // Should return filtered products
        return _products.Where(predicate);
    }

    // TODO: Calculate total inventory value
    public decimal CalculateTotalValue()
    {
        // Return sum of all product prices
        return _products.Sum(p => p.Price);
    }
}

// 2. Specialized electronic product
public class ElectronicProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Electronics;
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; }
}

// 3. Create a discounted product wrapper
public class DiscountedProduct<T> where T : IProduct
{
    private T _product;
    private decimal _discountPercentage;

    public DiscountedProduct(T product, decimal discountPercentage)
    {
        // TODO: Initialize with validation
        // Discount must be between 0 and 100
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Discount must be between 0 and 100.");

        _product = product;
        _discountPercentage = discountPercentage;
    }

    // TODO: Implement calculated price with discount
    public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

    // TODO: Override ToString to show discount details
    public override string ToString()
    {
        return $"{_product.Name} | Original: {_product.Price:C} | " +
               $"Discount: {_discountPercentage}% | Final: {DiscountedPrice:C}";
    }
}

// 4. Inventory manager with constraints
public class InventoryManager
{
    // TODO: Create method that accepts any IProduct collection
    public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
    {
        // a) Print all product names and prices
        // b) Find the most expensive product
        // c) Group products by category
        // d) Apply 10% discount to Electronics over $500

        Console.WriteLine("All Products:");
        foreach (var p in products)
            Console.WriteLine($"{p.Name} - {p.Price:C}");

        var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();

        Console.WriteLine($"\nMost Expensive: {mostExpensive?.Name} - {mostExpensive?.Price:C}");

        Console.WriteLine("\nGrouped by Category:");
        var grouped = products.GroupBy(p => p.Category);

        foreach (var group in grouped)
        {
            Console.WriteLine($"Category: {group.Key}");
            foreach (var item in group)
                Console.WriteLine($"  {item.Name}");
        }

        Console.WriteLine("\nApplying 10% discount to Electronics over $500:");

        foreach (var product in products
                     .Where(p => p.Category == Category.Electronics && p.Price > 500))
        {
            var discounted = new DiscountedProduct<IProduct>(product, 10);
            Console.WriteLine(discounted);
        }
    }

    // TODO: Implement bulk price update with delegate
    public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
        where T : IProduct
    {

        // Apply priceAdjuster to each product
        // Handle exceptions gracefully
        foreach (var product in products)
        {
            try
            {

                var newPrice = priceAdjuster(product);
                if (newPrice <= 0)
                    throw new InvalidOperationException("Updated price must be positive.");

                // Reflection-based update (since interface has no setter)
                var priceProperty = product.GetType().GetProperty("Price");
                priceProperty?.SetValue(product, newPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {product.Name}: {ex.Message}");
            }
        }
    }

    // 5. TEST SCENARIO: Your tasks:
    // a) Implement all TODO methods with proper error handling
    // b) Create a sample inventory with at least 5 products
    // c) Demonstrate:
    //    - Adding products with validation
    //    - Finding products by brand (for electronics)
    //    - Applying discounts
    //    - Calculating total value before/after discount
    //    - Handling a mixed collection of different product types
    class Program
    {
        static void Main()
        {
            var repo = new ProductRepository<IProduct>();

            try
            {
                repo.AddProduct(new ElectronicProduct
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200,
                    Brand = "Dell",
                    WarrantyMonths = 24
                });
                repo.AddProduct(new ElectronicProduct
                {
                    Id = 6,
                    Name = "IPhone",
                    Price = 1200,
                    Brand = "Dell",
                    WarrantyMonths = 24
                });

                repo.AddProduct(new ElectronicProduct
                {
                    Id = 2,
                    Name = "Smartphone",
                    Price = 800,
                    Brand = "Apple",
                    WarrantyMonths = 12
                });

                repo.AddProduct(new ElectronicProduct
                {
                    Id = 3,
                    Name = "Headphones",
                    Price = 300,
                    Brand = "Sony",
                    WarrantyMonths = 12
                });

                repo.AddProduct(new BookProduct
                {
                    Id = 4,
                    Name = "Clean Code",
                    Price = 50,
                    Author = "Robert C. Martin"
                });

                repo.AddProduct(new BookProduct
                {
                    Id = 5,
                    Name = "Design Patterns",
                    Price = 70,
                    Author = "GoF"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nTotal Inventory Value (Before Discount):");
            Console.WriteLine(repo.CalculateTotalValue());

            Console.WriteLine("\nFind Electronics by Brand = Dell:");
            var dellProducts = repo.FindProducts(p =>
                p is ElectronicProduct ep && ep.Brand == "Dell");

            foreach (var item in dellProducts)
                Console.WriteLine(item.Name);


            var manager = new InventoryManager();
            manager.ProcessProducts(repo.GerAll());

            Console.WriteLine("\nBulk Price Increase by 5%:");
            manager.UpdatePrices(repo.GetAll(), p => p.Price * 1.05m);

            Console.WriteLine("\nTotal Inventory Value (After Increase):");
            Console.WriteLine(repo.CalculateTotalValue());
        }
    }
