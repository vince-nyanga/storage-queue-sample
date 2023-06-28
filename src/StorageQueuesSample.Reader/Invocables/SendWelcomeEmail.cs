using Azure.Storage.Queues.Models;
using Coravel.Invocable;
using Serilog;
using StorageQueuesSample.Shared;
using StorageQueuesSample.Shared.Models;

namespace StorageQueuesSample.Reader.Invocables;

internal sealed class SendWelcomeEmail : IInvocable
{
    public async Task Invoke()
    {
        var queue = await QueueManager.Instance.GetQueueClientAsync();
        
        QueueMessage message = await queue.ReceiveMessageAsync();
        
        if (message == null)
        {
            return;
        }

        var user = message.Body.ToObjectFromJson<User>();
        
        Log.Information("Sending welcome email to {Name} with email {Email}", user.Name, user.Email);

        await queue.DeleteMessageAsync(message.MessageId, message.PopReceipt);
    }
}