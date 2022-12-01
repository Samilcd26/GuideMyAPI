using GuideAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuideAPI.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

      
        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {

            
            var cities = _context.Cities.Include(c=>c.Photos).ToList();
           
            return cities;
        }

        public City GetCityById(int CityId)
        {
            var city = _context.Cities.Include(c=>c.Photos).FirstOrDefault(c=>c.Id==CityId);
            return city;
        }

        public Photo GetPhotoById(int Id)
        {
            var photo = _context.Photos.FirstOrDefault(c=>c.Id==Id);
            return photo;
        }

        public List<Photo> GetPhotosByCity(int cityid)
        {
            var pohotos = _context.Photos.Where(p => p.CityId == cityid).ToList();
            return pohotos;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
