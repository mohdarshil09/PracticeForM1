using System.Collections.Concurrent;

namespace RateLimiter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var limiter = new SlidingWindowRateLimiter(maxRequests: 5, windowSeconds: 10);

            var now = DateTime.UtcNow;
            Console.WriteLine(limiter.AllowRequest("client1", now));      // true (1–5)
            Console.WriteLine(limiter.AllowRequest("client1", now));      // true
            Console.WriteLine(limiter.AllowRequest("client1", now));      // true
            Console.WriteLine(limiter.AllowRequest("client1", now));      // true
            Console.WriteLine(limiter.AllowRequest("client1", now));      // true
            Console.WriteLine(limiter.AllowRequest("client1", now));      // false (6th)
            Console.WriteLine(limiter.AllowRequest("client2", now));      // true (other client)
        }
    }

    /// <summary>
    /// Rate limiter using a sliding window: allows at most maxRequests per client
    /// within the last windowSeconds. Each client is tracked independently.
    /// </summary>
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

        /// <summary>
        /// Returns true if the request is allowed (under the limit); false if the client has exceeded the limit in the current window.
        /// </summary>
        /// <param name="clientId">Identifier for the client.</param>
        /// <param name="now">Current time (used as the request time and window end).</param>
        public bool AllowRequest(string clientId, DateTime now)
        {
            var windowStart = now.AddSeconds(-_windowSeconds);
            var queue = _clientRequests.GetOrAdd(clientId, _ => new Queue<DateTime>());

            lock (queue)
            {
                // Drop timestamps outside the sliding window
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
