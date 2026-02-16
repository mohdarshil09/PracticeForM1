namespace Inventory___Detect_Duplicate_Serials
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> serials = new List<string>
            {
                "S1","S2","S1","S3","S2","S2"
            };
            HashSet<string> uniqueSerials = new HashSet<string>();
            foreach (string serial in serials)
            {
                uniqueSerials.Add(serial);
            }
            foreach (string serial in uniqueSerials)
            {
                Console.WriteLine(string.Join(" ",serial));
            }
        }
    }
}
