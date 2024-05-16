using dotnet_web_api_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
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
                // URL de la API externa
                string apiUrl = "https://api-aspirantesweb.igrtecapi.site/api/Aspirantes";

                var client = _httpClientFactory.CreateClient();

                // Realiza la solicitud GET a la API externa
                var response = await client.GetAsync(apiUrl);

                // Si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta como una cadena JSON
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializa el contenido JSON en una lista de objetos UserModel
                    var usuarios = JsonSerializer.Deserialize<List<UserModel>>(content);

                    // Pasar la lista de usuarios a la vista para mostrarla
                    Console.WriteLine("Usuarios: ", content);
                    return View(usuarios);
                }
                else
                {
                    //si la solicitud no fue exitosa
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción durante la solicitud se da feedack
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
