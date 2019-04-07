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

    public class ItemPedidoController : Controller
    {
       public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";

        // GET: api/<controller>S
        [HttpGet]
        public ActionResult<List<ItemPedido>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from item_pedidos", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<ItemPedido> result = new List<ItemPedido>();

                while (dr.Read())
                {
                    ItemPedido ip = new ItemPedido()
                    {
                        PedidoId = (int)dr["pedido_id"],
                        ProdutoId = (int)dr["produto_id"],
                        Quantidade = (int)dr["quantidade"]
                    };
                    result.Add(ip);

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
        public ActionResult<ItemPedido> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from item_pedidos where id = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ItemPedido ip = new ItemPedido()
                    {
                        PedidoId = (int)dr["pedido_id"],
                        ProdutoId = (int)dr["produto_id"],
                        Quantidade = (int)dr["quantidade"]
                    };
                    sqlConn.Close();

                    return ip;
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
        public string Post(ItemPedido itemPedido)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO item_pedidos(pedido_id, produto_id, quantidade) values ('{0}','{1}','{2}') "
                    , itemPedido.PedidoId, itemPedido.ProdutoId, itemPedido.Quantidade), sqlConn);

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

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("Delete from item_pedidos WHERE id = {0}", id), sqlConn);


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
