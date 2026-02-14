namespace Pharmacy_Medicine_Inventory_System
{
    public class MedicineUtility
    {
        private readonly SortedDictionary<int, List<Medicine>> _inventory = new();

        public void AddMedicine(Medicine medicine)
        {
            if (medicine.Price <= 0)
                throw new InvalidPriceException("Price must be greater than zero.");

            int currentYear = DateTime.Now.Year;
            if (medicine.ExpiryYear < currentYear)
                throw new InvalidExpiryYearException("Expiry year cannot be in the past.");

            if (ContainsMedicineById(medicine.Id))
                throw new DuplicateMedicineException($"Medicine with ID {medicine.Id} already exists.");

            if (!_inventory.ContainsKey(medicine.ExpiryYear))
                _inventory[medicine.ExpiryYear] = new List<Medicine>();

            _inventory[medicine.ExpiryYear].Add(medicine);
        }

        public List<Medicine> GetAllMedicines()
        {
            var result = new List<Medicine>();
            foreach (var list in _inventory.Values)
                result.AddRange(list);
            return result;
        }

        public void UpdateMedicinePrice(string id, int newPrice)
        {
            if (newPrice <= 0)
                throw new InvalidPriceException("Price must be greater than zero.");

            Medicine? medicine = FindMedicineById(id);
            if (medicine == null)
                throw new MedicineNotFoundException($"Medicine with ID {id} not found.");

            medicine.Price = newPrice;
        }

        private bool ContainsMedicineById(string id)
        {
            return FindMedicineById(id) != null;
        }

        private Medicine? FindMedicineById(string id)
        {
            foreach (var list in _inventory.Values)
            {
                var found = list.FirstOrDefault(m => m.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
