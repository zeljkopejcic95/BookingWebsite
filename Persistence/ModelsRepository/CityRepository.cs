using Entities;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.FilterRepositoryExtensions;

namespace Persistence.ModelsRepository;

public class CityRepository : RepositoryBase<City>, ICityRepository
{
    public CityRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<City>> GetAllCitiesAsync(
        CityParameters cityParameters,
        bool trackChanges)
    {
        var cities = FindAll(trackChanges)
            .FilterCities(cityParameters)
            .OrderBy(c => c.Name);

        return await PagedList<City>.ToPagedList(
            cities,
            cityParameters.PageNumber,
            cityParameters.PageSize);
    }

    public async Task<City> GetCityAsync(int cityId, bool trackChanges) =>
        await FindByCondition(c => c.Id == cityId, trackChanges)
        .SingleOrDefaultAsync();

    public void CreateCity(City city) => Create(city);

    public void DeleteCity(City city) => Delete(city);
}
