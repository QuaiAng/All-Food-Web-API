namespace AllFoodAPI.Core.Exceptions
{
    public class DuplicateException : Exception
    {
        public string Field { get; }

        public DuplicateException(string field, string message) : base(message)
        {
            Field = field;
        }
    }
}
