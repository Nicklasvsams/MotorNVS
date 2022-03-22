using MotorNVS.BL.DTOs.AddressDTO;
using MotorNVS.BL.DTOs.ZipcodeDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface IAddressService
    {
        Task<List<AddressResponse>> GetAllAddresses();
        Task<AddressResponse> GetAddressById(int addressId);
        Task<AddressResponse> DeleteAddressById(int addressId);
        Task<AddressResponse> CreateAddress(AddressRequest newAddress);
        Task<AddressResponse> UpdateAddress(int addressId, AddressRequest addressUpdate);
    }

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<AddressResponse>> GetAllAddresses()
        {
            List<Address> fuelList = await _addressRepository.SelectAllAddresses();

            return fuelList.Select(x => MapAddressToAddressResponse(x)).ToList();
        }

        public async Task<AddressResponse> GetAddressById(int addressId)
        {
            Address address = await _addressRepository.SelectAddressById(addressId);

            if (address != null)
            {
                return MapAddressToAddressResponse(address);
            }

            return null;
        }

        public async Task<AddressResponse> DeleteAddressById(int addressId)
        {
            Address deletedAddress = await _addressRepository.DeleteAddressById(addressId);

            if (deletedAddress != null)
            {
                return MapAddressToAddressResponse(deletedAddress);
            }

            return null;
        }

        public async Task<AddressResponse> CreateAddress(AddressRequest newAddress)
        {
            Address createdAddress = await _addressRepository.InsertNewAddress(MapAddressRequestToAddress(newAddress));

            if (createdAddress != null)
            {
                return MapAddressToAddressResponse(createdAddress);
            }

            return null;
        }

        public async Task<AddressResponse> UpdateAddress(int addressId, AddressRequest addressUpdate)
        {
            Address updatedAddress = await _addressRepository.UpdateAddressById(addressId, MapAddressRequestToAddress(addressUpdate));

            if (updatedAddress != null)
            {
                return MapAddressToAddressResponse(updatedAddress);
            }

            return null;
        }

        private static AddressResponse MapAddressToAddressResponse(Address address)
        {
            AddressResponse res =  new AddressResponse()
            {
                Id = address.Id,
                StreetAndNo = address.StreetAndNo,
                CreateDate = address.CreateDate,
                ZipcodeId = address.ZipCodeId
            };

            if (address.Zipcode != null)
            {
                res.ZipcodeResponse = new ZipcodeResponse()
                {
                    Id = address.Zipcode.Id,
                    ZipcodeNo = address.Zipcode.ZipcodeNo,
                    City = address.Zipcode.City
                };
            };

            return res;
        }

        private static Address MapAddressRequestToAddress(AddressRequest addressReq)
        {
            return new Address()
            {
                StreetAndNo = addressReq.StreetAndNo,
                CreateDate = addressReq.CreateDate,
                ZipCodeId = addressReq.ZipcodeId
            };
        }
    }
}
