using dotnet_web_api_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_web_api_crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // URL de la API externa que deseas consumir
                string apiUrl = "https://api-aspirantesweb.igrtecapi.site/api/Aspirantes";

                var client = _httpClientFactory.CreateClient();

                // Realiza la solicitud GET a la API externa
                var response = await client.GetAsync(apiUrl);

                // Si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una lista de usuarios
                    var usuarios = await response.Content.ReadAsAsync<List<UserModel>>();

                    // Puedes pasar la lista de usuarios a la vista para mostrarla
                    return View(usuarios);
                }
                else
                {
                    // Si la solicitud no fue exitosa, maneja el error adecuadamente
                    // Aquí puedes redirigir a una vista de error o hacer cualquier otra acción
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción durante la solicitud, maneja el error adecuadamente
                // Por ejemplo, puedes registrar el error y redirigir a una vista de error
                _logger.LogError(ex, "Error al realizar la solicitud a la API externa");
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
