using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ClienteInvent
{
    public class RepositorioCliete
    {
        public  SqlConnection Open()
        {
            var dbCoonection = new SqlConnection(
                "Server=147.182.248.84;Database=ClienteInvent;UID=sa;Password=7878w1zKl");
            
         dbCoonection.Open();

         if (dbCoonection.State != ConnectionState.Open)
         {
             throw new Exception("Nao foi possivel se conectar");

         }

         return dbCoonection;
        }

        public List<Cliente> ListarTodos()
        {
            var listDeClientes = new List<Cliente>();

            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cliente";

                    var dbReader = command.ExecuteReader();

                    while (dbReader.Read())
                    {
                        listDeClientes.Add(PreencherCliente(dbReader));
                    }
                }
            }
            return listDeClientes;
        }

        public void Inserir(Cliente cliente)
        {
            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "INSERT INTO Cliente(nome, cpf) VALUES ( @parametroNome,@oarametroCpf)";
                    
                    command.CommandText = query;
                    
                    AddParam(command, "parametroNome", cliente.nome);
                    AddParam(command, "oarametroCpf", cliente.cpf);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        

        public void Deletar(Cliente cliente)
        {
            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "DELETE FROM Cliente WHERE IdCliente = @parametroId";
                    command.CommandText = query;
                    
                    AddParam(command, "parametroId", cliente.id);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public Cliente BuscarPeloId(int id)
        {
            var cliente = new Cliente();
            using (var dbConnection  = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "SELECT * FROM Cliente WHERE  IdCliente = @parametroId";

                    command.CommandText = query;
                    
                    AddParam(command, "parametroId", id);
                    var dbReader = command.ExecuteReader();
                    
                    while (dbReader.Read())
                    {
                        var clienteBd = PreencherCliente(dbReader);
                        cliente = clienteBd;
                    }
                }
            }
            return cliente;
        }
        
        public void Update(Cliente cliente)
        {
            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "UPDATE Cliente SET Nome = @parametroNome, Cpf = @poarametroCpf WHERE IdCliente = @parametroId";
                    
                    command.CommandText = query;
                    
                    AddParam(command, "parametroNome", cliente.nome);
                    AddParam(command, "poarametroCpf", cliente.cpf);
                    AddParam(command, "parametroId", cliente.id);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        
        private void AddParam(SqlCommand command, string name, object value)
        {
            var parmName = command.CreateParameter();
            parmName.ParameterName = name;
            parmName.Value = value;
            command.Parameters.Add(parmName);
        }
        
        private static Cliente PreencherCliente(SqlDataReader dbReader)
        {
            var cliente = new Cliente
            {
                id = dbReader.IsDBNull("IdCliente") ? default : dbReader.GetInt32("IdCliente"),
                nome = dbReader.IsDBNull("Nome") ? default : dbReader.GetString("Nome"),
                cpf = dbReader.IsDBNull("Cpf") ? default : dbReader.GetString("Cpf"),
            };
            return cliente;
        }
    }
}