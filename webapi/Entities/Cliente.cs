    namespace webapi.NewFolder;

public class Cliente
{
    public Guid Id { get; set; }
    public string CPF { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaSalt { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public bool Ativo { get; set; }
    public string Endereco { get; set; }

    public Cliente(string cpf, string email, string senhaSalt, string senha, bool ativo, string endereco, string telefone , string nome)
    {
        Id = Guid.NewGuid();
        CPF = cpf;
        Email = email;
        SenhaSalt = senhaSalt;
        Senha = senha;
        Ativo = ativo;
        Endereco = endereco;
        Telefone = telefone;
        Nome = nome;
        Endereco = endereco;
    }
}

