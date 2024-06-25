import React from 'react';

function CustomerForm({ formData, handleInputChange, handleFormSubmit }) {
  return (
    <form id="customerForm" onSubmit={handleFormSubmit}>
      <div className="form-group">
        <label htmlFor="customerFirstName">First Name:</label>
        <input
          type="text"
          id="customerFirstName"
          name="firstName"
          value={formData.firstName}
          onChange={handleInputChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="customerLastName">Last Name:</label>
        <input
          type="text"
          id="customerLastName"
          name="lastName"
          value={formData.lastName}
          onChange={handleInputChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="customerEmail">Email:</label>
        <input
          type="email"
          id="customerEmail"
          name="email"
          value={formData.email}
          onChange={handleInputChange}
          required
        />
      </div>
      <button type="submit">Add Customer</button>
    </form>
  );
}

export default CustomerForm;
