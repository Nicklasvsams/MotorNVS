using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> SelectAllCustomers();
        Task<Customer> SelectCustomerById(int customerId);
        Task<Customer> InsertNewCustomer(Customer customer);
        Task<Customer> DeleteCustomerById(int customerId);
        Task<Customer> UpdateCustomerById(int customerId, Customer customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly MotorDBContext _dBContext;

        public CustomerRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Customer> DeleteCustomerById(int customerId)
        {
            Customer customerToDelete = await _dBContext
                .Customer
                .FirstOrDefaultAsync(x => x.Id == customerId);

            if(customerToDelete != null)
            {
                _dBContext.Remove(customerToDelete);
                await _dBContext.SaveChangesAsync();
            }

            return customerToDelete;
        }

        public async Task<Customer> InsertNewCustomer(Customer customer)
        {
            await _dBContext.AddAsync(customer);
            await _dBContext.SaveChangesAsync();

            return customer;
        }

        public async Task<List<Customer>> SelectAllCustomers()
        {
            return await _dBContext
                .Customer
                .Include(x => x.Address)
                .Include(x => x.Address.Zipcode)
                .ToListAsync();
        }

        public async Task<Customer> SelectCustomerById(int customerId)
        {
            return await _dBContext
                .Customer
                .Include(x => x.Address)
                .Include(x => x.Address.Zipcode)
                .FirstOrDefaultAsync(x => x.Id == customerId);
        }

        public async Task<Customer> UpdateCustomerById(int customerId, Customer customer)
        {
            Customer customerToUpdate = await _dBContext
                .Customer
                .FirstOrDefaultAsync(x => x.Id == customerId);

            if(customerToUpdate != null)
            {
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.CreateDate = customer.CreateDate;
                customerToUpdate.AddressId = customer.AddressId;
            }

            return customerToUpdate;
        }
    }
}
