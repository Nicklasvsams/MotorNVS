using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> SelectAllAddresses();
        Task<Address> SelectAddressById(int addressId);
        Task<Address> InsertNewAddress(Address address);
        Task<Address> DeleteAddressById(int addressId);
        Task<Address> UpdateAddressById(int addressId, Address address);
    }

    public class AddressRepository : IAddressRepository
    {
        private readonly MotorDBContext _dBContext;

        public AddressRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Address> DeleteAddressById(int addressId)
        {
            Address addressToDelete = await _dBContext
                .Address
                .FirstOrDefaultAsync(x => x.Id == addressId);

            if (addressToDelete != null)
            {
                _dBContext.Remove(addressToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return addressToDelete;
        }

        public async Task<Address> InsertNewAddress(Address address)
        {
            await _dBContext.AddAsync(address);
            await _dBContext.SaveChangesAsync();

            return address;
        }

        public async Task<Address> SelectAddressById(int addressId)
        {
            return await _dBContext
                .Address
                .Include("Zipcode")
                .FirstOrDefaultAsync(x => x.Id == addressId);
        }

        public async Task<List<Address>> SelectAllAddresses()
        {
            return await _dBContext
                .Address
                .Include("Zipcode")
                .ToListAsync();
        }

        public async Task<Address> UpdateAddressById(int addressId, Address address)
        {
            Address addressToUpdate = await _dBContext
                .Address
                .FirstOrDefaultAsync(x => x.Id == addressId);

            if (addressToUpdate != null)
            {
                addressToUpdate.StreetAndNo = address.StreetAndNo;
                addressToUpdate.ZipCodeId = address.ZipCodeId;

                await _dBContext.SaveChangesAsync();
            }

            return addressToUpdate;
        }
    }
}
