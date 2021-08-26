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
                    command.CommandText = "select*From Cliente";

                    var dbReader = command.ExecuteReader();

                    while (dbReader.Read())
                    {
                        var cliente = new Cliente
                        {
                            id = dbReader.GetInt32("IdCliente"),
                            nome = dbReader.GetString("Nome"),
                            cpf = dbReader.GetString("Cpf")
                        };

                        listDeClientes.Add(cliente);
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
                    var query = "delete From Cliente where IdCliente = @parametroId";
                    command.CommandText = query;
                    
                    AddParam(command, "parametroId", cliente.id);
                    command.ExecuteNonQuery();
                    
                }
            }
        }
        

        public Cliente BuscarPeloId(int id)
        {
            var listDeClientes = new Cliente();
            using (var dbConnection  = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = $"select*from Cliente where  IdCliente = @parametroId";

                    command.CommandText = query;
                    
                    AddParam(command, "parametroId", id);
                    var dbReader = command.ExecuteReader();
                    
                    
                    while (dbReader.Read())
                    {
                        var cliente = new Cliente
                        {
                            id = dbReader.IsDBNull("IdCliente") ? default: dbReader.GetInt32("IdCliente"),
                            nome = dbReader.IsDBNull("Nome") ? default: dbReader.GetString("Nome"),
                            cpf = dbReader.IsDBNull("Cpf") ? default: dbReader.GetString("Cpf"),
                            
                            
                        };

                        listDeClientes = cliente;
                    }

                }
                
            }
            return listDeClientes;
        }
        
        public void Update(Cliente cliente)
        {
            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "update Cliente set Nome = @parametroNome, Cpf = @poarametroCpf where IdCliente = @parametroId";
                    
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
            var parmNome = command.CreateParameter();
            parmNome.ParameterName = name;
            parmNome.Value = value;
            command.Parameters.Add(parmNome);
        }
        
        
    }
}