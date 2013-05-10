namespace LiteDispatch.Domain.Entities
{
  using System.Collections.Generic;
  using System.Data.Entity.ModelConfiguration;
  using System.Linq;
  using Models;
  using Repository;

  public class Haulier
    :EntityBase
  {
    protected Haulier()
    {
      DispatchNoteSet = new HashSet<DispatchNote>();
    }       

    public static Haulier Create(IRepositoryLocator locator, HaulierModel model)
    {
      var instance = new Haulier {Name = model.Name};
      locator.Save(instance);
      return instance;
    }

    #region Persisted Properties
    
    public string Name { get; private set; }
    protected virtual ICollection<DispatchNote> DispatchNoteSet { get; set; }

    #endregion
    #region Public Methods
    
    public IEnumerable<DispatchNote> DispatchNotes()
    {
      return DispatchNoteSet;
    }   

    #endregion

    public class Mapping : EntityTypeConfiguration<Haulier>
    {
      public Mapping()
      {
        HasMany(c => c.DispatchNoteSet).WithRequired(a => a.Haulier);
      }
    }
  }
}