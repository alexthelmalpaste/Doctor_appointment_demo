using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using DoctorAppointmentDemo.Service.DataProviders; // Add this using

namespace MyDoctorAppointment
{
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
