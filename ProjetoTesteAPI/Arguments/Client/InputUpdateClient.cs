using System.Text.Json.Serialization;

namespace ProjetoTesteAPI.Arguments.Client
{
    [method: JsonConstructor]
    public class InputUpdateClient(string name, string email, string cpf, int phone)
    {
        public string Name { get; } = name;
        public string Email { get; } = email;
        public string CPF { get; } = cpf;
        public int Phone { get; } = phone;
    }
}