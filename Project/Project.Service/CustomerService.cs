using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;

namespace Project.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public Customer GetCustomerById(Guid id)
        {
            return _customerRepository.GetCustomerById(id);
        }

        public void AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(Guid id)
        {
            _customerRepository.DeleteCustomer(id);
        }
    }
}
