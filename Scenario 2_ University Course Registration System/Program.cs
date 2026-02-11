using System;
using System.Collections.Generic;
using System.Linq;

// Base constraints
public interface IStudent
{
    int StudentId { get; }
    string Name { get; }
    int Semester { get; }
}

public interface ICourse
{
    string CourseCode { get; }
    string Title { get; }
    int MaxCapacity { get; }
    int Credits { get; }
}

// 1. Generic enrollment system
public class EnrollmentSystem<TStudent, TCourse>
    where TStudent : IStudent
    where TCourse : ICourse
{
    private Dictionary<TCourse, List<TStudent>> _enrollments = new();

    // TODO: Enroll student with constraints
    public bool EnrollStudent(TStudent student, TCourse course)
    {
        // Rules:
        // - Course not at capacity
        // - Student not already enrolled
        // - Student semester >= course prerequisite (if any)
        // - Return success/failure with reason

        if (student == null || course == null)
        {
            Console.WriteLine("Invalid student or course.");
            return false;
        }

        if (!_enrollments.ContainsKey(course))
            _enrollments[course] = new List<TStudent>();

        var students = _enrollments[course];

        if (students.Count >= course.MaxCapacity)
        {
            Console.WriteLine($"Enrollment failed: {course.Title} is at full capacity.");
            return false;
        }

        if (students.Any(s => s.StudentId == student.StudentId))
        {
            Console.WriteLine($"Enrollment failed: {student.Name} already enrolled.");
            return false;
        }

        if (course is LabCourse lab && student.Semester < lab.RequiredSemester)
        {
            Console.WriteLine($"Enrollment failed: {student.Name} does not meet prerequisite semester.");
            return false;
        }

        students.Add(student);
        Console.WriteLine($"{student.Name} enrolled in {course.Title}");
        return true;
    }

    // TODO: Get students for course
    public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
    {
        // Return immutable list
        if (!_enrollments.ContainsKey(course))
            return Array.Empty<TStudent>();

        return _enrollments[course].AsReadOnly();
    }

    // TODO: Get courses for student
    public IEnumerable<TCourse> GetStudentCourses(TStudent student)
    {
        // Return courses student is enrolled in
        return _enrollments
            .Where(e => e.Value.Any(s => s.StudentId == student.StudentId))
            .Select(e => e.Key);
    }

    // TODO: Calculate student workload
    public int CalculateStudentWorkload(TStudent student)
    {
        // Sum credits of all enrolled courses
        return GetStudentCourses(student).Sum(c => c.Credits);
    }
}

// 2. Specialized implementations
public class EngineeringStudent : IStudent
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Semester { get; set; }
    public string Specialization { get; set; }
}

public class LabCourse : ICourse
{
    public string CourseCode { get; set; }
    public string Title { get; set; }
    public int MaxCapacity { get; set; }
    public int Credits { get; set; }
    public string LabEquipment { get; set; }
    public int RequiredSemester { get; set; } // Prerequisite
}

// 3. Generic gradebook
public class GradeBook<TStudent, TCourse>
{
    private Dictionary<(TStudent, TCourse), double> _grades = new();

    // TODO: Add grade with validation
    public void AddGrade(TStudent student, TCourse course, double grade)
    {
        // Grade must be between 0 and 100
        // Student must be enrolled in course
        if (grade < 0 || grade > 100)
            throw new ArgumentException("Grade must be between 0 and 100.");

        _grades[(student, course)] = grade;
    }

    // TODO: Calculate GPA for student
    public double? CalculateGPA(TStudent student)
    {
        // Weighted by course credits
        // Return null if no grades
        var studentGrades = _grades
            .Where(g => EqualityComparer<TStudent>.Default.Equals(g.Key.Item1, student))
            .ToList();

        if (!studentGrades.Any())
            return null;

        double totalPoints = 0;
        int totalCredits = 0;

        foreach (var entry in studentGrades)
        {
            var course = entry.Key.Item2 as ICourse;
            totalPoints += entry.Value * course.Credits;
            totalCredits += course.Credits;
        }

        return totalPoints / totalCredits;
    }

    // TODO: Find top student in course
    public (TStudent student, double grade)? GetTopStudent(TCourse course)
    {
        // Return student with highest grade
        var top = _grades
            .Where(g => EqualityComparer<TCourse>.Default.Equals(g.Key.Item2, course))
            .OrderByDescending(g => g.Value)
            .FirstOrDefault();

        if (top.Equals(default(KeyValuePair<(TStudent, TCourse), double>)))
            return null;

        return (top.Key.Item1, top.Value);
    }
}

// 4. TEST SCENARIO: Create a simulation
// a) Create 3 EngineeringStudent instances
// b) Create 2 LabCourse instances with prerequisites
// c) Demonstrate:
//    - Successful enrollment
//    - Failed enrollment (capacity, prerequisite)
//    - Grade assignment
//    - GPA calculation
//    - Top student per course
class Program
{
    static void Main()
    {
        var enrollmentSystem = new EnrollmentSystem<EngineeringStudent, LabCourse>();
        var gradeBook = new GradeBook<EngineeringStudent, LabCourse>();

        var s1 = new EngineeringStudent { StudentId = 1, Name = "Arjun", Semester = 5 };
        var s2 = new EngineeringStudent { StudentId = 2, Name = "Neha", Semester = 3 };
        var s3 = new EngineeringStudent { StudentId = 3, Name = "Ravi", Semester = 6 };

        var c1 = new LabCourse
        {
            CourseCode = "CS501",
            Title = "Advanced Systems Lab",
            Credits = 4,
            MaxCapacity = 2,
            RequiredSemester = 5
        };

        var c2 = new LabCourse
        {
            CourseCode = "CS301",
            Title = "Basic Electronics Lab",
            Credits = 3,
            MaxCapacity = 2,
            RequiredSemester = 3
        };

        enrollmentSystem.EnrollStudent(s1, c1);
        enrollmentSystem.EnrollStudent(s3, c1);
        enrollmentSystem.EnrollStudent(s2, c1); // prerequisite fail

        enrollmentSystem.EnrollStudent(s2, c2);
        enrollmentSystem.EnrollStudent(s1, c2);
        enrollmentSystem.EnrollStudent(s3, c2); // capacity fail

        gradeBook.AddGrade(s1, c1, 88);
        gradeBook.AddGrade(s3, c1, 92);
        gradeBook.AddGrade(s2, c2, 81);
        gradeBook.AddGrade(s1, c2, 90);

        Console.WriteLine($"\nGPA of {s1.Name}: {gradeBook.CalculateGPA(s1)}");

        var topStudent = gradeBook.GetTopStudent(c1);
        Console.WriteLine($"Top student in {c1.Title}: {topStudent?.student.Name} ({topStudent?.grade})");
    }
}
