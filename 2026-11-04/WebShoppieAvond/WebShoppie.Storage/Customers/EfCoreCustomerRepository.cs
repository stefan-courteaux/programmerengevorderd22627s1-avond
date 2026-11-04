using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Customers;

public class EfCoreCustomerRepository(WebShoppieDbContext _dbContext) : ICustomerRepository
{
    public CustomerDataModel Create(CustomerDataModel customer)
    {
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();
        return customer;
    }

    public CustomerDataModel? GetById(int id)
    {
        return _dbContext.Customers.Find(id);
    }

    public IEnumerable<CustomerDataModel> GetAll()
    {
        return _dbContext.Customers;
    }

    public bool Delete(int customerId)
    {
        var existing = _dbContext.Customers.Find(customerId);
        
        if (existing == null)
            return false;
        
        _dbContext.Customers.Remove(existing);
        _dbContext.SaveChanges();
        return true;
    }

    public CustomerDataModel Update(CustomerDataModel customer)
    {
        _dbContext.Customers.Update(customer);
        _dbContext.SaveChanges();
        return customer;
    }
}