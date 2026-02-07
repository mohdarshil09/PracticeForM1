using System.Collections.Concurrent;

namespace RateLimiter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var limiter = new SlidingWindowRateLimiter(maxRequests: 5, windowSeconds: 10);

            var now = DateTime.UtcNow;
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client1", now));      
            Console.WriteLine(limiter.AllowRequest("client2", now));      
        }
    }

    public class SlidingWindowRateLimiter
    {
        private readonly int _maxRequests;
        private readonly int _windowSeconds;
        private readonly ConcurrentDictionary<string, Queue<DateTime>> _clientRequests = new();

        public SlidingWindowRateLimiter(int maxRequests = 5, int windowSeconds = 10)
        {
            _maxRequests = maxRequests;
            _windowSeconds = windowSeconds;
        }

        public bool AllowRequest(string clientId, DateTime now)
        {
            var windowStart = now.AddSeconds(-_windowSeconds);
            var queue = _clientRequests.GetOrAdd(clientId, _ => new Queue<DateTime>());

            lock (queue)
            {
                while (queue.Count > 0 && queue.Peek() < windowStart)
                    queue.Dequeue();

                if (queue.Count >= _maxRequests)
                    return false;

                queue.Enqueue(now);
                return true;
            }
        }
    }
}
