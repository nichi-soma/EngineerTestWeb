using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EngineerTest
{
    public class TmdbHelper
    {
        public IConfiguration Configuration { get; }

        private const string ApiVersion = "3";
        private const string _apiBaseURI = "https://api.themoviedb.org/";
        public static string apiKey = "49819d70df6228e01af6f2b2b5ca6f41";

        /// <summary>
        /// Method to return TMDB client URL
        /// </summary>
        public HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();

            //Passing service base url  
            client.BaseAddress = new Uri(_apiBaseURI + ApiVersion);

            client.DefaultRequestHeaders.Clear();

            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    public class MovieDTO
    {
        public int movieId { get; set; }
        public string searchText { get; set; }
        public float vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public DateTime? release_date { get; set; }
    }

    public class ResultDTO
    {
        public List<MovieDTO> results { get; set; }
    }
}
