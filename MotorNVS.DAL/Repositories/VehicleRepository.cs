using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> SelectAllVehicles();
        Task<Vehicle> SelectVehicleById(int vehicleId);
        Task<Vehicle> InsertNewVehicle(Vehicle vehicle);
        Task<Vehicle> DeleteVehicleById(int vehicleId);
        Task<Vehicle> UpdateVehicleById(int vehicleId, Vehicle vehicle);
        Task<Vehicle> UpdateVehicleActivation(int vehicleId);
    }

    public class VehicleRepository : IVehicleRepository
    {
        private readonly MotorDBContext _dBContext;

        public VehicleRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Vehicle> DeleteVehicleById(int vehicleId)
        {
            Vehicle vehicleToDelete = await _dBContext
                .Vehicle
                .FirstOrDefaultAsync(x => x.Id == vehicleId);

            if (vehicleToDelete != null)
            {
                _dBContext.Remove(vehicleToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return vehicleToDelete;
        }

        public async Task<Vehicle> InsertNewVehicle(Vehicle vehicle)
        {
            await _dBContext.AddAsync(vehicle);
            await _dBContext.SaveChangesAsync();

            return vehicle;
        }

        public async Task<List<Vehicle>> SelectAllVehicles()
        {
            return await _dBContext
                .Vehicle
                .Include(x => x.Category)
                .Include(x => x.Fuel)
                .ToListAsync();
        }

        public async Task<Vehicle> SelectVehicleById(int vehicleId)
        {
            return await _dBContext
                .Vehicle
                .Include("Category")
                .Include("Fuel")
                .FirstOrDefaultAsync(x => x.Id == vehicleId);
        }

        public async Task<Vehicle> UpdateVehicleActivation(int vehicleId)
        {
            Vehicle vehicleActivation = await _dBContext
                .Vehicle
                .FirstOrDefaultAsync(x => x.Id == vehicleId);

            if (vehicleActivation != null)
            {
                if (vehicleActivation.IsActive == "yes")
                {
                    vehicleActivation.IsActive = "no";
                }
                else
                {
                    vehicleActivation.IsActive = "yes";
                }

                await _dBContext.SaveChangesAsync();
            }

            return vehicleActivation;
        }

        public async Task<Vehicle> UpdateVehicleById(int vehicleId, Vehicle vehicle)
        {
            Vehicle vehicleToUpdate = await _dBContext
                .Vehicle
                .FirstOrDefaultAsync(x => x.Id == vehicleId);

            if (vehicleToUpdate != null)
            {
                vehicleToUpdate.Make = vehicle.Make;
                vehicleToUpdate.Model = vehicle.Model;
                vehicleToUpdate.CreateDate = vehicle.CreateDate;
                vehicleToUpdate.CategoryId = vehicle.CategoryId;
                vehicleToUpdate.FuelId = vehicle.FuelId;

                await _dBContext.SaveChangesAsync();
            }

            return vehicleToUpdate;
        }
    }
}
