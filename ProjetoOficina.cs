using System;
using System.Collections.Generic;
using System.IO;

namespace SistemaCadastro
{
    public class Cliente
    {
        public string Nome {get; set;}
        public string CPF {get; set;}
        public List<Carro> Carros {get; set;}

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
            Carros = new List<Carro>();
        }

        public void AdicionarCarro(Carro carro)
        {
            Carros.Add(carro);
        }

        public string ObterDadosCliente()
        {
            string dados = $"Cliente:{Nome} | CPF:{CPF}\nCarros:\n";
            foreach (var carro in Carros)
            {
                dados += $"- Modelo:{carro.ObterModelo()}, Status:{carro.ObterStatus()}\n";
            }
            return dados;
        }
    }

    public class Carro
    {
        private string Modelo {get; set;}
        private string Placa {get; set;}
        private string Status {get; set;}

        public Carro(string modelo, string placa, string status)
        {
            Modelo = modelo;
            Placa = placa;
            Status = status;
        }

        public string ObterModelo() => Modelo;
        public string ObterStatus() => Status;

        public void AlterarStatus(string novoStatus)
        {
            Status = novoStatus;
        }
    }
    public class Program
    {
        static string CaminhoCliente = "clientes.txt";

        static void Main(string[] args)
        {
            List<Cliente> clientes = new List<Cliente>();
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("Sistema do cliente e do carro");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Visualizar Clientes");
                Console.WriteLine("3 - Adicionar Carro a Cliente");
                Console.WriteLine("4 - Alterar Status do Carro");
                Console.WriteLine("5 - Verificar e chamar outro serviço");
                Console.WriteLine("6 - Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Digite um numero de 1 a 5!");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        CadastrarCliente(clientes);
                        break;
                    case 2:
                        VisualizarClientes();
                        break;
                    case 3:
                        AdicionarCarro(clientes);
                        break;
                    case 4:
                        AlterarStatusCarro(clientes);
                        break;
                    case 5:
                        ChamarServico(clientes);
                        break;    
                    case 6:
                        Console.WriteLine("Sistema fechado");
                        break;
                    default:
                        Console.WriteLine("Digite um numero de 1 a 5!!!");
                        break;
                }

                if (opcao != 6)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 6);
        }

        static void CadastrarCliente(List<Cliente> clientes)
        {
            Console.Write("Digite o nome do cliente: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cpf))
            {
                var cliente = new Cliente(nome, cpf);
                clientes.Add(cliente);
               
                using (StreamWriter writer = new StreamWriter(CaminhoCliente, true)) // aqui salva o nome e o cpf
                {
                    writer.WriteLine($"{nome};{cpf}");
                }

                Console.WriteLine("Cliente cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Digite seu Nome e CPF.");
            }
        }

        static void VisualizarClientes()
        {
            if (File.Exists(CaminhoCliente))
            {
                Console.WriteLine("Clientes cadastrados:");
                string[] linhas = File.ReadAllLines(CaminhoCliente);
                foreach (var linha in linhas)
                {
                    string[] dados = linha.Split(';');
                    Console.WriteLine($"Nome: {dados[0]}, CPF: {dados[1]}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
        }

        static void AdicionarCarro(List<Cliente> clientes)
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            var cliente = clientes.Find(c => c.CPF == cpf);

            if (cliente != null)
            {
                Console.Write("Digite o modelo do carro: ");
                string modelo = Console.ReadLine();
                Console.Write("Digite a placa do carro: ");
                string placa = Console.ReadLine();
                Console.Write("Digite o status do carro: ");
                string status = Console.ReadLine();

                var carro = new Carro(modelo, placa, status);
                cliente.AdicionarCarro(carro);

                Console.WriteLine("Carro adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado!");
            }
        }

        static void AlterarStatusCarro(List<Cliente> clientes)
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            var cliente = clientes.Find(c => c.CPF == cpf);

            if (cliente != null)
            {
                Console.Write("Digite o modelo do carro para alterar o status: ");
                string modelo = Console.ReadLine();

                var carro = cliente.Carros.Find(c => c.ObterModelo() == modelo);

                if (carro != null)
                {
                    Console.Write("Digite o novo status: ");
                    string novoStatus = Console.ReadLine();
                    carro.AlterarStatus(novoStatus);

                    Console.WriteLine("Status do carro atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Carro não encontrado!");
                }
            }
            else
            {
                Console.WriteLine("Cliente não encontrado!");
            }
        }
        static void ChamarServico(List<Cliente> clientes)
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            var cliente = clientes.Find(c => c.CPF == cpf);

            if (cliente != null)
            {
                Console.Write("Digite o modelo do carro: ");
                string modelo = Console.ReadLine();

                var carro = cliente.Carros.Find(c => c.ObterModelo() == modelo);

                if (carro != null)
                {
                    if(carro.ObterStatus() == "indisponivel.")
                    {
                        Console.WriteLine("Carro indisponivel, prefere:");
                        Console.WriteLine("1 - Chamar uber");
                        Console.WriteLine("2 - Chamar taxi");
                        Console.WriteLine("3 - Chamar guincho");
                        Console.Write("Escolha uma opção. ");

                        if (int.TryParse(Console.ReadLine(), out int opcao))
                        {
                            switch(opcao)
                            {
                                case 1:
                                Console.WriteLine("Guincho á caminho.");
                                break;
                                case 2:
                                Console.WriteLine("Uber à caminho.");
                                break;
                                case 3:
                                Console.WriteLine("Taxi à caminho.");
                                break;
                                default:
                                Console.WriteLine("Digite um numero de 1 a 3!!"); // se o usuario digitar errado
                                break;
                            }
                        }
                        else // se o usuario digitar errado
                        {
                            Console.WriteLine("Digite um numero de 1 a 3!!");
                        }
                    }
                }
                else 
                {
                    Console.WriteLine("Não precisa chamar nenhum serviço.");
                }
            }
            else
            {
                Console.WriteLine("Carro não encontrado!");
            }
        }
    }
}