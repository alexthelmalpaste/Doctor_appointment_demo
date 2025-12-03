using MyDoctorAppointment.Domain.Entities;
Console.WriteLine($"{a.Id}: Doctor {a.DoctorId}, Patient {a.PatientId}, From {a.DateTimeFrom}, To {a.DateTimeTo}, Desc: {a.Description}");
Console.WriteLine($"{p.Id}: {p.Name} {p.Surname}, Info: {p.AdditionalInfo}, Phone: {p.Phone}");
using System.Text.Json;
using System.Xml.Serialization;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public DoctorAppointment(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        // ... rest of the class ...
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Select data storage format:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();

            IDataProvider<Doctor> doctorProvider;
            IDataProvider<Patient> patientProvider;
            IDataProvider<Appointment> appointmentProvider;

            if (choice == "2")
            {
                doctorProvider = new XmlDataProvider<Doctor>("doctors.xml");
                patientProvider = new XmlDataProvider<Patient>("patients.xml");
                appointmentProvider = new XmlDataProvider<Appointment>("appointments.xml");
            }
            else
            {
                doctorProvider = new JsonDataProvider<Doctor>("doctors.json");
                patientProvider = new JsonDataProvider<Patient>("patients.json");
                appointmentProvider = new JsonDataProvider<Appointment>("appointments.json");
            }

            var doctorService = new DoctorService(doctorProvider);
            var patientService = new PatientService(patientProvider);
            var appointmentService = new AppointmentService(appointmentProvider);

            var doctorAppointment = new DoctorAppointment(doctorService, patientService, appointmentService);
            doctorAppointment.Menu();
        }
    }
}
public enum MenuOptions
{
    Exit = 0,
    Doctors = 1,
    Patients = 2,
    Appointments = 3
}

public class DoctorAppointment
{
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;

    public DoctorAppointment()
    {
        _doctorService = new DoctorService();
        _patientService = new PatientService();
        _appointmentService = new AppointmentService();
    }

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Doctor Appointment Menu ===");
            Console.WriteLine("1. Doctors");
            Console.WriteLine("2. Patients");
            Console.WriteLine("3. Appointments");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
            var option = (MenuOptions)choice;

            switch (option)
            {
                case MenuOptions.Exit: return;
                case MenuOptions.Doctors: ShowDoctors(); break;
                case MenuOptions.Patients: ShowPatients(); break;
                case MenuOptions.Appointments: ShowAppointments(); break;
            }
        }
    }

    private void ShowDoctors()
    {
        Console.WriteLine("=== Doctors ===");
        foreach (var doc in _doctorService.GetAll())
            Console.WriteLine($"{doc.Id}: {doc.Name} {doc.Surname}, Exp: {doc.Experience}, Type: {doc.DoctorType}");
    }

    private void ShowPatients()
    {
        Console.WriteLine("=== Patients ===");
        foreach (var p in _patientService.GetAll())
            Console.WriteLine($"{p.Id}: {p.Name} {p.Surname}, Age: {p.Age}, Phone: {p.Phone}");
    }

    private void ShowAppointments()
    {
        Console.WriteLine("=== Appointments ===");
        foreach (var a in _appointmentService.GetAll())
            Console.WriteLine($"{a.Id}: Doctor {a.DoctorId}, Patient {a.PatientId}, Date {a.Date}, Desc: {a.Description}");
    }
}

public class JsonDataProvider<T> : IDataProvider<T>
{
    private readonly string _filePath;
    public JsonDataProvider(string filePath) => _filePath = filePath;

    public IEnumerable<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        using System.Text.Json; // Add this at the top of the file
    }

    public void Save(IEnumerable<T> items)
    {
        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(_filePath, json);
    }
}

public class XmlDataProvider<T> : IDataProvider<T>
{
    private readonly string _filePath;
    public XmlDataProvider(string filePath) => _filePath = filePath;

    public IEnumerable<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        using var stream = File.OpenRead(_filePath);
        var serializer = new XmlSerializer(typeof(List<T>));
        return (List<T>?)serializer.Deserialize(stream) ?? new List<T>();
    }

    public void Save(IEnumerable<T> items)
    {
        using var stream = File.Create(_filePath);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(stream, items.ToList());
    }
}
public class DoctorService : IDoctorService
{
    private readonly IDataProvider<Doctor> _provider;
    private List<Doctor> _doctors;

    public DoctorService(IDataProvider<Doctor> provider)
    {
        _provider = provider;
        _doctors = _provider.Load().ToList();
    }

    public IEnumerable<Doctor> GetAll() => _doctors;

    public Doctor Create(Doctor doctor)
    {
        doctor.Id = _doctors.Count > 0 ? _doctors.Max(d => d.Id) + 1 : 1;
        _doctors.Add(doctor);
        _provider.Save(_doctors);
        return doctor;
    }

    // Implement other methods similarly...
}
public class PatientService : IPatientService
{
    private readonly IDataProvider<Patient> _provider;
    private List<Patient> _patients;

    public PatientService(IDataProvider<Patient> provider)
    {
        _provider = provider;
        _patients = _provider.Load().ToList();
    }

    // Implement interface methods...
}

public class AppointmentService : IAppointmentService
{
    private readonly IDataProvider<Appointment> _provider;
    private List<Appointment> _appointments;

    public AppointmentService(IDataProvider<Appointment> provider)
    {
        _provider = provider;
        _appointments = _provider.Load().ToList();
    }

    // Implement interface methods...
}
public class Appointment : Auditable
{
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public DateTime DateTimeFrom { get; set; }
    public DateTime DateTimeTo { get; set; }
    public string? Description { get; set; }
}
public interface IDataProvider<T>
{
    IEnumerable<T> Load();
    void Save(IEnumerable<T> items);
}
