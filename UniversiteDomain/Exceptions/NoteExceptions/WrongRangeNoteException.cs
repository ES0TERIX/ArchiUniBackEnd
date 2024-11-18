namespace UniversiteDomain.Exceptions.NoteExceptions;

public class WrongRangeNoteException : Exception
{
    public WrongRangeNoteException() : base() { }
    public WrongRangeNoteException(string message) : base(message) { }
    public WrongRangeNoteException(string message, Exception inner) : base(message, inner) { }
}