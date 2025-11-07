namespace Domain.Exepctions
{
    public sealed class ValidationExecption : Exception
    {
        public IEnumerable<string> Errors { get; }
        public ValidationExecption(IEnumerable<string> errors) : base("Validation faild")
        {
            Errors = errors;
        }
    }
}
