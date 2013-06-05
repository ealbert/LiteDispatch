namespace LiteDispatch.Test.Mappings
{
  using AutoMapper;
  using Domain.Mappings;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class AutoMapperTests
  {
    [TestMethod]
    public void CheckMappings()
    {
      ModelToEntity.Install();
      EntityToModel.Install();
      DtoToEntity.Install();
      Mapper.AssertConfigurationIsValid();
    }
  }
}