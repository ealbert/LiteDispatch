using System.Linq;

namespace LiteDispatch.EF.Context.Migrations
{
  using AutoMapper;
  using DbContext;
  using Domain.Entities;
  using Domain.Mappings;
  using Domain.Models;
  using Domain.TransManager;
  using TransManager;

  class DomainDbManager
  {
    public IModelCreator ModelCreator { get; set; }
    public DomainDbManager(LiteDispatchDbContext context)
    {
      ModelCreator = context.ModelCreator;
    }

    public void Install()
    {
      EntityToModel.Install();
      ModelToEntity.Install();
      var factory = new TransManagerFactoryEF(ModelCreator);
      using (var transManager = factory.CreateManager())
      {
        transManager.ExecuteCommand(locator =>
        {
          var haulier = locator.FindAll<Haulier>().SingleOrDefault(h => h.Name == "BlueWhale");
          if (haulier != null)
          {
            return Mapper.Map<Haulier, HaulierModel>(haulier);
          }
          haulier = Haulier.Create(locator, new HaulierModel { Name = "BlueWhale" });
          return Mapper.Map<Haulier, HaulierModel>(haulier);
        });
      }
    }
  }
}
