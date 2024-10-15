using System.Threading.Tasks;
using Library.Components.StateMachines;

namespace Library.Components.Services;

public interface IWmsService
{
    public Task SubmitOrder(Order order);
    public Task CancelOrder(Order order);
    public Task UpdateOrder(Order order);
}

public interface IEmailService
{
    public Task SendOrderConfirmation(Order order);
    public Task SendOrderCancellation(Order order);
    public Task SendOrderMilestone(Order order, string milestone);
}