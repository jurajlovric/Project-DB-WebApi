import React from 'react';
import { Link } from 'react-router-dom';
import './HomePage.css';

function HomePage() {
  return (
    <div>
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/customers">Customers</Link></li>
          <li><Link to="/add">Add Customer</Link></li>
        </ul>
      </nav>
      <div className="welcome-container">
        <h1>Welcome</h1>
      </div>
    </div>
  );
}

export default HomePage;
