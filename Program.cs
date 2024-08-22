using System;
using System.Collections.Generic;
using System.Linq;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataAdmissao { get; set; }
    public string Tipo { get; set; }
}

public class Empresa
{
    private List<Funcionario> funcionarios;

    public Empresa()
    {
        funcionarios = new List<Funcionario>();
    }

    public string ObterPorNome(string nome)
    {
        var funcionario = funcionarios.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (funcionario == null)
        {
            return "Funcionário não encontrado.";
        }
        return $"Funcionário encontrado: {funcionario.Nome}, Salário: {funcionario.Salario:C}, Data de Admissão: {funcionario.DataAdmissao.ToShortDateString()}";
    }

    public void ObterFuncionariosRecentes()
    {
        funcionarios.RemoveAll(f => f.Id < 4);
        var funcionariosOrdenados = funcionarios.OrderByDescending(f => f.Salario).ToList();

        Console.WriteLine("Funcionários recentes:");
        foreach (var funcionario in funcionariosOrdenados)
        {
            Console.WriteLine($"Nome: {funcionario.Nome}, Salário: {funcionario.Salario:C}");
        }
    }

    public void ObterEstatisticas()
    {
        int quantidade = funcionarios.Count;
        decimal somatorioSalarios = funcionarios.Sum(f => f.Salario);

        Console.WriteLine($"Quantidade de Funcionários: {quantidade}");
        Console.WriteLine($"Somatório dos Salários: {somatorioSalarios:C}");
    }

    public string ValidarSalarioAdmissao(Funcionario funcionario)
    {
        if (funcionario.Salario == 0)
        {
            return "Salário não pode ser 0.";
        }
        if (funcionario.DataAdmissao < DateTime.Now.Date)
        {
            return "Data de admissão não pode ser anterior à data atual.";
        }

        return "OK"; // Indica que as validações foram bem-sucedidas
    }

    public string ValidarNome(Funcionario funcionario)
    {
        if (funcionario.Nome.Length < 2)
        {
            return "Nome do funcionário deve ter pelo menos 2 caracteres.";
        }

        return "OK"; // Indica que as validações foram bem-sucedidas
    }

    public void AdicionarFuncionario(Funcionario funcionario)
    {
        funcionarios.Add(funcionario);
        Console.WriteLine("Funcionário adicionado com sucesso.");
    }

    public void ObterPorTipo(string tipo)
    {
        var funcionariosFiltrados = funcionarios.Where(f => f.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase)).ToList();

        if (funcionariosFiltrados.Any())
        {
            Console.WriteLine($"Funcionários do tipo {tipo}:");
            foreach (var funcionario in funcionariosFiltrados)
            {
                Console.WriteLine($"Nome: {funcionario.Nome}, Salário: {funcionario.Salario:C}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum funcionário encontrado para o tipo informado.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Empresa empresa = new Empresa();

        while (true)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Obter funcionário por nome");
            Console.WriteLine("2. Obter funcionários recentes");
            Console.WriteLine("3. Obter estatísticas");
            Console.WriteLine("4. Validar e adicionar novo funcionário");
            Console.WriteLine("5. Obter funcionários por tipo");
            Console.WriteLine("6. Sair");
            Console.Write("Opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o nome do funcionário: ");
                    string nomeBusca = Console.ReadLine();
                    Console.WriteLine(empresa.ObterPorNome(nomeBusca));
                    break;

                case "2":
                    empresa.ObterFuncionariosRecentes();
                    break;

                case "3":
                    empresa.ObterEstatisticas();
                    break;

                case "4":
                    Console.Write("Digite o nome do funcionário: ");
                    string nomeNovo = Console.ReadLine();

                    if (nomeNovo.Length < 2)
                    {
                        Console.WriteLine("Nome do funcionário deve ter pelo menos 2 caracteres.");
                        break;
                    }

                    Console.Write("Digite o salário do funcionário: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal salarioNovo) || salarioNovo <= 0)
                    {
                        Console.WriteLine("Salário inválido. Deve ser um valor numérico maior que 0.");
                        break;
                    }

                    Console.Write("Digite a data de admissão (formato: dd/mm/aaaa): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataAdmissaoNova) || dataAdmissaoNova < DateTime.Now.Date)
                    {
                        Console.WriteLine("Data de admissão inválida. Deve ser uma data válida e não pode ser anterior à data atual.");
                        break;
                    }

                    Console.Write("Digite o tipo do funcionário: ");
                    string tipoNovo = Console.ReadLine();

                    Funcionario novoFuncionario = new Funcionario
                    {
                        Nome = nomeNovo,
                        Salario = salarioNovo,
                        DataAdmissao = dataAdmissaoNova,
                        Tipo = tipoNovo
                    };

                    empresa.AdicionarFuncionario(novoFuncionario);
                    break;

                case "5":
                    Console.Write("Digite o tipo de funcionário a buscar: ");
                    string tipoBusca = Console.ReadLine();
                    empresa.ObterPorTipo(tipoBusca);
                    break;

                case "6":
                    return;  // Encerra o programa

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine(); // Linha em branco para separar as interações
        }
    }
}
