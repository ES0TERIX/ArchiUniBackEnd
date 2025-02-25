﻿using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;

namespace UniversiteDomain.UseCases.UeUseCases.Get;

public class GetAllUesUseCase(IRepositoryFactory repositoryFactory)
{
    
    public async Task<List<Ue>> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Ue> ues = await repositoryFactory.UeRepository().FindAllAsync();
        return ues;
    }
    
    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IUeRepository ueRepository=repositoryFactory.UeRepository();
        ArgumentNullException.ThrowIfNull(ueRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Scolarite) || role.Equals(Roles.Responsable);
    }
    
}