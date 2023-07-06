using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models;

public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName ="money")]
    public decimal Rate { get; set; }

    [Column(TypeName ="money")]
    public decimal Price { get; set; }
    public DateTime? CreatedDate { get; set; }

    public Order()
    {
    }

    public void SetPrice()
    {
        Price = Rate * Quantity;
    }

    public void Update(string customerName, int quantity, decimal rate)
    {
        if(customerName is not null)
        {
            CustomerName = customerName;
        }
        if(quantity is not 0)
        {
            Quantity = quantity;
        }
        if(rate is not 0)
        {
            Rate = rate;
        }

        Price = Rate * Quantity;
    }
}