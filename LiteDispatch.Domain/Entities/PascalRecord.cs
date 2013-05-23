namespace LiteDispatch.Domain.Entities
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("PascalRecords")]
  public class PascalRecord
  {
    [Key, Column("L", Order = 0)]
    public int Level { get; set; }

    [Key, Column("R", Order = 1)]
    public int Row { get; set; }

    [Key, Column("P", Order = 2)]
    public int Position { get; set; }

    public decimal Value { get; set; }

  }
}
