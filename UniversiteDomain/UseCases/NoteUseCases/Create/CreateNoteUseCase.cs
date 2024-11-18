using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.NoteExceptions;

namespace UniversiteDomain.UseCases.NoteUseCases.Create;

public class CreateNoteUseCase(INoteRepository noteRepository)
{
    public async Task<Note> ExecuteAsync(long etudiantId, long ueId, float valeur)
    {
        var note = new Note{Valeur = valeur, EtudiantId = etudiantId, UeId = ueId};
        return await ExecuteAsync(note);
        
        
    }
    public async Task<Note> ExecuteAsync(Note note)
    {
        await CheckBusinessRules(note);
        Note noteCreated = await noteRepository.CreateAsync(note);
        noteRepository.SaveChangesAsync().Wait();
        return noteCreated;
    }

    private async Task CheckBusinessRules(Note note)
    {
        ArgumentNullException.ThrowIfNull(note);
        ArgumentNullException.ThrowIfNull(note.EtudiantId);
        ArgumentNullException.ThrowIfNull(note.UeId);
        ArgumentNullException.ThrowIfNull(note.Valeur);
        
        List<Note> noteDuplicated = await noteRepository.FindByConditionAsync(e=>(e.EtudiantId == note.EtudiantId && e.UeId == note.UeId));
        
        if (noteDuplicated.Any())
            throw new DuplicateNoteException(note.EtudiantId - note.UeId + "est déjà attribuée : " + note.Valeur);
        
        List<Note> noteMauvaiseRange = await noteRepository.FindByConditionAsync(e => e.Valeur > 20 || e.Valeur < 0);
        
        if (noteMauvaiseRange.Any())
            throw new WrongRangeNoteException(note.EtudiantId - note.UeId + "a une mauvaise range : " + note.Valeur);
    }
}