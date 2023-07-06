using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;

namespace OrderManagementSystem;

public static class ConsoleHelper
{
    private static readonly AppDbContext _dbContext = new();

    private static string CustomerName { get; set; } = null!;
    private static int Quantity { get; set; }
    private static decimal Rate { get; set; }

    public static void ShowActions()
    {
        Dictionary<Actions, string> actions = new()
        {
            {Actions.ShowOrders, "Show orders" },
            {Actions.AddOrder, "AddOrder"},
            {Actions.UpdateOrder, "UpdateOrder" },
            {Actions.DeleteOrder, "DeleteOrder"},
        };

        foreach(var action in actions)
        {
            Console.WriteLine($"{(int)action.Key}. {action.Value}");
        }
    }

    public static Actions GetActionChoice()
    {
        while(true)
        {
            if(Enum.TryParse<Actions>(Console.ReadLine(), out Actions result))
            {
                return result;
            }
            else
            {
                Console.WriteLine();
            }
        }
    }

    public static void ShowOrders()
    {
        Console.WriteLine(":::Orders:::");
        var orders = _dbContext.Orders.ToArray();
        foreach (var ord in orders)
        {
            Console.WriteLine($"{ord.Id}. {ord.CustomerName}");
            Console.WriteLine($"Quantity: {ord.Quantity}");
            Console.WriteLine($"Rate: {ord.Rate}");
            Console.WriteLine($"Price: {ord.Price}");
            Console.WriteLine($"Date: {ord.CreatedDate}");
            Console.WriteLine("====================");
        }
    }

    public static void AddOrder() 
    {
        GetDataForOrder();

        var order = new Order()
        {
            CustomerName = CustomerName,
            Rate = Rate,
            Quantity = Quantity,
            CreatedDate = DateTime.Now,
        };
        order.SetPrice();

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public static void UpdateOrder()
    {
        var order = GetOrder();

        GetDataForOrder();

        order.Update(CustomerName, Quantity, Rate);
        _dbContext.Update(order);
        _dbContext.SaveChanges();
    }

    public static void RemoveOrder()
    {
        Console.WriteLine("Enter the order id");

        var order = GetOrder();

        if(order is null)
        {
            PrintErrorMessage("No such order exists");
            return;
        }

        _dbContext.Orders.Remove(order);
        _dbContext.SaveChanges();

        PrintSuccessDeleteMessage($"The order by {order.CustomerName} has been deleted");
    }

    private static Order GetOrder()
    {
        var id = GetId();

        return _dbContext.Orders.Find(id)!;
    }

    private static int GetId()
    {
        int id = GetIntValue();
        return id;
    }

    private static string GetValue()
    {
        var value = Console.ReadLine();

        while(true)
        {
            if(value.IsNullOrEmpty())
            {
                PrintErrorMessage("Incorrect data");
                continue;
            }
            else
            {
                return value!;
            }
        }
    }

    private static int GetIntValue()
    {
        var value = GetValue();
        
        while(true)
        {
            if (int.TryParse(value, out int number))
            {
                return number;
            }
            else
            {
                PrintErrorMessage("Incorrect data");
                continue;
            }
        }
    }

    private static decimal GetDecimalValue()
    {
        var value = GetValue();

        while (true)
        {
            if (decimal.TryParse(value, out decimal number))
            {
                return number;
            }
            else
            {
                PrintErrorMessage("Incorrect data");
                continue;
            }
        }
    }

    private static void GetDataForOrder()
    {
        Console.Write("Enter the customer name: ");
        CustomerName = GetValue();

        Console.Write("Enter the quantity: ");
        Quantity = GetIntValue();

        Console.Write("Enter the rate: ");
        Rate = GetDecimalValue();
    }

    private static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static void PrintSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static void PrintSuccessDeleteMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
