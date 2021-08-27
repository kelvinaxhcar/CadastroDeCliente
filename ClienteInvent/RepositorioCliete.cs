using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ClienteInvent
{
    public class RepositorioCliete
    {
        public SqlConnection Open()
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
                    var query = "INSERT INTO Cliente(nome," +
                                "Cpf," +
                                "Municipio," +
                                "Rua," +
                                "Cep," +
                                "Numero," +
                                "Bairro," +
                                "Estado," +
                                "Email," +
                                "Telefone)" +
                                "VALUES ( @parametroNome," +
                                "@parametroCpf, " +
                                "@parametroMunicipio," +
                                "@parametroRua," +
                                "@parametroCep," +
                                "@parametroNumero," +
                                "@parametroBairro," +
                                "@parametroEstado," +
                                "@parametroEmail," +
                                "@parametroTelefone)";

                    command.CommandText = query;

                    SetParametroSql(cliente, command);

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
                    var query = "DELETE FROM Cliente WHERE Id_Cliente = @parametroId";
                    command.CommandText = query;

                    AddParam(command, "parametroId", cliente.id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Cliente BuscarPeloId(int id)
        {
            var cliente = new Cliente();
            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "SELECT * FROM Cliente WHERE  Id_Cliente = @parametroId";

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
            Console.Write(cliente.municipio);

            using (var dbConnection = Open())
            {
                using (var command = dbConnection.CreateCommand())
                {
                    var query = "UPDATE Cliente SET Nome = @parametroNome," +
                                "Cpf = @parametroCpf," +
                                " Municipio = @parametroMunicipio," +
                                " Rua = @parametroRua," +
                                " Cep = @parametroCep," +
                                " Numero = @parametroNumero," +
                                " Bairro = @parametroBairro, " +
                                "Estado = @parametroEstado, " +
                                "Email = @parametroEmail, Telefone = @parametroTelefone WHERE Id_Cliente = @parametroId";

                    command.CommandText = query;

                    SetParametroSql(cliente, command);

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
                id = dbReader.IsDBNull("Id_Cliente") ? default : dbReader.GetInt32("Id_Cliente"),
                nome = dbReader.IsDBNull("Nome") ? default : dbReader.GetString("Nome"),
                cpf = dbReader.IsDBNull("Cpf") ? default : dbReader.GetString("Cpf"),
                municipio = dbReader.IsDBNull("Municipio") ? default : dbReader.GetString("Municipio"),
                rua = dbReader.IsDBNull("Rua") ? default : dbReader.GetString("Rua"),
                cep = dbReader.IsDBNull("Cep") ? default : dbReader.GetString("Cep"),
                numero = dbReader.IsDBNull("Numero") ? default : dbReader.GetInt32("Numero"),
                bairro = dbReader.IsDBNull("Bairro") ? default : dbReader.GetString("Bairro"),
                estado = dbReader.IsDBNull("Estado") ? default : dbReader.GetString("Estado"),
                email = dbReader.IsDBNull("Email") ? default : dbReader.GetString("Email"),
                telefone = dbReader.IsDBNull("Telefone") ? default : dbReader.GetString("Telefone"),
            };
            return cliente;
        }

        private void SetParametroSql(Cliente cliente, SqlCommand command)
        {
            AddParam(command, "parametroId", cliente.id);
            AddParam(command, "parametroNome", cliente.nome);
            AddParam(command, "parametroCpf", cliente.cpf);
            AddParam(command, "parametroMunicipio", cliente.municipio);
            AddParam(command, "parametroRua", cliente.rua);
            AddParam(command, "parametroCep", cliente.cep);
            AddParam(command, "parametroNumero", cliente.numero);
            AddParam(command, "parametroBairro", cliente.bairro);
            AddParam(command, "parametroEstado", cliente.estado);
            AddParam(command, "parametroEmail", cliente.email);
            AddParam(command, "parametroTelefone", cliente.telefone);
        }
    }
}