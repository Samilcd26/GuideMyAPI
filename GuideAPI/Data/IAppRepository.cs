using GuideAPI.Models;

namespace GuideAPI.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        bool SaveAll(); 

        List<City> GetCities();
        List<Photo> GetPhotosByCity(int id);
        City GetCityById(int CityId);
        Photo GetPhotoById(int Id);
    }
}
