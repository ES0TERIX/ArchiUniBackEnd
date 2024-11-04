namespace UniversiteDomain.Exceptions.UeExceptions;

[Serializable]
public class IntituleInfThreeException : Exception
{
    public IntituleInfThreeException() : base() { }
    public IntituleInfThreeException(string message) : base(message) { }
    public IntituleInfThreeException(string message, Exception inner) : base(message, inner) { }
}