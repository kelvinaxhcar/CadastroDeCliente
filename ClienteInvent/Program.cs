using System;
using System.Diagnostics;

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

            Console.WriteLine("Sistema de cadastro de clientes\n");
            do
            {
            } while (Controle());
        }


        static bool Controle()
        {
            Console.WriteLine("------------Digite um comando------------\n" +
                              "Digite um comando e pressione enter \n\n" +
                              "(I) Para incluir um novo cliente \n" +
                              "(D) Para deletar cliente\n" +
                              "(M) Para mostrar todos os clientes\n" +
                              "(A) para alterar um cliente");
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
                    
                    MostrarTabele();
                    Console.Write("Digite um Id referente ao cliete que deseja alterar: ");
                    clienteModel.id = Int32.Parse(Console.ReadLine());
                    Console.Write("Digite um comando referente ao campo que deseja altarar: ");
                    var comandoCampo = Int32.Parse(Console.ReadLine());

                    Cliente clienteDoBanco = _repositorioCliete.BuscarPeloId(clienteModel.id);

                    Cliente clienteEsitado = EditarCampos(comandoCampo, clienteDoBanco);


                    _repositorioCliete.Update(clienteEsitado);

                    return true;
                default:
                    return true;
            }
        }

        public static void InserirCliente(Cliente clienteModel)
        {
            Console.WriteLine("Didite o nome do cliente e pressione enter");
            clienteModel.nome = Console.ReadLine();
            Console.WriteLine("Didite o CPF do cliente e pressione enter");
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
            Console.WriteLine("------------Cliente------------");

            foreach (var item in _repositorioCliete.ListarTodos())
            {
                Console.WriteLine($"Id: {item.id} \t Nome: {item.nome} \t Cpf: {item.cpf}");
            }

            Console.WriteLine("-------------------------------");
        }

        private static void DeletarCliente(Cliente clienteModel)
        {
            Console.WriteLine("Didite o ID do cliente e pressione enter para deletar ou \n" +
                              "pressione somente enter para sair");
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
                    Console.Write("Digite a nova informaçao e pressione enter: ");
                    cliente.nome = Console.ReadLine();
                    return cliente;

                case 2:
                    Console.Write("Digite a nova informaçao e pressione enter: ");
                    cliente.cpf = Console.ReadLine();
                    return cliente;

                default:
                    return cliente;
            }
        }
    }
}