import React, { useState, useEffect } from 'react';
import './Customer.css';

function EditCustomerForm({ customer, onSave }) {
  const [name, setName] = useState(customer.name);
  const [email, setEmail] = useState(customer.email);

  const handleSubmit = (event) => {
    event.preventDefault();
    onSave({ ...customer, name, email });
  };

  useEffect(() => {
    setName(customer.name);
    setEmail(customer.email);
  }, [customer]);

  return (
    <form id="editCustomerForm" onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="editCustomerName">Name:</label>
        <input type="text" id="editCustomerName" value={name} onChange={(e) => setName(e.target.value)} required />
      </div>
      <div className="form-group">
        <label htmlFor="editCustomerEmail">Email:</label>
        <input type="email" id="editCustomerEmail" value={email} onChange={(e) => setEmail(e.target.value)} required />
      </div>
      <button type="submit">Save Changes</button>
    </form>
  );
}

export default EditCustomerForm;
