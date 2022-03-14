using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface IRegistrationRepository
    {
        Task<List<Registration>> SelectAllRegistrations();
        Task<Registration> SelectRegistrationById(int registrationId);
        Task<Registration> InsertNewRegistration(Registration registration);
        Task<Registration> DeleteRegistrationById(int registrationId);
        Task<Registration> UpdateRegistrationById(int registrationId, Registration registration);
    }

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly MotorDBContext _dBContext;

        public RegistrationRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Registration> DeleteRegistrationById(int registrationId)
        {
            Registration registrationToDelete = await _dBContext
                .Registration
                .FirstOrDefaultAsync(x => x.Id == registrationId);

            if (registrationToDelete != null)
            {
                _dBContext.Remove(registrationToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return registrationToDelete;
        }

        public async Task<Registration> InsertNewRegistration(Registration registration)
        {
            await _dBContext.AddAsync(registration);
            await _dBContext.SaveChangesAsync();

            return registration;
        }

        public async Task<List<Registration>> SelectAllRegistrations()
        {
            return await _dBContext
                .Registration
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Fuel)
                .Include(x => x.Vehicle.Category)
                .Include(x => x.Customer)
                .Include(x => x.Customer.Address)
                .Include(x => x.Customer.Address.Zipcode)
                .ToListAsync();
        }

        public async Task<Registration> SelectRegistrationById(int registrationId)
        {
            return await _dBContext
                .Registration
                .Include(x => x.Vehicle)
                .Include(x => x.Vehicle.Fuel)
                .Include(x => x.Vehicle.Category)
                .Include(x => x.Customer)
                .Include(x => x.Customer.Address)
                .Include(x => x.Customer.Address.Zipcode)
                .FirstOrDefaultAsync(x => x.Id == registrationId);
        }

        public async Task<Registration> UpdateRegistrationById(int registrationId, Registration registration)
        {
            Registration registrationToUpdate = await _dBContext
                .Registration
                .FirstOrDefaultAsync(x => x.Id == registrationId);

            if (registrationToUpdate != null)
            {
                registrationToUpdate.RegistrationDate = registration.RegistrationDate;
                registrationToUpdate.CustomerId = registration.CustomerId;
                registrationToUpdate.VehicleId = registration.VehicleId;
            }

            return registrationToUpdate;
        }
    }
}
