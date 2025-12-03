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

    public Doctor? Get(int id) => _doctors.FirstOrDefault(d => d.Id == id);

    public bool Delete(int id)
    {
        var doctor = Get(id);
        if (doctor == null) return false;
        _doctors.Remove(doctor);
        _provider.Save(_doctors);
        return true;
    }

    public Doctor Update(int id, Doctor doctor)
    {
        var existing = Get(id);
        if (existing == null) return null;
        existing.Name = doctor.Name;
        existing.Surname = doctor.Surname;
        existing.Experience = doctor.Experience;
        existing.DoctorType = doctor.DoctorType;
        existing.Salary = doctor.Salary;
        _provider.Save(_doctors);
        return existing;
    }
}
