using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService()
        {
            _patientRepository = new PatientRepository();
        }

        public Patient Create(Patient patient) => _patientRepository.Create(patient);
        public IEnumerable<Patient> GetAll() => _patientRepository.GetAll();
        public Patient? Get(int id) => _patientRepository.GetById(id);
        public bool Delete(int id) => _patientRepository.Delete(id);
        public Patient Update(int id, Patient patient) => _patientRepository.Update(id, patient);
    }
}
