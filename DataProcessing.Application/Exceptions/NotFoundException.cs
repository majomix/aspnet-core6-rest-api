namespace DataProcessing.Application.Exceptions
{
    /// <summary>
    /// Application exception indicating requested object was not found.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            : base($"Item with ID {key} was not found for method {name}.")
        { }
    }
}
