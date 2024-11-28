using Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.WorkLogs;

public class WorkLog : BaseEntity
{
    public Customer Customer { get; private set; } = default!;

    public int CustomerId { get; private set; }

    public int Amount { get; private set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; private set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; private set; }

    public bool IsCompleted { get; private set; } = false;

    public Status Status { get; private set; }

    public DateTime CreatedDate => DateTime.UtcNow;

    public DateTime CompletedDate { get; private set; }

    public WorkType WorkType { get; private set; } = default!;

    public int TypeId { get; private set; }

    public WorkLog()
    { }

    public WorkLog(Customer customer, WorkType type, int amount, decimal rate)
    {
        Customer = customer;
        WorkType = type;
        Amount = amount;
        UnitPrice = rate;
    }

    public void SetStatus(Status status)
    {
        Status = status;
    }

    public void UpdatePrice(decimal price)
    {
        TotalPrice = price;
    }

    public void Update(Customer customer, WorkType workType, int amount, decimal unitPrice)
    {
        Customer = customer;
        WorkType = workType;
        Amount = amount;
        UnitPrice = unitPrice;
        TotalPrice = amount * unitPrice;
    }
}
