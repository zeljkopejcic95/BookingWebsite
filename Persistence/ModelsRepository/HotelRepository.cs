using Entities;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.FilterRepositoryExtensions;

namespace Persistence.ModelsRepository;

public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
{
    public HotelRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Hotel>> GetAllHotelsAsync(
        HotelParameters hotelParameters,
        bool trackChanges)
    {
        var hotels = FindAll(trackChanges)
            .FilterHotels(hotelParameters)
            .OrderBy(h => h.Name);

        return await PagedList<Hotel>.ToPagedList(
           hotels,
           hotelParameters.PageNumber,
           hotelParameters.PageSize);
    }

    public async Task<Hotel> GetHotelAsync(int hotelId, bool trackChanges) =>
       await FindByCondition(h => h.Id == hotelId, trackChanges)
        .SingleOrDefaultAsync();

    public void CreateHotel(Hotel hotel) => Create(hotel);

    public void DeleteHotel(Hotel hotel) => Delete(hotel);

    public async Task<PagedList<Hotel>> GetHotelsByCityAsync(
        int cityId,
        HotelParameters hotelParameters,
        bool trackChanges)
    {
        var hotels = FindByCondition(h => h.CityId == cityId, trackChanges)
        .OrderBy(h => h.Name);

        return await PagedList<Hotel>.ToPagedList(
            hotels,
            hotelParameters.PageNumber,
            hotelParameters.PageSize);
    }
}
