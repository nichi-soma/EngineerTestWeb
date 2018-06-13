using EngineerTest.Data.Models;
using EngineerTest.Data.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace EngineerTest.Api.Controllers
{

    [Route("api/tmdb")]
    public class TmdbController : Controller
    {
        private IDataRepository<Movie, int> _iRepo;
        public TmdbController(IServiceProvider provider)
        {
            _iRepo = provider.GetService<IDataRepository<Movie, int>>();// repo;
        }

        // GET: api/values  
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _iRepo.GetAll();
        }


        [HttpGet("{filter}")]
        public IEnumerable<Movie> Get(string filter)
        {
            return _iRepo.GetAll(filter);
        }

        // GET api/values/5  
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _iRepo.Get(id);
        }

        // POST api/values  
        [HttpPost]
        public void Post([FromBody]List<Movie> movie)
        {
           _iRepo.Add(movie);
        }

        // POST api/values  
        [HttpPut]
        public void Put([FromBody]Movie movie)
        {
            _iRepo.Update(movie.movieId, movie);
        }

        // DELETE api/values/5  
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _iRepo.Delete(id);
        }
    }
}