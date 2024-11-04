using System.Linq.Expressions;
using Moq;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.UseCases.UeUseCases.Create;

namespace UniversiteDomainUnitTests;

public class UeUnitTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateUeUseCase()
    {
        long ueId = 1;
        string numeroUe = "PRO23";
        string intitule = "BoringPorteFolio";
        
        Ue ueSansId = new Ue{NumeroUe= numeroUe, Intitule = intitule};
        
        var mock = new Mock<IUeRepository>();
        
        var reponseFindByCondition = new List<Ue>();
        
        mock.Setup(repo=>repo.FindByConditionAsync(It.IsAny<Expression<Func<Ue, bool>>>())).ReturnsAsync(reponseFindByCondition);
        
        Ue ueCree =new Ue{Id= ueId, NumeroUe = numeroUe, Intitule = intitule};
        mock.Setup(repoUe=>repoUe.CreateAsync(ueSansId)).ReturnsAsync(ueCree);
        
        var fauxUeRepository = mock.Object;
        
        CreateUeUseCase useCase=new CreateUeUseCase(fauxUeRepository);
        // Appel du use case
        var ueTeste=await useCase.ExecuteAsync(ueSansId);
        
        // Vérification du résultat
        Assert.That(ueTeste.Id, Is.EqualTo(ueCree.Id));
        Assert.That(ueTeste.NumeroUe, Is.EqualTo(ueCree.NumeroUe));
        Assert.That(ueTeste.Intitule, Is.EqualTo(ueCree.Intitule));
    }
}