namespace dotnet_web_api_crud.Models
{
    public interface IUsuario
    {
        int Id { get; set; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        string CorreoElectronico { get; set; }
        string NumTelefonico { get; set; }
        string LugarNacimiento { get; set; }
    }
    public class UserModel : IUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string NumTelefonico { get; set; }
        public string LugarNacimiento { get; set; }
    }
}
