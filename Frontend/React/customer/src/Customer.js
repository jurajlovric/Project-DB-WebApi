import React, { useState, useEffect } from 'react';
import seedData from './seedData';
import CustomerForm from './CustomerForm';
import CustomerTableBody from './CustomerTableBody';
import EditCustomerForm from './EditCustomerForm';
import './Customer.css';

function Customer() {
  const [customers, setCustomers] = useState([]);
  const [editingCustomer, setEditingCustomer] = useState(null);

  useEffect(() => {
    const storedCustomers = JSON.parse(localStorage.getItem('customers'));
    if (storedCustomers && storedCustomers.length > 0) {
      setCustomers(storedCustomers);
    } else {
      setCustomers(seedData);
    }
  }, []);

  useEffect(() => {
    localStorage.setItem('customers', JSON.stringify(customers));
  }, [customers]);

  const handleFormSubmit = (event) => {
    event.preventDefault();
    const id = event.target.customerId.value;
    const name = event.target.customerName.value;
    const email = event.target.customerEmail.value;

    const customerIndex = customers.findIndex(customer => customer.id === id);

    if (customerIndex === -1) {
      setCustomers([...customers, { id, name, email }]);
    } else {
      const updatedCustomers = customers.map((customer, index) => 
        index === customerIndex ? { id, name, email } : customer
      );
      setCustomers(updatedCustomers);
    }

    event.target.reset();
  };

  const handleEdit = (id) => {
    const customer = customers.find(c => c.id === id);
    setEditingCustomer(customer);
  };

  const handleDelete = (id) => {
    const updatedCustomers = customers.filter(customer => customer.id !== id);
    setCustomers(updatedCustomers);
  };

  const handleSave = (updatedCustomer) => {
    const updatedCustomers = customers.map(customer => 
      customer.id === updatedCustomer.id ? updatedCustomer : customer
    );
    setCustomers(updatedCustomers);
    setEditingCustomer(null);
  };

  return (
    <div className="content">
      <nav>
        <ul>
          <li><a href="/">Home</a></li>
          <li><a href="/customers">Customers</a></li>
          <li><a href="/orders">Orders</a></li>
          <li><a href="/form">Add Customer</a></li>
        </ul>
      </nav>
      <h1>Customers</h1>
      <CustomerForm onSubmit={handleFormSubmit} />
      <table>
        <thead>
          <tr>
            <th>Customer ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Actions</th>
          </tr>
        </thead>
        <CustomerTableBody customers={customers} onEdit={handleEdit} onDelete={handleDelete} />
      </table>
      {editingCustomer && <EditCustomerForm customer={editingCustomer} onSave={handleSave} />}
    </div>
  );
}

export default Customer;
