using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Data.Repository;
using FIAP.IRRIGACAO.API.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

public class FaucetRepository : IFaucetRepository
{
    private readonly DatabaseContext _context;

    public FaucetRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IPagedList<FaucetModel> GetAllPaged(int pageNumber, int pageSize)
    {
       return _context.Faucet.Include(c => c.Location).ToPagedList(pageNumber, pageSize);
    } 

    public FaucetModel? FindById(long id) 
    {
        var model = _context.Faucet.Include(f => f.Location).First(f => f.Id == id);

        return model;
    }

    public void Create(FaucetModel model)
    {
        model.Location = _context.Location.Find(model.LocationId);
        _context.Faucet.Add(model);
        _context.SaveChanges();
    }

    public void Update(FaucetModel model)
    {
        model.Location = _context.Location.Find(model.LocationId);
        _context.Faucet.Update(model);
        _context.SaveChanges();
    }

    public FaucetModel? DeleteAndReturn(long id)
    {
        var model = _context.Faucet.Find(id);

        if (model == null)
            return null;

        _context.Faucet.Remove(model);
        _context.SaveChanges();

        return model;
    }
}