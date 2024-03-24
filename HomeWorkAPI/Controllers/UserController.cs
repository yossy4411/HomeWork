using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ScheduleLib;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeWorkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController() : DataBaseController("private")
    {
        // GET: api/<ValuesController>
        [HttpGet("login")]
        public ActionResult Get()
        {
            return Ok();
        }
        public static string GenerateSalt()
        {
            var buff = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buff);
            }
            return Convert.ToBase64String(buff);
        }
        public static string GeneratePasswordHash(string pwd, string salt)
        {
            var saltAndPwd = string.Concat(pwd, salt);
            var encoder = new UTF8Encoding();
            var buffer = encoder.GetBytes(saltAndPwd);
            var hash = SHA256.HashData(buffer);
            return Convert.ToBase64String(hash);
        }
        [HttpGet("exists/{username}")]
        public async Task<bool> Exists(string username)
        {
            bool exists;
            using var command = new MySqlCommand("SELECT COUNT(*) FROM users WHERE UserName = @User", _connection);
            try
            {
                command.Parameters.AddWithValue("@User", username);
                var count = await command.ExecuteScalarAsync();
                exists = Convert.ToInt32(count) > 0;
            }
            catch
            {
                exists = true;
            }
            return exists;
        }
        [HttpPut("create")]
        public async Task<ActionResult> CreateUser(string username, string password)
        {
            var exist = await Exists(username);
            if (exist)
            {
                var salt = GenerateSalt();
                var hash = GeneratePasswordHash(password, salt);
                try
                {
                    using var cmd = new MySqlCommand("INSERT INTO users (UserName, PasswordHash, Salt) VALUES (@User, @Hash, @Salt)", _connection);
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Hash", hash);
                    cmd.Parameters.AddWithValue("@Salt", salt);
                    if (cmd.ExecuteNonQuery() > 0)
                        return Ok();
                    else
                        return BadRequest();

                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
            else
            {
                return BadRequest("The specified user already exists");
            }
        }

        
    }
}
