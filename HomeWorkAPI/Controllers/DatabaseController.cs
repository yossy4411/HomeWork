using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Newtonsoft.Json;
namespace HomeWorkAPI.Controllers
{
    public abstract class DataBaseController : ControllerBase
    {
        public MySqlConnection _connection;
        public DataBaseController(string key)
        {
            Auth auth = Auth.FromFile()[key];
            
            _connection = new MySqlConnection($"Server=34.168.36.70;Port=3306;Database={auth.Database};User ID={auth.Username};Password={auth.Password};");
            _connection.Open();
        }
    }
    internal class Auth(string username, string password, string database)
    {
        public static Dictionary<string, Auth> FromFile() =>
           JsonConvert.DeserializeObject<Dictionary<string, Auth>>(File.ReadAllText("auth.json")) ?? [];
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
        public string Database { get; set; } = database;
    }
}
