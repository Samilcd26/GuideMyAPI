using AutoMapper;
using GuideAPI.Data;
using GuideAPI.Dtos;
using GuideAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        IAppRepository _appRepository;
        IMapper _mapper;
        public CitiesController(IAppRepository appRepository,IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
           
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);

        }


        [HttpPost]
        [Route("detail")]
        public ActionResult GetCityByID(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToRetun = _mapper.Map<CityForDetailDto>(city); 
            return Ok(cityToRetun);

        }

        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            var photos = _appRepository.GetPhotoById(cityId);
           
            return Ok(photos);

        }
    }
}
