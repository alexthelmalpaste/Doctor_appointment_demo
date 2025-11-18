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
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public Appointment Create(Appointment appointment) => _appointmentRepository.Create(appointment);
        public IEnumerable<Appointment> GetAll() => _appointmentRepository.GetAll();
        public Appointment? Get(int id) => _appointmentRepository.GetById(id);
        public bool Delete(int id) => _appointmentRepository.Delete(id);
        public Appointment Update(int id, Appointment appointment) => _appointmentRepository.Update(id, appointment);
    }
}
