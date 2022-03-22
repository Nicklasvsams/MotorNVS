using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface IZipcodeRepository
    {
        Task<List<Zipcode>> SelectAllZipcodes();
        Task<Zipcode> SelectZipcodeById(int zipcodeId);
        Task<Zipcode> InsertNewZipcode(Zipcode zipcode);
        Task<Zipcode> DeleteZipcodeById(int zipcodeId);
        Task<Zipcode> UpdateZipcodeById(int zipcodeId, Zipcode zipcode);
    }

    public class ZipcodeRepository : IZipcodeRepository
    {
        private readonly MotorDBContext _dBContext;

        public ZipcodeRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Zipcode> DeleteZipcodeById(int zipcodeId)
        {
            Zipcode zipcodeToDelete = await _dBContext
                .Zipcode
                .FirstOrDefaultAsync(x => x.Id == zipcodeId);

            if (zipcodeToDelete != null)
            {
                _dBContext.Remove(zipcodeToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return zipcodeToDelete;
        }

        public async Task<Zipcode> InsertNewZipcode(Zipcode zipcode)
        {
            await _dBContext.AddAsync(zipcode);
            await _dBContext.SaveChangesAsync();

            return zipcode;
        }

        public async Task<List<Zipcode>> SelectAllZipcodes()
        {
            return await _dBContext
                .Zipcode
                .ToListAsync();
        }

        public async Task<Zipcode> SelectZipcodeById(int zipcodeId)
        {
            return await _dBContext
                .Zipcode
                .FirstOrDefaultAsync(x => x.Id == zipcodeId);
        }

        public async Task<Zipcode> UpdateZipcodeById(int zipcodeId, Zipcode zipcode)
        {
            Zipcode zipcodeToUpdate = await _dBContext
                .Zipcode
                .FirstOrDefaultAsync(x => x.Id == zipcodeId);

            if (zipcodeToUpdate != null)
            {
                zipcodeToUpdate.ZipcodeNo = zipcode.ZipcodeNo;
                zipcodeToUpdate.City = zipcode.City;

                await _dBContext.SaveChangesAsync();
            }

            return zipcodeToUpdate;
        }
    }
}
