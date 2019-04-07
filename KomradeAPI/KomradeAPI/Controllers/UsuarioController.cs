using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GuiaAlimentarAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuiaAlimentarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {
        public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Usuario>> Get(string username, string password)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("Select * from usuarios where email = '{0}'", username), sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Usuario> result = new List<Usuario>();
                if (dr.Read())
                {
                    Usuario usu = new Usuario()
                    {
                        Email = (string)dr["email"],
                        Senha = (string)dr["password"]

                    };
                    result.Add(usu);
                    sqlConn.Close();
                    if (password == usu.Senha)
                        return Ok(usu);
                    else
                        return Unauthorized();
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from usuarios where id = " + id,sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Usuario usu = new Usuario()
                    {
                        Email = (string)dr["email"],
                        Senha = (string)dr["password"]
                    };
                    sqlConn.Close();
                    return usu;
                }
                else
                    return BadRequest();
                
                
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public string Post(Usuario usuario)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO usuarios([email],[password]) values ('{0}','{1}') ",usuario.Email, usuario.Senha),sqlConn);

                int result = cmd.ExecuteNonQuery();

                sqlConn.Close();

                if (result == 0)
                    return "{\"resposta\" : \"Ocorreu um erro ao inserir os dados.\"}";

                return "{\"resposta\" : \"Registro incluído com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"resposta\" : \"Ocorreu um erro.\"}";
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public string Put(int id, Usuario usuario)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE usuarios SET [email] = '{0}' ,[password] = '{1}' where id = {2}", usuario.Email, usuario.Senha, id),sqlConn);


                int result = cmd.ExecuteNonQuery();

                sqlConn.Close();

                if (result == 0)
                    return "{\"resposta\" : \"Ocorreu um erro ao atualizar os dados.\"}";

                return "{\"resposta\" : \"Registro atualizado com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"resposta\" : \"Ocorreu um erro.\"}";
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("Delete from usuarios WHERE id = {0}", id),sqlConn);


                int result = cmd.ExecuteNonQuery();

                sqlConn.Close();

                if (result == 0)
                    return "{\"resposta\" : \"Ocorreu um erro ao excluir o registro.\"}";

                return "{\"resposta\" : \"Registro excluído com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"resposta\" : \"Ocorreu um erro.\"}";
            }

        }
    }
}
