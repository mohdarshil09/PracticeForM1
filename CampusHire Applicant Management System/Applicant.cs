namespace CampusHire_Applicant_Management_System;

public class Applicant
{
    public string ApplicantId { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string CurrentLocation { get; set; } = string.Empty;
    public string PreferredJobLocation { get; set; } = string.Empty;
    public string CoreCompetency { get; set; } = string.Empty;
    public int PassingYear { get; set; }

    public static string[] ValidCurrentLocations => ["Mumbai", "Pune", "Chennai"];
    public static string[] ValidJobLocations => ["Mumbai", "Pune", "Chennai", "Delhi", "Kolkata", "Bangalore"];
    public static string[] ValidCompetencies => [".NET", "JAVA", "ORACLE", "Testing"];

    public static bool ValidateApplicantId(string? id, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(id))
        {
            error = "Applicant ID is mandatory.";
            return false;
        }
        if (id.Length != 8)
        {
            error = "Applicant ID must be exactly 8 characters long.";
            return false;
        }
        if (!id.StartsWith("CH", StringComparison.OrdinalIgnoreCase))
        {
            error = "Applicant ID must start with 'CH'.";
            return false;
        }
        return true;
    }

    public static bool ValidateApplicantName(string? name, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Applicant Name is mandatory.";
            return false;
        }
        if (name.Length < 4 || name.Length > 15)
        {
            error = "Applicant Name must be between 4 and 15 characters.";
            return false;
        }
        return true;
    }

    public static bool ValidatePassingYear(int year, out string error)
    {
        error = string.Empty;
        if (year > DateTime.Now.Year)
        {
            error = $"Passing Year cannot be greater than {DateTime.Now.Year}.";
            return false;
        }
        return true;
    }

    public override string ToString() =>
        $"ID: {ApplicantId} | Name: {ApplicantName} | Current: {CurrentLocation} | Preferred: {PreferredJobLocation} | Competency: {CoreCompetency} | Year: {PassingYear}";
}
