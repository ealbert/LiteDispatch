namespace LiteDispatch.Domain.Entities
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Base class for entities that have a long ID as PK
  /// </summary>
  public abstract class EntityBase
  {
    [Key]
    public virtual long Id { get; protected set; }
  }
}