using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.Models;
using System.Text.Json.Nodes;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult GetPopulares()
        {
            Models.Movie movie = new Models.Movie();
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("movie/popular?api_key=2156ee3bb0970659b0b34c1ce9a63a76&language=es-MX");
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    dynamic JsonObject = JObject.Parse(readTask.Result);

                    foreach (var registro in JsonObject.results)
                    {
                        Models.Movie movie1 = new Models.Movie();

                        movie1.IdPelicula = registro.id;
                        movie1.Titulo = registro.original_title;
                        movie1.Descripcion = registro.overview;
                        movie1.Poster = "https://media.themoviedb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;

                        movie.Movies.Add(movie1);
                    }
                    return View(movie);
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public IActionResult FavoriteGetAll()
        {
            Models.Movie movie = new Models.Movie();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("account/20961218/favorite/movies?language=en-US&session_id=3d4436685fc28654df211677dbb3503e95357661&api_key=2156ee3bb0970659b0b34c1ce9a63a76");
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    dynamic JsonObject = JObject.Parse(readTask.Result);

                    foreach (var registro in JsonObject.results)
                    {
                        Models.Movie movie1 = new Models.Movie();

                        movie1.IdPelicula = registro.id;
                        movie1.Titulo = registro.original_title;
                        movie1.Descripcion = registro.overview;
                        movie1.Poster = "https://media.themoviedb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;

                        movie.Movies.Add(movie1);
                    }
                    return View(movie);
                }
                else
                {
                    return View();
                }
            }
        }

        public IActionResult Favorito(int idPelicula)
        {
            Models.Movie movie = new Models.Movie();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.PostAsJsonAsync("account/20961218/favorite?api_key=2156ee3bb0970659b0b34c1ce9a63a76&session_id=3d4436685fc28654df211677dbb3503e95357661", new
                {
                    media_type = "movie",
                    media_id = idPelicula,
                    favorite = true
                });
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    //var readTask = respuesta.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    //movie.Movies = new List<object>();
                    //dynamic JsonObject = JObject.Parse(readTask.Result);

                    //foreach (var registro in JsonObject.results)
                    //{
                    //    Models.Movie movie1 = new Models.Movie();

                    //    movie1.IdPelicula = registro.id;
                    //    movie1.Titulo = registro.original_title;
                    //    movie1.Descripcion = registro.overview;
                    //    movie1.Poster = "https://media.themoviedb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;

                    //    movie.Movies.Add(movie1);
                    //}
                    ViewBag.Mensaje = "La pelicula se agrego correctamente a Favoritos";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "La pelicula no se pudo agregar a Favoritos";
                    return View("Modal");
                }
            }
        }

        public IActionResult FavoritoDelete(int idPelicula)
        {
            Models.Movie movie = new Models.Movie();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.PostAsJsonAsync("account/20961218/favorite?api_key=2156ee3bb0970659b0b34c1ce9a63a76&session_id=3d4436685fc28654df211677dbb3503e95357661", new
                {
                    media_type = "movie",
                    media_id = idPelicula,
                    favorite = false
                });
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "La pelicula se elimino correctamente de Favoritos";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "La pelicula no se pudo eliminar de Favoritos";
                    return View("Modal");
                }
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            Dictionary<string, object> resultRol = BL.Rol.GetAll();
            bool rolCorrect = (bool)resultRol["Resultado"];
            if (rolCorrect == true)
            {
                ML.Rol rol = (ML.Rol)resultRol["Rol"];
                if (usuario.Rol == null)
                {
                    usuario.Rol = new ML.Rol();
                }
                usuario.Rol.Roles = rol.Roles;

                return View(usuario);
            }
            else
            {
                string exepcion = (string)resultRol["Exepcion"];
                ViewBag.Mensaje = "Ocurrio un error al recuperar la informacion" + exepcion;
                return PartialView("Modal");
            }
        }
        [HttpPost]
        public IActionResult Login(ML.Usuario usuario)
        {
            Dictionary<string, object> resultado = BL.Usuario.Add(usuario);
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                ViewBag.Mensaje = "El usuario fue agregado correctamente";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)resultado["Excepcion"];
                ViewBag.Mensaje = "El usuario no pudo ser registrado: " + excepcion;
                return PartialView("Modal");
            }
        }
    }
}
