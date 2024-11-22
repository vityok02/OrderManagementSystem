using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models;

public class Order : BaseEntity
{
    public Customer Customer { get; private set; } = default!;

    public int Amount { get; private set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; private set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; private set; }

    public bool IsCompleted { get; private set; } = false;

    public Status Status { get; private set; }

    public DateTime CreatedDate => DateTime.UtcNow;

    public DateTime CompletedDate { get; private set; }

    public WorkType? Type { get; private set; }

    public int TypeId { get; private set; }

    public Order(Customer customer, WorkType type, int amount, decimal rate)
    {
        Customer = customer;
        Type = type;
        Amount = amount;
        UnitPrice = rate;
    }

    public void Create()
    {
        Status = Status.NotPaid;
    }

    public void Cancel()
    {
        Status = Status.Cancelled;
    }

    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
    }

    public void CompletedPartially(decimal price)
    {
        TotalPrice -= price;
        Status = Status.CompletedPartially;
    }

    public void UpdatePrice(decimal price)
    {
        TotalPrice = price;
    }

    public void Update(Customer customer, WorkType type, int quantity, decimal rate)
    {
        Customer = customer;
        Type = type;
        Amount = quantity;
        UnitPrice = rate;
        TotalPrice = quantity * rate;
    }
}
