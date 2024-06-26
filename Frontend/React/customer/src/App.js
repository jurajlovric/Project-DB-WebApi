import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from './HomePage';
import CustomerList from './CustomerList';
import EditCustomer from './EditCustomer';
import AddCustomer from './AddCustomer';

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/customers" element={<CustomerList />} />
      <Route path="/edit/:id" element={<EditCustomer />} />
      <Route path="/add" element={<AddCustomer />} />
    </Routes>
  );
}

export default App;
