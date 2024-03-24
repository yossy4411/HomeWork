using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
    public class SchoolsController() : DataBaseController("default")
    {
        [HttpGet]
        public ActionResult<List<SchoolObject>> Get()
        {
            try
            {
                using MySqlCommand cmd = new($"SELECT * FROM schools", _connection);
                using var reader = cmd.ExecuteReader();
                List<SchoolObject> values = [];
                while (reader.Read())
                {
                    values.Add(new SchoolObject
                    {
                        Id = (string)reader["school_id"],
                        Name = (string)reader["school_name"],
                        Post = (string)reader["posting_address"],
                        Address = (string)reader["school_address"],
                    });

                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("area")]
        public ActionResult<List<SchoolObject>> SearchByAreaCode(int areacode = 0)
        {
            try
            {
                using MySqlCommand cmd = new($"SELECT * FROM schools WHERE posting_address LIKE @Areacode", _connection);
                cmd.Parameters.AddWithValue("@areacode", areacode+"%");
                using var reader = cmd.ExecuteReader();
                List<SchoolObject> values = [];
                while (reader.Read())
                {
                    values.Add(new SchoolObject
                    {
                        Id = (string)reader["school_id"],
                        Name = (string)reader["school_name"],
                        Post = (string)reader["posting_address"],
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
        [HttpGet("id")]
        public ActionResult<SchoolObject> GetById(string id)
        {
            try
            {
                using MySqlCommand cmd = new($"SELECT * FROM schools WHERE school_id = @Id", _connection);
                cmd.Parameters.AddWithValue("id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return Ok(
                        new SchoolObject
                        {
                            Id = (string)reader["school_id"],
                            Name = (string)reader["school_name"],
                            Post = (string)reader["posting_address"],
                            Address = (string)reader["school_address"],
                        });
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
