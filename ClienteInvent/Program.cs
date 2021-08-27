using System;
using System.Diagnostics;
using ConsoleTables;

namespace ClienteInvent
{
    class Program
    {
        private static RepositorioCliete _repositorioCliete = new RepositorioCliete();

        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                 Bem vindo                        ");
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine(" Sistema de cadastro de clientes\n");
            do
            {
            } while (Controle());
        }


        static bool Controle()
        {
            Console.WriteLine("------------- Digite um comando ------------------\n\n" +
                              "Digite um comando e pressione enter. \n\n" +
                              "(I)  Para incluir um novo cliente \n" +
                              "(D)  Para deletar cliente\n" +
                              "(M)  Para mostrar todos os clientes\n" +
                              "(A)  para alterar um cliente\n");
            Console.Write(" -->");
            var comando = Console.ReadLine();
            Console.Clear();

            var clienteModel = new Cliente();

            switch (comando.ToUpper())
            {
                case "I":
                    InserirCliente(clienteModel);
                    MostrarTabele();
                    return true;

                case "D":
                    MostrarTabele();
                    DeletarCliente(clienteModel);
                    return true;

                case "M":
                    MostrarTabele();
                    return true;
                case "A":
                    
                    AlterarCliente(clienteModel);
                    return true;
                default:
                    return true;
            }
        }

        private static void AlterarCliente(Cliente clienteModel)
        {
            MostrarTabele();
            Console.Write("Digite um Id referente ao cliete que deseja alterar: \n");
            clienteModel.id = Int32.Parse(Console.ReadLine());
            Cliente clienteDoBanco = _repositorioCliete.BuscarPeloId(clienteModel.id);
            Console.WriteLine("Digite um comando referente ao campo que deseja altarar: \n" +
                              $"(1) Para Alterar o Nome: {clienteDoBanco.nome}\n" +
                              $"(2) Para Alterar o CPF:  {clienteDoBanco.cpf}");
            Console.WriteLine();
            Console.Write(" -->");
            var comandoCampo = Int32.Parse(Console.ReadLine());


            Cliente clienteEsitado = EditarCampos(comandoCampo, clienteDoBanco);


            _repositorioCliete.Update(clienteEsitado);
            Console.Clear();
            MostrarTabele();
        }

        public static void InserirCliente(Cliente clienteModel)
        {
            Console.WriteLine("Didite o nome do cliente e pressione enter");
            Console.Write( "-->");
            clienteModel.nome = Console.ReadLine();
            Console.WriteLine("Didite o CPF do cliente e pressione enter");
            Console.Write( "-->");
            clienteModel.cpf = Console.ReadLine();

            if (clienteModel.nome != "" && clienteModel.cpf != "")
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(">>>> Registro salvo com sucesso <<<<");
                Console.BackgroundColor = ConsoleColor.Black;

                _repositorioCliete.Inserir(clienteModel);
            }
            else
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(">>>> Erro ao cadastrar! <<<<");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            
        }

        private static void MostrarTabele()
        {
            var table = new ConsoleTable("Id:", "Nome:", "CPF:");
            
            Console.WriteLine("------------- Lista de Clientes ---------------");

            foreach (var item in _repositorioCliete.ListarTodos())
            {
                table.AddRow(item.id, item.nome, item.cpf);
            }
            
            table.Write();
            Console.WriteLine();

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
        }

        private static void DeletarCliente(Cliente clienteModel)
        {
            Console.WriteLine("Didite o ID do cliente e pressione enter para deletar ou \n" +
                              "pressione somente enter para sair");
            Console.Write( "-->");
            var idCliente = Console.ReadLine();

            var idClienteInt = 0;
            if (!int.TryParse(idCliente, out idClienteInt))
            {
                Console.Clear();
                
            }

            clienteModel.id = Int32.Parse(idCliente);
            clienteModel = _repositorioCliete.BuscarPeloId(clienteModel.id);

            if (clienteModel.id == default)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(">>>> Cliente nao existe <<<<");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                _repositorioCliete.Deletar(clienteModel);
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(">>>> Cliente deletado com sucesso <<<<");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            
        }

        static Cliente EditarCampos(int comando, Cliente cliente)
        {
            switch (comando)
            {
                case 1:
                    Console.WriteLine("Digite o novo nome e pressione enter: ");
                    Console.Write( "-->");
                    cliente.nome = Console.ReadLine();
                    return cliente;

                case 2:
                    Console.WriteLine("Digite o novo CPF e pressione enter: ");
                    Console.Write( "-->");
                    cliente.cpf = Console.ReadLine();
                    return cliente;

                default:
                    return cliente;
            }
        }
    }
}