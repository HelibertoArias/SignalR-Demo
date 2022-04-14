using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Services;

namespace SignalRDemo.Hubs;

// [Authorize]
public class OrdersHub : Hub
{
    private readonly OrderService orderService;

    public OrdersHub(OrderService orderService)
    {
        this.orderService = orderService;
    }

    public async Task GetUpdate(int orderNo)
    {
        CheckResult result;

        do
        {
            result = orderService.GetUpdate(orderNo);
            Thread.Sleep(2000);

            if (result.New)
            {
                // All, clients, groups, caller and so on.
                await Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
            }
        } while (!result.Finished);

        await Clients.Caller.SendAsync("Finished");
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;

        await Groups.AddToGroupAsync(connectionId, "Avangers");

        //await Clients.AllExcept(connectionId).SendAsync("clientMethodName", param1);

        // await Clients.Client(connectionId).SendAsync("clientMethodName", param1);

        //Context.User.Identity.Name
        Console.WriteLine("A client is connected");


    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("A client is disconnected " + exception?.Message);


    }
}
