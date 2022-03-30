using MotorNVS.BL.DTOs.AddressDTO;
using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.DTOs.FuelDTO;
using MotorNVS.BL.DTOs.RegistrationDTO;
using MotorNVS.BL.DTOs.VehicleDTO;
using MotorNVS.BL.DTOs.ZipcodeDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface IRegistrationService
    {
        Task<List<RegistrationResponse>> GetAllRegistrations();
        Task<List<RegistrationResponse>> GetAllRegistrationsByVehicleId(int vehicleId);
        Task<RegistrationResponse> GetRegistrationById(int registrationId);
        Task<RegistrationResponse> DeleteRegistrationById(int registrationId);
        Task<RegistrationResponse> CreateRegistration(RegistrationRequest newRegistration);
        Task<RegistrationResponse> UpdateRegistration(int registrationId, RegistrationRequest registrationRequest);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<RegistrationResponse> CreateRegistration(RegistrationRequest newRegistration)
        {
            Registration createdRegistration = await _registrationRepository.InsertNewRegistration(MapRegistrationRequestToRegistration(newRegistration));

            if (createdRegistration != null)
            {
                return MapRegistrationToRegistrationResponse(createdRegistration);
            }

            return null;
        }

        public async Task<RegistrationResponse> DeleteRegistrationById(int registrationId)
        {
            Registration deletedRegistration = await _registrationRepository.DeleteRegistrationById(registrationId);

            if (deletedRegistration != null)
            {
                return MapRegistrationToRegistrationResponse(deletedRegistration);
            }

            return null;
        }

        public async Task<List<RegistrationResponse>> GetAllRegistrations()
        {
            List<Registration> registrationList = await _registrationRepository.SelectAllRegistrations();

            return registrationList.Select(x => MapRegistrationToRegistrationResponse(x)).ToList();
        }

        public async Task<List<RegistrationResponse>> GetAllRegistrationsByVehicleId(int vehicleId)
        {
            List<Registration> registrationList = await _registrationRepository.SelectAllRegistrationsByVehicleId(vehicleId);

            return registrationList.Select(x => MapRegistrationToRegistrationResponse(x)).ToList();
        }

        public async Task<RegistrationResponse> GetRegistrationById(int registrationId)
        {
            Registration registration = await _registrationRepository.SelectRegistrationById(registrationId);

            if (registration != null)
            {
                return MapRegistrationToRegistrationResponse(registration);
            }

            return null;
        }

        public async Task<RegistrationResponse> UpdateRegistration(int registrationId, RegistrationRequest registrationRequest)
        {
            Registration updatedRegistration = await _registrationRepository.UpdateRegistrationById(registrationId, MapRegistrationRequestToRegistration(registrationRequest));

            if (updatedRegistration != null)
            {
                return MapRegistrationToRegistrationResponse(updatedRegistration);
            }

            return null;
        }

        private static RegistrationResponse MapRegistrationToRegistrationResponse(Registration registration)
        {
            RegistrationResponse regRes = new RegistrationResponse()
            {
                Id = registration.Id,
                RegistrationDate = registration.RegistrationDate,
                CustomerId = registration.CustomerId,
                VehicleId = registration.VehicleId
            };

            if (registration.Customer != null && registration.Vehicle != null)
            {
                regRes.CustomerResponse = new CustomerResponse()
                {
                    Id = registration.Customer.Id,
                    FirstName = registration.Customer.FirstName,
                    LastName = registration.Customer.LastName,
                    CreateDate = registration.Customer.CreateDate,
                    AddressId = registration.Customer.AddressId,
                    AddressResponse = new AddressResponse()
                    {
                        Id = registration.Customer.Address.Id,
                        StreetAndNo = registration.Customer.Address.StreetAndNo,
                        CreateDate = registration.Customer.Address.CreateDate,
                        ZipcodeId = registration.Customer.Address.ZipCodeId,
                        ZipcodeResponse = new ZipcodeResponse()
                        {
                            Id = registration.Customer.Address.Zipcode.Id,
                            ZipcodeNo = registration.Customer.Address.Zipcode.ZipcodeNo,
                            City = registration.Customer.Address.Zipcode.City
                        }
                    }
                };
                regRes.VehicleResponse = new VehicleResponse()
                {
                    Id = registration.Vehicle.Id,
                    Make = registration.Vehicle.Make,
                    Model = registration.Vehicle.Model,
                    CreateDate = registration.Vehicle.CreateDate,
                    CategoryId = registration.Vehicle.CategoryId,
                    FuelId = registration.Vehicle.FuelId,
                    CategoryResponse = new DTOs.CategoryDTO.CategoryResponse()
                    {
                        Id = registration.Vehicle.Category.Id,
                        CategoryName = registration.Vehicle.Category.CategoryName
                    },
                    FuelResponse = new FuelResponse()
                    {
                        Id = registration.Vehicle.Fuel.Id,
                        FuelName = registration.Vehicle.Fuel.FuelName
                    }
                };
            };

            return regRes;
        }

        private static Registration MapRegistrationRequestToRegistration(RegistrationRequest registrationReq)
        {
            return new Registration()
            {
                RegistrationDate = registrationReq.RegistrationDate,
                CustomerId = registrationReq.CustomerId,
                VehicleId = registrationReq.VehicleId
            };
        }
    }
}
