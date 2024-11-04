using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.UeExceptions;

namespace UniversiteDomain.UseCases.UeUseCases.Create;

public class CreateUeUseCase(IUeRepository ueRepository)
{
    public async Task<Ue> ExecuteAsync(string numeroUe, string intitule)
    {
        var ue = new Ue{NumeroUe = numeroUe, Intitule = intitule};
        return await ExecuteAsync(ue);
    }
    
    public async Task<Ue> ExecuteAsync(Ue ue)
    {
        await CheckBusinessRules(ue);
        Ue ueCreated = await ueRepository.CreateAsync(ue);
        ueRepository.SaveChangesAsync().Wait();
        return ueCreated;
    }

    private async Task CheckBusinessRules(Ue ue)
    {
        ArgumentNullException.ThrowIfNull(ue);
        ArgumentNullException.ThrowIfNull(ue.NumeroUe);
        ArgumentNullException.ThrowIfNull(ue.Intitule);
        ArgumentNullException.ThrowIfNull(ueRepository);
        
        // On recherche un ue avec le même nom de ue
        List<Ue> ueWithDuplicatedNames  = await ueRepository.FindByConditionAsync(e=>e.NumeroUe.Equals(ue.NumeroUe));

        // Si un parcours avec le même nom existe déjà, on lève une exception personnalisée
        if (ueWithDuplicatedNames.Any())
            throw new DuplicateNumUeException(ue.NumeroUe + " - ce nom de ue est déjà affecté à un ue");
        
        List<Ue> ueSmalerThanThree = await ueRepository.FindByConditionAsync(e => e.Intitule.Length >= 3);
        
        if (ueWithDuplicatedNames.Any())
            throw new IntituleInfThreeException(ue.Intitule + " - cet intitule de ue est plus petit que 3");
        
    }
    
}