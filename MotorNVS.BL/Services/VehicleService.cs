using MotorNVS.BL.DTOs.CategoryDTO;
using MotorNVS.BL.DTOs.FuelDTO;
using MotorNVS.BL.DTOs.VehicleDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleResponse>> GetAllVehicles();
        Task<VehicleResponse> GetVehicleById(int vehicleId);
        Task<VehicleResponse> DeleteVehicleById(int vehicleId);
        Task<VehicleResponse> CreateVehicle(VehicleRequest newVehicle);
        Task<VehicleResponse> UpdateVehicle(int vehicleId, VehicleRequest vehicleUpdate);
        Task<VehicleResponse> VehicleActivation(int vehicleId);
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResponse> CreateVehicle(VehicleRequest newVehicle)
        {
            Vehicle createdVehicle = await _vehicleRepository.InsertNewVehicle(MapVehicleRequestToVehicle(newVehicle));

            if (createdVehicle != null)
            {
                return MapVehicleToVehicleResponse(createdVehicle);
            }

            return null;
        }

        public async Task<VehicleResponse> DeleteVehicleById(int vehicleId)
        {
            Vehicle deletedVehicle = await _vehicleRepository.DeleteVehicleById(vehicleId);

            if (deletedVehicle != null)
            {
                return MapVehicleToVehicleResponse(deletedVehicle);
            }

            return null;
        }

        public async Task<List<VehicleResponse>> GetAllVehicles()
        {
            List<Vehicle> vehicleList = await _vehicleRepository.SelectAllVehicles();

            return vehicleList.Select(x => MapVehicleToVehicleResponse(x)).ToList();
        }

        public async Task<VehicleResponse> GetVehicleById(int vehicleId)
        {
            Vehicle vehicle = await _vehicleRepository.SelectVehicleById(vehicleId);

            if (vehicle != null)
            {
                return MapVehicleToVehicleResponse(vehicle);
            }

            return null;
        }

        public async Task<VehicleResponse> UpdateVehicle(int vehicleId, VehicleRequest vehicleUpdate)
        {
            Vehicle updatedVehicle = await _vehicleRepository.UpdateVehicleById(vehicleId, MapVehicleRequestToVehicle(vehicleUpdate));

            if (updatedVehicle != null)
            {
                return MapVehicleToVehicleResponse(updatedVehicle);
            }

            return null;
        }

        public async Task<VehicleResponse> VehicleActivation(int vehicleId)
        {
            Vehicle vehicleActivation = await _vehicleRepository.UpdateVehicleActivation(vehicleId);

            if (vehicleActivation != null)
            {
                return MapVehicleToVehicleResponse(vehicleActivation);
            }

            return null;
        }

        private static VehicleResponse MapVehicleToVehicleResponse(Vehicle vehicle)
        {
            VehicleResponse res = new VehicleResponse()
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                CreateDate = vehicle.CreateDate,
                CategoryId = vehicle.CategoryId,
                FuelId = vehicle.FuelId,
                IsActive = vehicle.IsActive == "yes" ? true : false
            };

            if(vehicle.Fuel != null && vehicle.Category != null)
            {
                res.FuelResponse = new FuelResponse()
                {
                    Id = vehicle.Fuel.Id,
                    FuelName = vehicle.Fuel.FuelName
                };

                res.CategoryResponse = new CategoryResponse()
                {
                    Id = vehicle.Category.Id,
                    CategoryName = vehicle.Category.CategoryName
                };
            }

            return res;
        }

        private static Vehicle MapVehicleRequestToVehicle(VehicleRequest vehicleReq)
        {
            return new Vehicle()
            {
                Make = vehicleReq.Make,
                Model = vehicleReq.Model,
                CreateDate = vehicleReq.CreateDate,
                CategoryId = vehicleReq.CategoryId,
                FuelId = vehicleReq.FuelId,
                IsActive = vehicleReq.IsActive ? "yes" : "no"
            };
        }
    }
}
