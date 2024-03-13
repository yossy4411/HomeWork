using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using ScheduleLib;
using System;
using System.Data;
using System.Net;

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
        public IActionResult Get(int areacode = 0)
        {
            try
            {
                using MySqlCommand cmd = areacode == 0 ?
                    new MySqlCommand($"SELECT * FROM schools;", _connection) :
                    new MySqlCommand($"SELECT * FROM schools WHERE LPAD(posting_address, 7, '0') LIKE '{areacode}%';", _connection);
                using var reader = cmd.ExecuteReader();
                List<SchoolObject> values = [];
                while (reader.Read())
                {
                    string post = (reader["posting_address"].ToString())?.PadLeft(7, '0')??string.Empty;
                    values.Add(new SchoolObject
                    {
                        Id = (string)reader["school_id"],
                        Name = (string)reader["school_name"],
                        Post = post,
                        Address = (string)reader["school_address"],
                    });

                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost("PostTest")]
        public IActionResult Post(string body)
        {
            return Ok(body + " Received!");
        }
    }
}
