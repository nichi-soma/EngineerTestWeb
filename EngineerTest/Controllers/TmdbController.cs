using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EngineerTest.Controllers
{
    [Authorize]
    public class TmdbController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string param)
        {
            try
            {
                TmdbHelper _tmdbHelper = new TmdbHelper();
                List<MovieDTO> movieDto = new List<MovieDTO>();

                HttpClient client = _tmdbHelper.InitializeClient();

                if (string.IsNullOrEmpty(param))
                {
                    return View(movieDto);
                }

                string apiKey = TmdbHelper.apiKey;
                var parameters = string.Empty;
                HttpWebRequest apiRequest;
                string apiResponse;
                var jsonString = string.Empty;

                // Calling Local EngineerTest.API using URL
                movieDto = GetData<List<MovieDTO>>("http://localhost:51585/api/Tmdb/", param);

                // If movie data is not present in our local database, then need to fetch from TMDB.
                if (movieDto == null || movieDto.Count == 0)
                {
                    // Fetching and storing only 20 records (1st page data) from TMDB.
                    // TODO: Need to know how the filtering condition in TMDB to apply the same in our filter logic from local database.

                    switch (param)
                    {
                        case "Upcoming":
                            {
                                parameters = $"/movie/upcoming?api_key={apiKey}&language=en-US&page=1";
                                break;
                            }

                        case "TopRated":
                            {
                                parameters = $"/movie/top_rated?api_key={apiKey}&language=en-US&page=1";
                                break;
                            }
                        case "Popular":
                            {
                                parameters = $"/movie/popular?api_key={apiKey}&language=en-US&page=1";
                                break;
                            }
                        default:
                            {
                                parameters = $"/search/movie?api_key={apiKey}&language=en-US&query=" + param + "&page=1&include_adult=false";
                                break;
                            }
                    }

                    apiRequest = WebRequest.Create(client.BaseAddress + parameters) as HttpWebRequest;
                    apiResponse = ResponseString(apiRequest);

                    ResultDTO dto = BindData<ResultDTO>(apiResponse, param);

                    movieDto = dto.results;

                    dto.results = dto.results.Select(m =>
                    {
                        m.searchText = param;
                        return m;
                    }).ToList();

                    // Storing result data fetched from TMDB into local database.
                    jsonString = JsonConvert.SerializeObject(dto.results);
                    PostData("http://localhost:51585/api/Tmdb/", jsonString);
                }

                return View(movieDto);
            }
            catch (Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public string ResponseString(HttpWebRequest apiRequest)
        {
            try
            {
                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public void PostData(string url, string postData)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public T GetData<T>(string url, string filterKey)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url + filterKey);

                var apiResponse = ResponseString(request);

                return BindData<T>(apiResponse, filterKey);
            }
            catch (Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }

        public T BindData<T>(string apiResponse, string searchText)
        {
            try
            {
                T obj = JsonConvert.DeserializeObject<T>(apiResponse);
                return obj;
            }
            catch (Exception ex)
            {
                // TODO: Need to Handle exception
                throw ex;
            }
        }
    }
}