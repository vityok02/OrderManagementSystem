using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models;

public class Order : BaseEntity
{
    public Customer Customer { get; private set; } = default!;

    public int Quantity { get; private set; }

    [Column(TypeName = "money")]
    public decimal Rate { get; private set; }

    [Column(TypeName = "money")]
    public decimal Price => Quantity * Rate;

    public bool IsCompleted { get; private set; } = false;

    public Status Status { get; private set; }

    public DateTime? CreatedDate => DateTime.UtcNow;

    public DateTime? CompletedDate { get; private set; }

    public WorkType? Type { get; private set; }

    public int TypeId { get; private set; }

    public Order(Customer customer, WorkType type, int quantity, decimal rate)
    {
        Customer = customer;
        Type = type;
        Quantity = quantity;
        Rate = rate;
    }

    public void Update(Customer customer, WorkType type, int quantity, decimal rate)
    {
        Customer = customer;
        Type = type;
        Quantity = quantity;
        Rate = rate;
    }
}
