using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MotorNVS.DAL.Repositories
{
    public interface IFuelRepository
    {
        Task<List<Fuel>> SelectAllFuels();
        Task<Fuel> SelectFuelById(int fuelId);
        Task<Fuel> InsertNewFuel(Fuel fuel);
        Task<Fuel> DeleteFuelById(int fuelId);
        Task<Fuel> UpdateFuelById(int fuelId, Fuel fuel);
    }

    public class FuelRepository : IFuelRepository
    {
        private readonly MotorDBContext _dBContext;

        public FuelRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Fuel> DeleteFuelById(int fuelId)
        {
            Fuel fuelToDelete = await _dBContext
                .Fuel
                .FirstOrDefaultAsync(x => x.Id == fuelId);

            if (fuelToDelete != null)
            {
                _dBContext.Remove(fuelToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return fuelToDelete;
        }

        public async Task<Fuel> SelectFuelById(int fuelId)
        {
            return await _dBContext
                .Fuel
                .FirstOrDefaultAsync(x => x.Id == fuelId);
        }

        public async Task<List<Fuel>> SelectAllFuels()
        {
            return await _dBContext
                .Fuel
                .ToListAsync();
        }

        public async Task<Fuel> InsertNewFuel(Fuel fuel)
        {
            await _dBContext.AddAsync(fuel);
            await _dBContext.SaveChangesAsync();

            return fuel;
        }

        public async Task<Fuel> UpdateFuelById(int fuelId, Fuel fuel)
        {
            Fuel fuelToUpdate = await _dBContext
                .Fuel
                .FirstOrDefaultAsync(x => x.Id == fuelId);

            if (fuelToUpdate != null)
            {
                fuelToUpdate.FuelName = fuel.FuelName;
            }

            return fuelToUpdate;
        }
    }
}
