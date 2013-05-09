namespace LiteDispatch.Domain.Entities
{
  using System;
  using System.Collections.Generic;
  using Models;
  using Repository;

  public class DispatchNote
    :EntityBase
  {
    protected DispatchNote()
    {
      DispatchLineSet = new HashSet<DispatchLine>();
    }

    public static DispatchNote Create(IRepositoryLocator locator, DispatchModel model)
    {
      var haulier = locator.GetById<Haulier>(model.HaulierId);
      var instance = new DispatchNote
        {
          CreationDate = model.CreationDate,
          DispatchDate = model.DispatchDate,
          DispatchNoteStatus = DispatchNoteStatusEnum.New,
          DispatchReference = model.DispatchReference,
          Haulier = haulier,
          TruckReg = model.TruckReg,
          User = model.User
        };

      locator.Save(instance);
      model.Lines.ForEach(l => instance.AddLine(locator, l));
      return instance;
    }

    public DispatchLine AddLine(IRepositoryLocator locator, DispatchLineModel dispatchLineModel)
    {
      var line = DispatchLine.Create(locator, dispatchLineModel);
      DispatchLineSet.Add(line);
      return line;
    }

    #region Persisted Properties

    public virtual Haulier Haulier { get; private set; }
    public virtual DateTime DispatchDate { get; private set; }
    public virtual DispatchNoteStatusEnum DispatchNoteStatus { get; private set; }
    public virtual string TruckReg { get; private set; }
    public virtual string DispatchReference { get; private set; }
    public virtual DateTime CreationDate { get; private set; }
    public virtual string User { get; private set; }
    protected virtual ICollection<DispatchLine> DispatchLineSet { get; set; }

    #endregion
    #region Public Methods

    public IEnumerable<DispatchLine> DispatchLines()
    {
      return DispatchLineSet;
    }

    #endregion
  }
}