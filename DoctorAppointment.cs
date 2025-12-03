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

    // ... rest of the class
}
