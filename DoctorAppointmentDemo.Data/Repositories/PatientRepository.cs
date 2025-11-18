using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace MyDoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public PatientRepository()
        {
            var db = ReadFromAppSettings();
            Path = db.Database.Patients.Path;
            LastId = db.Database.Patients.LastId;
        }

        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine($"[{patient.Id}] {patient.Name} {patient.Surname}, Age: {patient.Age}, Phone: {patient.Phone}");
        }

        protected override void SaveLastId()
        {
            var db = ReadFromAppSettings();
            db.Database.Patients.LastId = LastId;
            File.WriteAllText(Constants.AppSettingsPath, JsonConvert.SerializeObject(db, Formatting.Indented));
        }
    }
}
