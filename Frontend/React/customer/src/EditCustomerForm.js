import React from 'react';

function EditCustomerForm({ formData, handleInputChange, handleSave }) {
  return (
    <form id="editCustomerForm" onSubmit={(e) => {
      e.preventDefault();
      handleSave(formData);
    }}>
      <div className="form-group">
        <label htmlFor="editCustomerFirstName">First Name:</label>
        <input
          type="text"
          id="editCustomerFirstName"
          name="firstName"
          value={formData.firstName}
          onChange={handleInputChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="editCustomerLastName">Last Name:</label>
        <input
          type="text"
          id="editCustomerLastName"
          name="lastName"
          value={formData.lastName}
          onChange={handleInputChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="editCustomerEmail">Email:</label>
        <input
          type="email"
          id="editCustomerEmail"
          name="email"
          value={formData.email}
          onChange={handleInputChange}
          required
        />
      </div>
      <button type="submit">Save Changes</button>
    </form>
  );
}

export default EditCustomerForm;
