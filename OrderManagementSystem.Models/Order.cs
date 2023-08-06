using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models;

public class Order : BaseEntity
{
    public string? CustomerName { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "money")]
    public decimal Rate { get; set; }
    [Column(TypeName = "money")]
    public decimal Price
    {
        get
        {
            return Rate * Quantity;
        }
        set { }
    }

    public bool IsCompleted { get; set; } = false;
    //public bool IsCompleted => Status == "Completed";

    [NotMapped]
    public string Status
    {
        get
        {
            string orderStatus;

            if (IsCompleted is true)
            {
                orderStatus = "Completed";
            }
            else
            {
                orderStatus = "Not paid";
            }

            return orderStatus;
        }
    }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public OrderType? Type { get; set; }
    public int TypeId { get; set; }

    public void SetPrice()
    {
        Price = Rate * Quantity;
    }

    public void Update(string customerName, int quantity, decimal rate)
    {
        CustomerName = customerName;
        Quantity = quantity;
        Rate = rate;

        Price = Rate * Quantity;
    }
}