using MotorNVS.BL.DTOs.ZipcodeDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface IZipcodeService
    {
        Task<List<ZipcodeResponse>> GetAllZipcodes();
        Task<ZipcodeResponse> GetZipcodeById(int zipcodeId);
        Task<ZipcodeResponse> DeleteZipcodeById(int zipcodeId);
        Task<ZipcodeResponse> CreateZipcode(ZipcodeRequest newZipcode);
        Task<ZipcodeResponse> UpdateZipcode(int zipcodeId, ZipcodeRequest zipcodeUpdate);
    }

    public class ZipcodeService : IZipcodeService
    {
        private readonly IZipcodeRepository _zipcodeRepository;

        public ZipcodeService(IZipcodeRepository zipcodeRepository)
        {
            _zipcodeRepository = zipcodeRepository;
        }

        public async Task<ZipcodeResponse> CreateZipcode(ZipcodeRequest newZipcode)
        {
            Zipcode createdZipcode = await _zipcodeRepository.InsertNewZipcode(MapZipcodeRequestToZipcode(newZipcode));

            if (createdZipcode != null)
            {
                return MapZipcodeToZipcodeResponse(createdZipcode);
            }

            return null;
        }

        public async Task<ZipcodeResponse> DeleteZipcodeById(int zipcodeId)
        {
            Zipcode deletedZipcode = await _zipcodeRepository.DeleteZipcodeById(zipcodeId);

            if (deletedZipcode != null)
            {
                return MapZipcodeToZipcodeResponse(deletedZipcode);
            }

            return null;
        }

        public async Task<List<ZipcodeResponse>> GetAllZipcodes()
        {
            List<Zipcode> zipcodeList = await _zipcodeRepository.SelectAllZipcodes();

            return zipcodeList.Select(x => MapZipcodeToZipcodeResponse(x)).ToList();
        }

        public async Task<ZipcodeResponse> GetZipcodeById(int zipcodeId)
        {
            Zipcode zipcode = await _zipcodeRepository.SelectZipcodeById(zipcodeId);

            if (zipcode != null)
            {
                return MapZipcodeToZipcodeResponse(zipcode);
            }

            return null;
        }

        public async Task<ZipcodeResponse> UpdateZipcode(int zipcodeId, ZipcodeRequest zipcodeUpdate)
        {
            Zipcode updatedZipcode = await _zipcodeRepository.UpdateZipcodeById(zipcodeId, MapZipcodeRequestToZipcode(zipcodeUpdate));

            if (updatedZipcode != null)
            {
                return MapZipcodeToZipcodeResponse(updatedZipcode);
            }

            return null;
        }

        private static ZipcodeResponse MapZipcodeToZipcodeResponse(Zipcode zipcode)
        {
            return new ZipcodeResponse()
            {
                Id = zipcode.Id,
                ZipcodeNo = zipcode.ZipcodeNo,
                City = zipcode.City
            };
        }

        private static Zipcode MapZipcodeRequestToZipcode(ZipcodeRequest zipcodeReq)
        {
            return new Zipcode()
            {
                ZipcodeNo = zipcodeReq.ZipcodeNo,
                City = zipcodeReq.City
            };
        }
    }
}
