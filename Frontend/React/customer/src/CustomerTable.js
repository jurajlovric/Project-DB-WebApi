import React from 'react';
import { Link } from 'react-router-dom';

function CustomerTable({ customers, handleDelete }) {
  return (
    <table>
      <thead>
        <tr>
          <th>Customer ID</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Email</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {customers.map((customer) => (
          <tr key={customer.id}>
            <td>{customer.id}</td>
            <td>{customer.firstName}</td>
            <td>{customer.lastName}</td>
            <td>{customer.email}</td>
            <td>
              <Link to={`/edit/${customer.id}`}>
                <button>Edit</button>
              </Link>
              <button onClick={() => handleDelete(customer.id)}>Delete</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default CustomerTable;
