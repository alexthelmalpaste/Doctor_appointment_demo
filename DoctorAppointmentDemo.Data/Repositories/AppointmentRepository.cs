using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace MyDoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository()
        {
            var db = ReadFromAppSettings();
            Path = db.Database.Appointments.Path;
            LastId = db.Database.Appointments.LastId;
        }

        public override void ShowInfo(Appointment appointment)
        {
            Console.WriteLine($"[{appointment.Id}] DoctorId: {appointment.DoctorId}, PatientId: {appointment.PatientId}, Date: {appointment.Date}, Desc: {appointment.Description}");
        }

        protected override void SaveLastId()
        {
            var db = ReadFromAppSettings();
            db.Database.Appointments.LastId = LastId;
            File.WriteAllText(Constants.AppSettingsPath, JsonConvert.SerializeObject(db, Formatting.Indented));
        }
    }
}
