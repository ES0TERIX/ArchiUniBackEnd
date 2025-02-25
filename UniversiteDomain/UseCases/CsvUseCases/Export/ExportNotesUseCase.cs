﻿using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Dtos;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.ParcoursExceptions;

namespace UniversiteDomain.UseCases.CsvUseCases.Export;

public class ExportNotesUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<string> ExecuteAsync(long idUe)
    {
        await CheckBusinessRules();
        var ue = await repositoryFactory.UeRepository().FindAsync(idUe);
        if (ue == null)
        {
            throw new ArgumentException($"L'UE avec l'ID {idUe} est introuvable.");
        }

        List<Parcours> parcoursAssocies = await repositoryFactory.ParcoursRepository()
            .FindByConditionAsync(p => p.UesEnseignees.Any(u => u.Id == idUe));

        if (parcoursAssocies == null || !parcoursAssocies.Any())
        {
            throw new ArgumentException($"Aucun parcours n'est associé à l'UE {ue.Id}.");
        }

        var etudiantsAssocies = parcoursAssocies
            .SelectMany(p => p.Inscrits)
            .Distinct()
            .ToList();
        

        if (!etudiantsAssocies.Any())
        {
            throw new ArgumentException($"Aucun étudiant n'est inscrit pour l'UE {ue.Intitule}.");
        }

        var notes = await repositoryFactory.NoteRepository()
            .FindByConditionAsync(n => n.UeId == idUe);

        ue.EnseigneeDans = parcoursAssocies;
        ue.Notes = notes;

        return repositoryFactory.CsvDataAdapterRepository().ExportUeWithNotesToCsv(ue);
    }
    
    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IUeRepository ueRepository = repositoryFactory.UeRepository();
        ArgumentNullException.ThrowIfNull(ueRepository);
        INoteRepository noteRepository = repositoryFactory.NoteRepository();
        ArgumentNullException.ThrowIfNull(noteRepository);
        ICsvDataAdapterRepository csvDataAdapterRepository = repositoryFactory.CsvDataAdapterRepository();
        ArgumentNullException.ThrowIfNull(csvDataAdapterRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Scolarite);
    }
}