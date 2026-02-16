using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital_Patient_Management_System
{
    internal class Program
    {
        //  Patient Class
        public class Patient
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public int Age { get; private set; }
            public string Condition { get; private set; }

            // Medical history list
            public List<string> MedicalHistory { get; private set; }

            public Patient(int id, string name, int age, string condition)
            {
                Id = id;
                Name = name;
                Age = age;
                Condition = condition;
                MedicalHistory = new List<string>();
            }

            public void AddMedicalRecord(string record)
            {
                MedicalHistory.Add(record);
            }
        }

        //  Hospital Manager
        public class HospitalManager
        {
            private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
            private Queue<Patient> _appointmentQueue = new Queue<Patient>();

            // Register Patient
            public void RegisterPatient(int id, string name, int age, string condition)
            {
                if (!_patients.ContainsKey(id))
                {
                    Patient patient = new Patient(id, name, age, condition);
                    _patients.Add(id, patient);
                }
                else
                {
                    Console.WriteLine("Patient with this ID already exists.");
                }
            }

            // Schedule Appointment
            public void ScheduleAppointment(int patientId)
            {
                if (_patients.ContainsKey(patientId))
                {
                    _appointmentQueue.Enqueue(_patients[patientId]);
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
            }

            // Process Next Appointment
            public Patient ProcessNextAppointment()
            {
                if (_appointmentQueue.Count > 0)
                {
                    return _appointmentQueue.Dequeue();
                }

                return null;
            }

            // Find Patients by Condition (LINQ)
            public List<Patient> FindPatientsByCondition(string condition)
            {
                return _patients.Values
                                .Where(p => p.Condition.Equals(condition, StringComparison.OrdinalIgnoreCase))
                                .ToList();
            }
        }

        static void Main(string[] args)
        {
            HospitalManager manager = new HospitalManager();

            manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
            manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");

            manager.ScheduleAppointment(1);
            manager.ScheduleAppointment(2);

            var nextPatient = manager.ProcessNextAppointment();
            Console.WriteLine(nextPatient.Name); // John Doe

            var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
            Console.WriteLine(diabeticPatients.Count); // 1
        }
    }
}

