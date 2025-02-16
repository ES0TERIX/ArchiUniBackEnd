namespace UniversiteDomain.Exceptions.EtudiantExceptions;

public class NomEtudiantIncorrectException : Exception
{
    public NomEtudiantIncorrectException() : base() { }
    public NomEtudiantIncorrectException(string message) : base(message) { }
    public NomEtudiantIncorrectException(string message, Exception innerException) : base(message, innerException) { }
}