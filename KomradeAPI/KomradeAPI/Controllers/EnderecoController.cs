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
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : Controller
    {

      public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Endereco>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from enderecos",sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Endereco> result = new List<Endereco>();

                while (dr.Read())
                {
                    Endereco en = new Endereco()
                    {
                        CEP = (string)dr["cep"],
                        Estado = (string)dr["estado"],
                        Cidade = (string)dr["cidade"],
                        Bairro = (string)dr["bairro"],
                        Logradouro= (string)dr["logradouro"],
                        Complemento = (string)dr["complemento"],
                        Numero = (string)dr["numero"],
                        Responsavel = (string)dr["responsavel"],
                        UsuarioId = (int)dr["usuario_id"]

                    };
                    result.Add(en);

                }

                sqlConn.Close();
                return result;

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Endereco> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from enderecos where id = " + id,sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Endereco en = new Endereco()
                    {
                        CEP = (string)dr["cep"],
                        Estado = (string)dr["estado"],
                        Cidade = (string)dr["cidade"],
                        Bairro = (string)dr["bairro"],
                        Logradouro = (string)dr["logradouro"],
                        Complemento = (string)dr["complemento"],
                        Numero = (string)dr["numero"],
                        Responsavel = (string)dr["responsavel"],
                        UsuarioId = (int)dr["usuario_id"]
                    };
                    sqlConn.Close();
                    return en;
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public string Post(Endereco endereco)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO enderecos(cep, estado, cidade, bairro, logradouro, complemento, numero, responsavel, usuario_id) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}') "
                    , endereco.CEP, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.Logradouro, endereco.Complemento, endereco.Numero, endereco.Responsavel, endereco.UsuarioId),sqlConn);

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
        public string Put(int id, Endereco endereco)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE enderecos SET cep = '{0}', estado = '{1}',cidade = '{2}',bairro = '{3}',logradouro = '{4}',complemento = '{5}',numero = '{6}',responsavel = '{7}',usuario_id = '{8}' WHERE id = {9}"
                    , endereco.CEP, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.Logradouro, endereco.Complemento, endereco.Numero, endereco.Responsavel, endereco.UsuarioId, id),sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from enderecos WHERE id = {0}", id),sqlConn);


                int result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"resposta\" : \"Ocorreu um erro ao excluir o registro.\"}";

                cmd = new SqlCommand(string.Format("Delete from Locais WHERE id = {0}", id),sqlConn);


                result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"resposta\" : \"Ocorreu um erro ao excluir o registro.\"}";

               

                sqlConn.Close();

                return "{\"resposta\" : \"Registro excluído com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"resposta\" : \"Ocorreu um erro.\"}";
            }

        }
    }
}

