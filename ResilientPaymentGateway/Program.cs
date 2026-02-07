namespace ResilientPaymentGateway;

internal class Program
{
    static async Task Main(string[] args)
    {
        var gateway = new PaymentGateway();
        var request = new PaymentRequest { Amount = 100m, Currency = "USD" };

        var result = await gateway.ProcessPaymentAsync(request, CancellationToken.None);
        Console.WriteLine($"Success: {result.Success}, Message: {result.Message}");
    }
}

/// <summary>Input for a payment operation.</summary>
public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "";
}

/// <summary>Outcome of a payment operation.</summary>
public class PaymentResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
}

/// <summary>
/// Payment gateway with retry (3 attempts on timeout) and circuit breaker
/// (5 failures in 1 minute opens circuit for 30 seconds; then fail fast).
/// </summary>
public class PaymentGateway
{
    private const int MaxRetries = 3;
    private const int FailureWindowSeconds = 60;
    private const int CircuitOpenSeconds = 30;
    private const int MaxFailuresBeforeOpen = 5;

    private readonly List<DateTime> _failureTimes = new();
    private DateTime? _circuitOpenUntil;
    private readonly object _lock = new();

    /// <summary>
    /// Processes a payment with retry and circuit breaker. Supports cancellation via <paramref name="cancellationToken"/>.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The payment result (success or failure message).</returns>
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken)
    {
        if (IsCircuitOpen())
            return new PaymentResult { Success = false, Message = "Circuit open (fail fast)." };

        int attempt = 0;
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            attempt++;

            try
            {
                var result = await CallPaymentProviderAsync(request, cancellationToken);
                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (TimeoutException) when (attempt < MaxRetries)
            {
                // Transient failure: retry
                continue;
            }
            catch (TimeoutException)
            {
                RecordFailure();
                return new PaymentResult { Success = false, Message = "Timeout after retries." };
            }
        }
    }

    /// <summary>Returns true if the circuit is open (fail fast); otherwise false. Prunes old failures and resets when open period ends.</summary>
    private bool IsCircuitOpen()
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            _failureTimes.RemoveAll(t => (now - t).TotalSeconds > FailureWindowSeconds);
            if (_circuitOpenUntil.HasValue)
            {
                if (now < _circuitOpenUntil.Value) return true;
                _circuitOpenUntil = null;
                _failureTimes.Clear();
            }
            return false;
        }
    }

    /// <summary>Records a failure and opens the circuit when failures in the last minute reach the threshold.</summary>
    private void RecordFailure()
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            _failureTimes.RemoveAll(t => (now - t).TotalSeconds > FailureWindowSeconds);
            _failureTimes.Add(now);
            if (_failureTimes.Count >= MaxFailuresBeforeOpen && !_circuitOpenUntil.HasValue)
                _circuitOpenUntil = now.AddSeconds(CircuitOpenSeconds);
        }
    }

    /// <summary>Stub that calls the actual payment provider. Replace with real API call.</summary>
    private async Task<PaymentResult> CallPaymentProviderAsync(PaymentRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        throw new TimeoutException("Simulated timeout.");
    }
}
