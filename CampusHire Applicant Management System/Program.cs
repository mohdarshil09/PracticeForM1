namespace CampusHire_Applicant_Management_System;
internal class Program
{
    private static readonly ApplicantManager Manager = new();

    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            var choice = ReadChoice();
            Console.WriteLine();

            switch (choice)
            {
                case 1: AddApplicant(); break;
                case 2: DisplayAll(); break;
                case 3: SearchApplicant(); break;
                case 4: UpdateApplicant(); break;
                case 5: DeleteApplicant(); break;
                case 6:
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("=== CampusHire Applicant Management System ===");
        Console.WriteLine("1. Add Applicant");
        Console.WriteLine("2. Display All Applicants");
        Console.WriteLine("3. Search by Applicant ID");
        Console.WriteLine("4. Update Applicant");
        Console.WriteLine("5. Delete Applicant");
        Console.WriteLine("6. Exit");
        Console.Write("Enter choice (1-6): ");
    }
    static int ReadChoice()
    {
        return int.TryParse(Console.ReadLine(), out var n) ? n : -1;
    }

    static void AddApplicant()
    {
        try
        {
            var applicant = ReadApplicantData(null);
            Manager.Add(applicant);
            Console.WriteLine("Applicant added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    static void DisplayAll()
    {
        var list = Manager.GetAll();
        if (list.Count == 0)
        {
            Console.WriteLine("No applicants found.");
            return;
        }
        Console.WriteLine($"Total Applicants: {list.Count}");
        Console.WriteLine(new string('-', 80));
        foreach (var a in list)
            Console.WriteLine(a);
    }

    static void SearchApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        var id = Console.ReadLine()?.Trim() ?? "";
        if (!Applicant.ValidateApplicantId(id, out var err))
        {
            Console.WriteLine(err);
            return;
        }

        var applicant = Manager.GetById(id);
        if (applicant == null)
        {
            Console.WriteLine("Applicant not found.");
            return;
        }
        Console.WriteLine("Applicant Details:");
        Console.WriteLine($"  ID: {applicant.ApplicantId}");
        Console.WriteLine($"  Name: {applicant.ApplicantName}");
        Console.WriteLine($"  Current Location: {applicant.CurrentLocation}");
        Console.WriteLine($"  Preferred Job Location: {applicant.PreferredJobLocation}");
        Console.WriteLine($"  Core Competency: {applicant.CoreCompetency}");
        Console.WriteLine($"  Passing Year: {applicant.PassingYear}");
    }


    static void UpdateApplicant()
    {
        Console.Write("Enter Applicant ID to update: ");
        var id = Console.ReadLine()?.Trim() ?? "";
        if (!Applicant.ValidateApplicantId(id, out var err))
        {
            Console.WriteLine(err);
            return;
        }

        var existing = Manager.GetById(id);
        if (existing == null)
        {
            Console.WriteLine("Applicant not found.");
            return;
        }

        try
        {
            var updated = ReadApplicantData(id); // Pass ID so we keep same ID when updating
            Manager.Update(id, updated);
            Console.WriteLine("Applicant updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void DeleteApplicant()
    {
        Console.Write("Enter Applicant ID to delete: ");
        var id = Console.ReadLine()?.Trim() ?? "";
        if (!Applicant.ValidateApplicantId(id, out var err))
        {
            Console.WriteLine(err);
            return;
        }

        try
        {
            Manager.Delete(id);
            Console.WriteLine("Applicant deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static Applicant ReadApplicantData(string? existingId)
    {
        var applicant = new Applicant();

        // Applicant ID
        if (!string.IsNullOrEmpty(existingId))
        {
            applicant.ApplicantId = existingId;
        }
        else
        {
            while (true)
            {
                Console.Write("Applicant ID (e.g. CH123456): ");
                var id = Console.ReadLine()?.Trim() ?? "";
                if (Applicant.ValidateApplicantId(id, out var err))
                {
                    applicant.ApplicantId = id;
                    break;
                }
                Console.WriteLine(err);
            }
        }

        // Applicant Name
        while (true)
        {
            Console.Write("Applicant Name: ");
            var name = Console.ReadLine()?.Trim() ?? "";
            if (Applicant.ValidateApplicantName(name, out var err))
            {
                applicant.ApplicantName = name;
                break;
            }
            Console.WriteLine(err);
        }

        // Current Location
        applicant.CurrentLocation = SelectFromList("Current Location", Applicant.ValidCurrentLocations);

        // Preferred Job Location
        applicant.PreferredJobLocation = SelectFromList("Preferred Job Location", Applicant.ValidJobLocations);

        // Core Competency
        applicant.CoreCompetency = SelectFromList("Core Competency", Applicant.ValidCompetencies);

        // Passing Year
        while (true)
        {
            Console.Write("Passing Year: ");
            var input = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Passing Year is mandatory.");
                continue;
            }
            if (!int.TryParse(input, out var year))
            {
                Console.WriteLine("Invalid year. Enter a valid number.");
                continue;
            }
            if (Applicant.ValidatePassingYear(year, out var err))
            {
                applicant.PassingYear = year;
                break;
            }
            Console.WriteLine(err);
        }

        return applicant;
    }

    static string SelectFromList(string prompt, string[] options)
    {
        while (true)
        {
            Console.WriteLine($"{prompt} (select number):");
            for (var i = 0; i < options.Length; i++)
                Console.WriteLine($"  {i + 1}. {options[i]}");
            Console.Write("Choice: ");
            var input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out var idx) && idx >= 1 && idx <= options.Length)
                return options[idx - 1];
            Console.WriteLine("Invalid choice. Try again.");
        }
    }
}
