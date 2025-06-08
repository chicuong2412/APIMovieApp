using API.DTOs;
using API.Models;
using API.Repositories;
using API.ReturnCodes.SuccessCodes;

namespace API.Services
{
    public class GenereServices
    {
        private readonly GenereRepository _genereRepository;

        public GenereServices(GenereRepository genereRepository)
        {
            _genereRepository = genereRepository;
        }

        public async Task<APIresponse<List<Genere>>> GetAllGeneres()
        {
            return new APIresponse<List<Genere>>(SuccessCodes.Success)
            {
                data = await _genereRepository.GetAllGenenres()
            };
        }

    }
}
