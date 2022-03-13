using MotorNVS.BL.DTOs.FuelDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface IFuelService
    {
        Task<List<FuelResponse>> GetAllFuels();
        Task<FuelResponse> GetFuelById(int fuelId);
        Task<FuelResponse> DeleteFuelById(int fuelId);
        Task<FuelResponse> CreateFuel(FuelRequest newFuel);
        Task<FuelResponse> UpdateFuel(int fuelId, FuelRequest fuelUpdate);
    }

    public class FuelService : IFuelService
    {
        private readonly IFuelRepository _fuelRepository;

        public FuelService(IFuelRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task<List<FuelResponse>> GetAllFuels()
        {
            List<Fuel> fuelList = await _fuelRepository.SelectAllFuels();

            return fuelList.Select(x => MapFuelToFuelResponse(x)).ToList();
        }

        public async Task<FuelResponse> GetFuelById(int fuelId)
        {
            Fuel fuel = await _fuelRepository.SelectFuelById(fuelId);

            if (fuel != null)
            {
                return MapFuelToFuelResponse(fuel);
            }

            return null;
        }

        public async Task<FuelResponse> DeleteFuelById(int fuelId)
        {
            Fuel deletedFuel = await _fuelRepository.DeleteFuelById(fuelId);

            if (deletedFuel != null)
            {
                return MapFuelToFuelResponse(deletedFuel);
            }

            return null;
        }

        public async Task<FuelResponse> CreateFuel(FuelRequest newFuel)
        {
            Fuel createdFuel = await _fuelRepository.InsertNewFuel(MapFuelRequestToFuel(newFuel));

            if (createdFuel != null)
            {
                return MapFuelToFuelResponse(createdFuel);
            }

            return null;
        }

        public async Task<FuelResponse> UpdateFuel(int fuelId, FuelRequest fuelUpdate)
        {
            Fuel updatedFuel = await _fuelRepository.UpdateFuelById(fuelId, MapFuelRequestToFuel(fuelUpdate));

            if (updatedFuel != null)
            {
                return MapFuelToFuelResponse(updatedFuel);
            }

            return null;
        }

        private static FuelResponse MapFuelToFuelResponse(Fuel fuel)
        {
            return new FuelResponse()
            {
                Id = fuel.Id,
                FuelName = fuel.FuelName
            };
        }

        private static Fuel MapFuelRequestToFuel(FuelRequest fuelReq)
        {
            return new Fuel()
            {
                FuelName = fuelReq.FuelName
            };
        }
    }
}
