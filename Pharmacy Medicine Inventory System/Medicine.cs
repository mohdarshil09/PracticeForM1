namespace Pharmacy_Medicine_Inventory_System
{
    public class Medicine
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int ExpiryYear { get; set; }

        public override string ToString() => $"Details: {Id} {Name} {Price} {ExpiryYear}";
    }
}
