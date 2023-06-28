using System.Text.Json;
using Coravel.Invocable;
using Serilog;
using StorageQueuesSample.Shared;
using StorageQueuesSample.Shared.Models;
using Tynamix.ObjectFiller;

namespace StorageQueuesSample.Writer.Invocables;

internal sealed class RegisterUser : IInvocable
{
    public async Task Invoke()
    {
        var queue = await QueueManager.Instance.GetQueueClientAsync();

        var user = new User(CreateRandomName(), CreateRandomEmail());
        var message = JsonSerializer.Serialize(user);
        await queue.SendMessageAsync(message);
        
        Log.Information("User {Name} with email {Email} registered", user.Name, user.Email);
    }
    
    static string CreateRandomName() =>
        new RealNames(NameStyle.FirstNameLastName).GetValue();

    static string CreateRandomEmail() =>
        new EmailAddresses().GetValue();
}