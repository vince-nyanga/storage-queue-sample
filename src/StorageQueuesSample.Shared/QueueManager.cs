using Azure.Storage.Queues;
using StorageQueuesSample.Shared.Constants;

namespace StorageQueuesSample.Shared;

public class QueueManager
{
    private static QueueManager? _instance;

    private readonly QueueClient _queueClient;
    
    private QueueManager()
    {
        var queueOptions = new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        };
        
        _queueClient = new QueueClient(QueueConstants.ConnectionString, QueueConstants.QueueName, queueOptions);
    }
    
    public static QueueManager Instance => _instance ??= new QueueManager();

    public async ValueTask<QueueClient> GetQueueClientAsync()
    {
        await _queueClient.CreateIfNotExistsAsync();
        return _queueClient;
    }
}