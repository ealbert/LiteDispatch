using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteDispatch.Web.BusinessAdapters
{
  using AutoMapper;
  using Domain.Entities;
  using Domain.Models;
  using Domain.Repository;
  using Domain.Services;

  public class DispatchAdapter
  {
    public DispatchNoteModel GetDispathNoteById(long id)
    {
      return ExecuteCommand(locator => GetDispathNoteByIdImpl(locator, id));
    }


    private DispatchNoteModel GetDispathNoteByIdImpl(IRepositoryLocator locator, long id)
    {
      var instance = locator.GetById<DispatchNote>(id);
      return Mapper.Map<DispatchNote, DispatchNoteModel>(instance);
    }

    public IEnumerable<DispatchNoteModel> GetAllDispatches()
    {
      return ExecuteCommand(GetAllDispatchesImpl);
    }

    private IEnumerable<DispatchNoteModel> GetAllDispatchesImpl(IRepositoryLocator locator)
    {
      var instances = locator.FindAll<DispatchNote>().ToList();
      return Mapper.Map<List<DispatchNote>, List<DispatchNoteModel>>(instances);
    }

    private TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) where TResult : class
    {
      using (var transManager = Container.GlobalContext.TransFactory.CreateManager())
      {
        return transManager.ExecuteCommand(command);
      }
    }

    public DispatchNoteModel SaveDispatch(DispatchNoteModel dispatchModel)
    {
      return ExecuteCommand(locator => SaveDispatchImpl(locator, dispatchModel));
    }

    private DispatchNoteModel SaveDispatchImpl(IRepositoryLocator locator, DispatchNoteModel dispatchModel)
    {
      var entity = DispatchNote.Create(locator, dispatchModel);
      locator.FlushModifications(); // need to flush in order to get the Id into the DTO
      return Mapper.Map<DispatchNote, DispatchNoteModel>(entity);
    }
  }
}