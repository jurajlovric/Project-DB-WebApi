import React from 'react';
import './Customer.css';

function CustomerTableBody({ customers, onEdit, onDelete }) {
  return (
    <tbody id="customerTableBody">
      {customers.map((customer) => (
        <tr key={customer.id}>
          <td>{customer.id}</td>
          <td>{customer.name}</td>
          <td>{customer.email}</td>
          <td>
            <button onClick={() => onEdit(customer.id)}>Edit</button>
            <button onClick={() => onDelete(customer.id)}>Delete</button>
          </td>
        </tr>
      ))}
    </tbody>
  );
}

export default CustomerTableBody;
