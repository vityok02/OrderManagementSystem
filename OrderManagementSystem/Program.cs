using OrderManagementSystem;
using static OrderManagementSystem.ConsoleHelper;

public class Program
{
    static void Main(string[] args)
    {
        while(true)
        {
            SelectAction();
        }

        void SelectAction()
        {
            ShowActions();

            var actionId = GetActionChoice();

            switch(actionId)
            {
                case Actions.ShowOrders:
                    ShowOrders();
                    break;
                case Actions.AddOrder:
                    AddOrder();
                    break;
                case Actions.UpdateOrder:
                    UpdateOrder();
                    break;
                case Actions.DeleteOrder:
                    RemoveOrder();
                    break;
            }
        }
    }
}
