namespace Domain.Exepctions
{
    public sealed class UnauthorizedExecption : Exception
    {
        public UnauthorizedExecption(string message = "Invalid Email or Password") : base(message)
        {
        }
    }
}
