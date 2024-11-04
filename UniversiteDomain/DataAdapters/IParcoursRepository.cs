using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapters;

public interface IParcoursRepository : IRepository<Parcours>
{
    Task<Parcours> AddEtudiantAsync(Parcours parcours, Etudiant etudiant);
    Task<Parcours> AddEtudiantAsync(long idParcours, long idEtudiant);
    Task<Parcours> AddEtudiantAsync(Parcours ? parcours, List<Etudiant> etudiants);
    Task<Parcours> AddEtudiantAsync(long idParcours, long[] idEtudiants);
    
    Task<Parcours> AddUeAsync(Parcours parcours, Ue ue);
    Task<Parcours> AddUeAsync(long idParcours, long idUe);
    Task<Parcours> AddEtudiantAsync(Ue ? ue, List<Ue> ues);
    Task<Parcours> AddUeAsync(long idParcours, long[] idUes);
    
    
    
    
    
    
}