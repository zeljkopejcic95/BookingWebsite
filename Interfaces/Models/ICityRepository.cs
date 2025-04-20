using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Interfaces;

public interface ICityRepository
{
    Task<PagedList<City>> GetAllCitiesAsync(CityParameters cityParameters, bool trackChanges);
    Task<City> GetCityAsync(int cityId, bool trackChanges);
    void CreateCity(City city);
    void DeleteCity(City city);
}
