using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            //while (true)
            //{
            //    // add Enum for menu items and describe menu
            //}

            Console.WriteLine("Current doctors list: ");
            var docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }

            Console.WriteLine("Adding doctor: ");

            var newDoctor = new Doctor
            {
                Name = "Vasya",
                Surname = "Petrov",
                Experience = 20,
                DoctorType = Domain.Enums.DoctorTypes.Dentist
            };

            _doctorService.Create(newDoctor);

            Console.WriteLine("Current doctors list: ");
            docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
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
