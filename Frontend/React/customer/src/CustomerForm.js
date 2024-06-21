import React from 'react';
import './Customer.css';

function CustomerForm({ onSubmit }) {
  return (
    <form id="customerForm" onSubmit={onSubmit}>
      <div className="form-group">
        <label htmlFor="customerId">Customer ID:</label>
        <input type="text" id="customerId" name="customerId" required/>
      </div>
      <div className="form-group">
        <label htmlFor="customerName">Name:</label>
        <input type="text" id="customerName" name="customerName" required />
      </div>
      <div className="form-group">
        <label htmlFor="customerEmail">Email:</label>
        <input type="email" id="customerEmail" name="customerEmail" required />
      </div>
      <button type="submit">Add Customer</button>
    </form>
  );
}

export default CustomerForm;
