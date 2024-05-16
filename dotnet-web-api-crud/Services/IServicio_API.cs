using dotnet_web_api_crud.Models;

namespace dotnet_web_api_crud.Services
{
    public interface IServicio_API
    {
        Task<List<UserModel>> Lista();
        Task<UserModel> Obtener(int idUsuario);
        Task<bool> Guardar(UserModel user);
        Task<bool> Editar(UserModel user);
        Task<bool> Delete(int id);

    }
}
