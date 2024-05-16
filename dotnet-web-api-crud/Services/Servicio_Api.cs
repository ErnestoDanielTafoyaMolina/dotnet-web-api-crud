using dotnet_web_api_crud.Models;
using Newtonsoft.Json;
using System.Text;

namespace dotnet_web_api_crud.Services
{
    public class Servicio_Api:IServicio_API
    {
        private static string _baseurl;

        public Servicio_Api()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<List<UserModel>> Lista()
        {
            List<UserModel> Usuarios = new List<UserModel>();

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.GetAsync("/");

            Console.WriteLine("Respuesta: " + response);
            Console.WriteLine("BaseUrl: " + _baseurl);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<List<UserModel>>(json_respuesta);

                Usuarios = resultado;
            }

            return Usuarios;
        }
        public async Task<UserModel> Obtener(int idUsuario)
        {
            UserModel Usuario = new UserModel();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.GetAsync($"/{idUsuario}");
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<UserModel>(json_respuesta);

                Usuario = resultado;
            }

            return Usuario;

        }

        public async Task<bool> Guardar(UserModel user)
        {

            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("/", content);
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(UserModel user)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("/", content);
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Delete(int id)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

         

            var response = await cliente.DeleteAsync($"/{id}");
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

    }
}
