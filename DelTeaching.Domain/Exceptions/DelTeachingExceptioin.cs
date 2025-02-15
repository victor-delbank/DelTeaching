namespace DelTeaching.Domain.Exceptions;

public class DelTeachingException : Exception
{
    public string Code { get; }
    public DelTeachingException(string message) : base(message) { }
    public DelTeachingException(string message, string code)
    : base(message)
    {
        Code = code;
    }
    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new DelTeachingException(error);
        }
    }
}