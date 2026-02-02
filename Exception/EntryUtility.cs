using System;
using System.Collections.Generic;
using System.Text;



namespace M1Exception
{
    public class EntryUtility
    {
        public bool ValidateEmployeeId(string employeeId)
        {
            if (employeeId.Length != 10 ||
                !employeeId.StartsWith("GOAIR/") ||
                !int.TryParse(employeeId.Substring(6), out _))
            {
                throw new InvalidEntryException("Invalid entry details");
            }
            return true;
        }

        public bool ValidateDuration(int duration)
        {
            if (duration < 1 || duration > 5)
            {
                throw new InvalidEntryException("Invalid entry details");
            }
            return true;
        }
    }
}
