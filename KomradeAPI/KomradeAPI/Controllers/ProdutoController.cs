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

    public class ProdutoController : Controller
    {
       public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from produtos",sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Produto> result = new List<Produto>();

                while (dr.Read())
                {
                    Produto pd = new Produto()
                    {
                        Id = (int)dr["id"],
                        Nome = (string)dr["nome"],
                        Descricao = (string)dr["descricao"],
                        Valor = (int)dr["valor"],
                        Plataforma = (string)dr["plataforma"],
                        Genero = (string)dr["genero"],
                        Estudio = (string)dr["estudio"],
                        IdadeRecomendada = (int)dr["idade_recomendada"],
                        DataLançamento = (DateTime)dr["data_lancamento"],
                        ResoluçãoMaxima = (string)dr["resolucao_maxima"],
                        Tipo = (string)dr["tipo"]

                    };
                    result.Add(pd);

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
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from produtos where id = " + id,sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Produto pd = new Produto()
                    {
                        Id = (int)dr["id"],
                        Nome = (string)dr["nome"],
                        Descricao = (string)dr["descricao"],
                        Valor = (int)dr["valor"],
                        Plataforma = (string)dr["plataforma"],
                        Genero = (string)dr["genero"],
                        Estudio = (string)dr["estudio"],
                        IdadeRecomendada = (int)dr["idade_recomendada"],
                        DataLançamento = (DateTime)dr["data_lancamento"],
                        ResoluçãoMaxima = (string)dr["resolucao_maxima"],
                        Tipo = (string)dr["tipo"]
                    };
                    sqlConn.Close();
                    return pd;
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
        public string Post(Produto produto)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO produtos(nome, descricao, valor, plataforma, genero, estudio, idade_recomendada, data_lancamento, resolucao_maxima, tipo)" +
                    " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}') ", produto.Nome, produto.Descricao, produto.Valor, produto.Plataforma, produto.Genero, produto.Estudio, produto.IdadeRecomendada, produto.DataLançamento, produto.ResoluçãoMaxima, produto.Tipo),sqlConn);

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
        public string Put(int id, Produto produto)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE produtos SET nome = '{0}', descricao = '{1}',valor = '{2}',plataforma = '{3}',genero = '{4}',estudio = '{5}' ,idade_recomendada = '{6}',data_lancamento = '{7}',resolucao_maxima = '{8}',tipo = '{9}' WHERE id = {10}"
                    , produto.Nome, produto.Descricao, produto.Valor, produto.Plataforma, produto.Genero, produto.Estudio, produto.IdadeRecomendada, produto.DataLançamento, produto.ResoluçãoMaxima, produto.Tipo, id),sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from produtos WHERE id = {0}", id),sqlConn);


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
