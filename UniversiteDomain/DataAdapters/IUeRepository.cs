using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapters;

public interface IUeRepository : IRepository<Ue>
{
    Task<Ue> AddEtudiantAsync(Ue ue);
    
    Task<Ue> AddEtudiantAsync(string numeroUe, string intitule);
}
