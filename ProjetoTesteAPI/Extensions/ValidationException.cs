namespace ProjetoTesteAPI.Extensions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
