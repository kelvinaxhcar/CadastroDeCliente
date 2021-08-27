using System;
using ConsoleTables;

namespace ClienteInvent
{
    class Program
    {
        private static RepositorioCliete _repositorioCliete = new RepositorioCliete();

        private static void Main(string[] args)
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
                              "(A)  para alterar um cliente\n"+
                              "(");
            Console.Write(" --> ");
            var comando = Console.ReadLine();
            Console.Clear();

            switch (comando.ToUpper())
            {
                case "I":
                    InserirCliente();
                    MostrarTabele();
                    return true;

                case "D":
                    MostrarTabele();
                    DeletarCliente();
                    return true;

                case "M":
                    MostrarTabele();
                    DetalharCliente();
                    return true;
                
                case "A":
                    AlterarCliente();
                    return true;
                
                default:
                    return true;
            }
        }

        private static void AlterarCliente()
        {
            var clienteModel = new Cliente();
            MostrarTabele();
            Console.Write("Digite um Id referente ao cliete que deseja alterar: \n");
            Console.Write(" --> ");
            clienteModel.id = Int32.Parse(Console.ReadLine() ?? string.Empty);
            Cliente clienteDoBanco = _repositorioCliete.BuscarPeloId(clienteModel.id);
            Console.WriteLine("Digite um comando referente ao campo que deseja altarar: \n" +
                              $"(1) Para Alterar o Nome: {clienteDoBanco.nome}\n" +
                              $"(2) Para Alterar o CPF:  {clienteDoBanco.cpf}");
            Console.WriteLine();
            Console.Write(" --> ");
            var comandoCampo = Int32.Parse(Console.ReadLine());

            Cliente clienteEditado = EditarCampos(comandoCampo, clienteDoBanco);

            _repositorioCliete.Update(clienteEditado);
            Console.Clear();
            MostrarTabele();
        }

        public static void InserirCliente()
        {
            Cliente clienteModel = new Cliente();
            Console.WriteLine("Didite o nome do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.nome = Console.ReadLine();
            
            Console.WriteLine("Didite o CPF do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.cpf = Console.ReadLine();
            
            Console.WriteLine("Didite o Municipio do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.municipio = Console.ReadLine();
            
            Console.WriteLine("Didite o Rua do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.rua = Console.ReadLine();
            
            Console.WriteLine("Didite o CEP do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.cep = Console.ReadLine();
            
            Console.WriteLine("Didite o Numero da casa do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.numero = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("Didite o Bairro do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.bairro = Console.ReadLine();
            
            Console.WriteLine("Didite o Estado do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.estado = Console.ReadLine();
            
            Console.WriteLine("Didite o Email do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.email = Console.ReadLine();
            
            Console.WriteLine("Didite o Numero do cliente e pressione enter");
            Console.Write(" --> ");
            clienteModel.telefone = Console.ReadLine();

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

        private static void DetalharCliente()
        {
            var clienteModel = new Cliente();
            Console.Write("Digite um Id e pressione enter para detalhar");
            Console.Write(" --> ");
            clienteModel.id = Int32.Parse(Console.ReadLine() ?? string.Empty);
            Cliente clienteDoBanco = _repositorioCliete.BuscarPeloId(clienteModel.id);

            var tableDetail = new ConsoleTable("Id:", "Nome:", "CPF:", "Municipio:", "Rua:", "Cep:", "Numero:", "Bairro:",
                "Estado:", "Email:", "Telefone:");

            Console.WriteLine("------------- Cliente ---------------");


            tableDetail.AddRow(clienteDoBanco.id,
                clienteDoBanco.nome,
                clienteDoBanco.cpf,
                clienteDoBanco.municipio,
                clienteDoBanco.rua,
                clienteDoBanco.cep,
                clienteDoBanco.numero,
                clienteDoBanco.bairro,
                clienteDoBanco.estado,
                clienteDoBanco.email,
                clienteDoBanco.telefone);
            
            tableDetail.Write();
            Console.WriteLine();

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
        }

        private static bool DeletarCliente()
        {
            var clienteModel = new Cliente();
            Console.WriteLine("Didite o ID do cliente e pressione enter para deletar ou \n" +
                              "pressione somente enter para sair");
            Console.Write(" --> ");
            var idCliente = Console.ReadLine();

            var idClienteInt = 0;
            if (!int.TryParse(idCliente, out idClienteInt))
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(">>>> Digite um ID valido <<<<");
                Console.BackgroundColor = ConsoleColor.Black;
                return true;
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

            return true;
        }

        static Cliente EditarCampos(int comando, Cliente cliente)
        {
            switch (comando)
            {
                case 1:
                    Console.WriteLine("Digite o novo nome e pressione enter: ");
                    Console.Write(" --> ");
                    cliente.nome = Console.ReadLine();
                    return cliente;

                case 2:
                    Console.WriteLine("Digite o novo CPF e pressione enter: ");
                    Console.Write(" --> ");
                    cliente.cpf = Console.ReadLine();
                    return cliente;

                default:
                    return cliente;
            }
        }
    }
}