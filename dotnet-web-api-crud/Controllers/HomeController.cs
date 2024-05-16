using dotnet_web_api_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using dotnet_web_api_crud.Services;

namespace dotnet_web_api_crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicio_api;


        public HomeController(IServicio_API servicio_API)
        {
            _servicio_api = servicio_API;
        }

        public async Task<IActionResult> Index()
        {
            List<UserModel> Usuarios = await _servicio_api.Lista();
            return View(Usuarios);
        }

        public async Task<IActionResult> Usuario(int idUsuario)
        {
            UserModel Usuario = new UserModel();

            ViewBag.Accion = "Nuevo Usuario";

            if (idUsuario != 0)
            {
                Usuario = await _servicio_api.Obtener(idUsuario);
                ViewBag.Accion = "Editar Usuario";
            }
            return View(Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(UserModel ob_usuario)
        {
            bool respuesta;
            if (ob_usuario.Id == 0) {
                respuesta = await _servicio_api.Guardar(ob_usuario);
            }
            else {
                respuesta = await _servicio_api.Editar(ob_usuario);
            }

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet] 
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicio_api.Delete(id);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
