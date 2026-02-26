using System.Text.Json;

namespace CampusHire_Applicant_Management_System;

public class ApplicantManager
{
    private readonly List<Applicant> _applicants = [];
    private readonly string _filePath;

    public ApplicantManager()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "applicants.json");
        LoadFromFile();
    }

    public void Add(Applicant applicant)
    {
        if (GetById(applicant.ApplicantId) != null)
            throw new ArgumentException($"Applicant with ID {applicant.ApplicantId} already exists.");

        _applicants.Add(applicant);
        SaveToFile();
    }

    public List<Applicant> GetAll() => new(_applicants);

    public Applicant? GetById(string applicantId) =>
        _applicants.Find(a => a.ApplicantId.Equals(applicantId, StringComparison.OrdinalIgnoreCase));

    public void Update(string applicantId, Applicant updated)
    {
        var index = _applicants.FindIndex(a => a.ApplicantId.Equals(applicantId, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
            throw new ArgumentException($"Applicant with ID {applicantId} not found.");

        _applicants[index] = updated;
        SaveToFile();
    }

    public void Delete(string applicantId)
    {
        var removed = _applicants.RemoveAll(a => a.ApplicantId.Equals(applicantId, StringComparison.OrdinalIgnoreCase));
        if (removed == 0)
            throw new ArgumentException($"Applicant with ID {applicantId} not found.");

        SaveToFile();
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_filePath)) return;

        try
        {
            var json = File.ReadAllText(_filePath);
            var loaded = JsonSerializer.Deserialize<List<Applicant>>(json);
            if (loaded != null)
                _applicants.AddRange(loaded);
        }
        catch
        {
            // If file is corrupt or empty, start fresh
        }
    }

    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_applicants, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
