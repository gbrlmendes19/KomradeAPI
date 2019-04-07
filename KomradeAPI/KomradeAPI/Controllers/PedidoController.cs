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

    public class PedidoController : Controller
    {
                public string connectionString = "Server=tcp:guiaalimentar.database.windows.net;Database=Komrade;User ID =gabriel@guiaalimentar.database.windows.net;Password=Guia123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Pedido>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from pedidos",sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Pedido> result = new List<Pedido>();

                while (dr.Read())
                {
                    Pedido pe = new Pedido()
                    {
                        Total = (int)dr["total"],
                        FormaPagamento = (int)dr["forma_pagamento"],
                        Data = (DateTime)dr["data"],
                        EndereçoId = (int)dr["endereco_id"]

                    };
                    result.Add(pe);

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
        public ActionResult<Pedido> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from pedidos where id = " + id,sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Pedido pe = new Pedido()
                    {
                        Total = (int)dr["total"],
                        FormaPagamento = (int)dr["forma_pagamento"],
                        Data = (DateTime)dr["data"],
                        EndereçoId = (int)dr["endereco_id"]
                    };
                    sqlConn.Close();
                    return pe;
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
        public string Post(Pedido pedido)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO pedidos (total, forma_pagamento, data, endereco_id) values ('{0}','{1}','{2}','{3}') "
                    , pedido.Total, pedido.FormaPagamento, pedido.Data, pedido.EndereçoId),sqlConn);

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
        public string Put(int id, Pedido pedido)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE dbo.pedidos SET total = '{0}', forma_pagamento = '{1}', data = '{2}', endereco_id = '{3}' WHERE id = {4}"
                    , pedido.Total, pedido.FormaPagamento, pedido.Data, pedido.EndereçoId, id),sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from pedidos WHERE id = {0}", id),sqlConn);


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
