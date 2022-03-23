using MotorNVS.BL.DTOs.AddressDTO;
using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.DTOs.ZipcodeDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse> GetCustomerById(int customerId);
        Task<CustomerResponse> DeleteCustomerById(int customerId);
        Task<CustomerResponse> CreateCustomer(CustomerRequest newCustomer);
        Task<CustomerResponse> UpdateCustomer(int customerId, CustomerRequest customerUpdate);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest newCustomer)
        {
            Customer createdCustomer = await _customerRepository.InsertNewCustomer(MapCustomerRequestToCustomer(newCustomer));

            if (createdCustomer != null)
            {
                return MapCustomerToCustomerResponse(createdCustomer);
            }

            return null;
        }

        public async Task<CustomerResponse> DeleteCustomerById(int customerId)
        {
            Customer deletedCustomer = await _customerRepository.DeleteCustomerById(customerId);

            if (deletedCustomer != null)
            {
                return MapCustomerToCustomerResponse(deletedCustomer);
            }

            return null;
        }

        public async Task<List<CustomerResponse>> GetAllCustomers()
        {
            List<Customer> customers = await _customerRepository.SelectAllCustomers();

            return customers.Select(x => MapCustomerToCustomerResponse(x)).ToList();
        }

        public async Task<CustomerResponse> GetCustomerById(int customerId)
        {
            Customer customer = await _customerRepository.SelectCustomerById(customerId);

            if (customer != null)
            {
                return MapCustomerToCustomerResponse(customer);
            }

            return null;
        }

        public async Task<CustomerResponse> UpdateCustomer(int customerId, CustomerRequest customerUpdate)
        {
            Customer updatedCustomer = await _customerRepository.UpdateCustomerById(customerId, MapCustomerRequestToCustomer(customerUpdate));

            if (updatedCustomer != null)
            {
                return MapCustomerToCustomerResponse(updatedCustomer);
            }

            return null;
        }

        private static CustomerResponse MapCustomerToCustomerResponse(Customer customer)
        {
            CustomerResponse res = new CustomerResponse()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                CreateDate = customer.CreateDate,
                AddressId = customer.AddressId
            };

            if(customer.Address != null)
            {
                res.AddressResponse = new AddressResponse()
                {
                    Id = customer.Address.Id,
                    StreetAndNo = customer.Address.StreetAndNo,
                    CreateDate = customer.Address.CreateDate,
                    ZipcodeId = customer.Address.ZipCodeId,
                    ZipcodeResponse = new ZipcodeResponse()
                    {
                        Id = customer.Address.Zipcode.Id,
                        ZipcodeNo = customer.Address.Zipcode.ZipcodeNo,
                        City = customer.Address.Zipcode.City
                    }
                };
            };

            return res;
        }

        private static Customer MapCustomerRequestToCustomer(CustomerRequest customerReq)
        {
            return new Customer()
            {
                FirstName = customerReq.FirstName,
                LastName = customerReq.LastName,
                CreateDate = customerReq.CreateDate,
                AddressId = customerReq.AddressId
            };
        }
    }
}
