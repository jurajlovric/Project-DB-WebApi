import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { addCustomer } from './customerService';
import CustomerForm from './CustomerForm';
import './CustomerForm.css';

function AddCustomer() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: ''
  });
  const navigate = useNavigate();

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleFormSubmit = (event) => {
    event.preventDefault();
    const { firstName, lastName, email } = formData;

    addCustomer({ firstName, lastName, email })
      .then(() => {
        navigate('/customers');
      })
      .catch(error => {
        console.error('There was an error adding the customer!', error);
      });
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
      <h1>Add Customer</h1>
      <CustomerForm
        formData={formData}
        handleInputChange={handleInputChange}
        handleFormSubmit={handleFormSubmit}
      />
    </div>
  );
}

export default AddCustomer;
