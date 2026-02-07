using System.Collections.Concurrent;

namespace ProducerConsumerOrderProcessing;

internal class Program
{
    static async Task Main(string[] args)
    {
        var queue = new BlockingCollection<Order>();
        const int consumerCount = 3;
        const int processDelayMs = 100;

        var producer = Task.Run(() =>
        {
            for (int i = 1; i <= 10; i++)
                queue.Add(new Order { Id = i });
            queue.CompleteAdding(); // Signal no more orders; allows graceful consumer shutdown
        });

        var totalProcessed = await RunConsumers(queue, consumerCount, processDelayMs);
        await producer;

        Console.WriteLine($"Total processed: {totalProcessed}");
    }

    /// <summary>Starts the given number of consumer tasks that process orders from the queue until it is completed; returns total processed count.</summary>
    static async Task<int> RunConsumers(BlockingCollection<Order> queue, int consumerCount, int processDelayMs)
    {
        var processed = new ConcurrentCounter();

        var consumers = Enumerable.Range(0, consumerCount).Select(_ => Task.Run(async () =>
        {
            foreach (var order in queue.GetConsumingEnumerable()) // Blocks until item available or adding completed
            {
                await Task.Delay(processDelayMs); // Simulate work
                processed.Increment();
            }
        })).ToArray();

        await Task.WhenAll(consumers);
        return processed.Value;
    }
}

/// <summary>Represents an order to be processed.</summary>
public class Order
{
    public int Id { get; set; }
}

/// <summary>Thread-safe counter for total processed count.</summary>
public class ConcurrentCounter
{
    private int _value;
    public int Value => _value;
    public void Increment() => Interlocked.Increment(ref _value);
}
