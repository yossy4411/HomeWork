using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace HomeWorkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;
        public DatabaseController()
        {
            _connectionString = "Server=34.168.36.70;Port=3306;Database=homework;User ID=homework;Password=webapi-homework;";
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            try
            {
                using var cmd = new MySqlCommand("SELECT * FROM schools", _connection);
                using var reader = cmd.ExecuteReader();
                List<object> values = [];
                while (reader.Read())
                {
                    if (((string)reader[0])[0] == 'A')
                    {
                        values.Add(new { Id = reader[0] });
                    }
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostTest")]
        public IActionResult Post(string body)
        {
            return Ok(body + " Received!");
        }
    }
}
