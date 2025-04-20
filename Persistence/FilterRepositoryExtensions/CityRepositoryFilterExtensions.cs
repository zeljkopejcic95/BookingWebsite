using Entities.Models;
using Entities.Paging.Parameters;

namespace Persistence.FilterRepositoryExtensions;

public static class CityRepositoryFilterExtensions
{
    public static IQueryable<City> FilterCities(
        this IQueryable<City> cities,
        CityParameters cityParameters)
    {
        if (!string.IsNullOrWhiteSpace(cityParameters.Name))
        {
            var lowerName = cityParameters.Name.Trim().ToLower();
            cities = cities.Where(c => c.Name.ToLower().Contains(lowerName));

        }

        if (!string.IsNullOrWhiteSpace(cityParameters.Country))
        {
            var lowerCountry = cityParameters.Country.Trim().ToLower();
            cities = cities.Where(c => c.Country.ToLower().Contains(lowerCountry));
        }
        return cities;
    }
}
