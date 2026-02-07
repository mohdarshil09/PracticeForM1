namespace TransactionalMoneyTransfer;

internal class Program
{
    static void Main(string[] args)
    {
        var service = new TransferService();
        service.CreateAccount("A", 100m);
        service.CreateAccount("B", 50m);

        var r1 = service.Transfer("A", "B", 30m);
        Console.WriteLine($"Transfer: Success={r1.Success}, Message={r1.Message}");

        try { service.Transfer("A", "B", -10m); }
        catch (InvalidAmountException ex) { Console.WriteLine($"Invalid: {ex.Message}"); }

        foreach (var e in service.GetAuditLog())
            Console.WriteLine($"Audit: {e}");
    }
}

/// <summary>Result of a transfer attempt (success or failure message).</summary>
public class TransferResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
}

/// <summary>Thrown when an account is invalid (null, same as counterpart, or not found).</summary>
public class InvalidAccountException : Exception
{
    public InvalidAccountException(string message) : base(message) { }
}

/// <summary>Thrown when the source account has insufficient balance.</summary>
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
}

/// <summary>Thrown when the transfer amount is invalid (e.g. not positive).</summary>
public class InvalidAmountException : Exception
{
    public InvalidAmountException(string message) : base(message) { }
}

/// <summary>Service for atomic money transfers with rollback on credit failure and audit logging.</summary>
public class TransferService
{
    private readonly Dictionary<string, decimal> _balances = new();
    private readonly List<string> _auditLog = new();
    private readonly object _lock = new();

    public void CreateAccount(string accountId, decimal initialBalance)
    {
        lock (_lock) _balances[accountId] = initialBalance;
    }

    /// <summary>Transfers amount from fromAcc to toAcc. Debit and credit are atomic; debit is rolled back if credit fails. Throws domain exceptions for invalid inputs.</summary>
    public TransferResult Transfer(string fromAcc, string toAcc, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(fromAcc) || string.IsNullOrWhiteSpace(toAcc))
            throw new InvalidAccountException("Account id cannot be null or empty.");
        if (fromAcc == toAcc)
            throw new InvalidAccountException("From and to account cannot be the same.");
        if (amount <= 0)
            throw new InvalidAmountException("Amount must be positive.");

        lock (_lock)
        {
            if (!_balances.ContainsKey(fromAcc))
                throw new InvalidAccountException($"Account not found: {fromAcc}.");
            if (!_balances.ContainsKey(toAcc))
                throw new InvalidAccountException($"Account not found: {toAcc}.");
            if (_balances[fromAcc] < amount)
                throw new InsufficientFundsException($"Insufficient funds in {fromAcc}.");

            _balances[fromAcc] -= amount;
            try
            {
                _balances[toAcc] += amount;
            }
            catch
            {
                _balances[fromAcc] += amount; // Rollback debit
                RecordAudit(fromAcc, toAcc, amount, false);
                return new TransferResult { Success = false, Message = "Credit failed; debit rolled back." };
            }

            RecordAudit(fromAcc, toAcc, amount, true);
            return new TransferResult { Success = true, Message = "Transfer completed." };
        }
    }

    /// <summary>Appends one audit log entry for the transfer attempt.</summary>
    private void RecordAudit(string from, string to, decimal amount, bool success)
    {
        _auditLog.Add($"{DateTime.UtcNow:O} | {from} -> {to} | {amount} | {(success ? "Success" : "Failure")}");
    }

    public IReadOnlyList<string> GetAuditLog()
    {
        lock (_lock) return _auditLog.ToList();
    }
}
