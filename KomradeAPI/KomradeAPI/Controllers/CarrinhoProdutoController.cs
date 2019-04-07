using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GuiaAlimentarAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuiaAlimentarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrinhoProdutoController : Controller
    {

        public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";

        // GET: api/<controller>S
        [HttpGet]
        public ActionResult<List<CarrinhoProduto>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from carrinho_produtos", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<CarrinhoProduto> result = new List<CarrinhoProduto>();

                while (dr.Read())
                {
                    CarrinhoProduto cp = new CarrinhoProduto()
                    {
                        CarrinhoId = (int)dr["carrinho_id"],
                        ProdutoId = (int)dr["produto_id"],
                        Quantidade = (int)dr["quantidade"]
                    };
                    result.Add(cp);

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
        public ActionResult<CarrinhoProduto> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from carrinho_produtos where id = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    CarrinhoProduto cp = new CarrinhoProduto()
                    {
                        CarrinhoId = (int)dr["carrinho_id"],
                        ProdutoId = (int)dr["produto_id"],
                        Quantidade = (int)dr["quantidade"]
                    };
                    sqlConn.Close();

                    return cp;
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
        public string Post(CarrinhoProduto carrinhoProduto)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO carrinho_produtos(carrinho_id, produto_id, quantidade) values ('{0}','{1}','{2}') "
                    , carrinhoProduto.CarrinhoId, carrinhoProduto.ProdutoId, carrinhoProduto.Quantidade), sqlConn);

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

        [HttpPut("{id}")]
        public string Put(int id, CarrinhoProduto carrinhoProduto)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE carrinho_produtos SET quantidade = '{0}' WHERE id = {1}", carrinhoProduto.Quantidade, id), sqlConn);

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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from carrinho_produtos WHERE id = {0}", id), sqlConn);


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
