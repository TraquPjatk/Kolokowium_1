using Kolokwium_1.Dtos;

namespace Services
{
    public interface IMedicamentService
    {
        public MedicamentDto GetMedicine(int id);
    }
}
