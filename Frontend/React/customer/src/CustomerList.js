import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getCustomers, deleteCustomer } from './customerService';
import CustomerTable from './CustomerTable';
import './CustomerList.css';

function CustomerList() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    loadCustomers();
  }, []);

  const loadCustomers = () => {
    getCustomers()
      .then(response => {
        setCustomers(response.data);
      })
      .catch(error => {
        console.error('There was an error fetching the customers!', error);
      });
  };

  const handleDelete = (id) => {
    if (window.confirm('Are you sure you want to delete this customer?')) {
      deleteCustomer(id)
        .then(() => {
          loadCustomers();
        })
        .catch(error => {
          console.error('There was an error deleting the customer!', error);
        });
    }
  };

  return (
    <div className="content">
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/customers">Customers</Link></li>
          <li><Link to="/add">Add Customer</Link></li>
        </ul>
      </nav>
      <h1>Customers</h1>
      <CustomerTable
        customers={customers}
        handleDelete={handleDelete}
      />
    </div>
  );
}

export default CustomerList
