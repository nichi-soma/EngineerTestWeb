using EngineerTest.Data.Models.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineerTest.Data.Models.DataManager
{
    public class MovieManager : IDataRepository<Movie, int>
    {
        ApplicationContext applicationContext;

        public MovieManager(IServiceProvider provider)
        {
            applicationContext = provider.GetService<ApplicationContext>();
        }

        public int Add(List<Movie> movie)
        {
            try
            {
                applicationContext.Movies.AddRange(movie);
                int count = applicationContext.SaveChanges();
                return count;
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                int movieId = 0;
                var movie = applicationContext.Movies.FirstOrDefault(b => b.movieId == id);
                if (movie != null)
                {
                    applicationContext.Movies.Remove(movie);
                    movieId = applicationContext.SaveChanges();
                }
                return movieId;
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public Movie Get(int id)
        {
            try
            {
                var movie = applicationContext.Movies.FirstOrDefault(n => n.movieId == id);
                return movie;
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            try
            {
                var movies = applicationContext.Movies.ToList();
                return movies;
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public IEnumerable<Movie> GetAll(string filter)
        {
            try
            {
                return applicationContext.Movies.Where(n => n.searchText == filter).ToList();
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public int Update(int id, Movie b)
        {
            try
            {
                int movieId = 0;
                var movie = applicationContext.Movies.Find(id);
                if (movie != null)
                {
                    //TODo: Update logic goes here
                }
                return movieId;
            }
            catch(Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }
    }
}
