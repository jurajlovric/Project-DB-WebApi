import React, { Component } from 'react';
import { getCustomers, addCustomer, updateCustomer, deleteCustomer } from './customerService';
import CustomerForm from './CustomerForm';
import CustomerTable from './CustomerTable';
import EditCustomerForm from './EditCustomerForm';
import './Customer.css';

class Customer extends Component {
  state = {
    customers: [],
    editingCustomer: null,
    formData: {
      firstName: '',
      lastName: '',
      email: ''
    }
  };

  componentDidMount() {
    this.loadCustomers();
  }

  loadCustomers = () => {
    getCustomers()
      .then(response => {
        this.setState({ customers: response.data });
      })
      .catch(error => {
        console.error('There was an error fetching the customers!', error);
      });
  };

  handleFormSubmit = (event) => {
    event.preventDefault();
    const { firstName, lastName, email } = this.state.formData;

    addCustomer({ firstName, lastName, email })
      .then(() => {
        this.loadCustomers();
        this.setState({ formData: { firstName: '', lastName: '', email: '' } });
      })
      .catch(error => {
        console.error('There was an error adding the customer!', error);
      });
  };

  handleEdit = (id) => {
    const customer = this.state.customers.find(c => c.id === id);
    this.setState({ editingCustomer: customer, formData: customer });
  };

  handleDelete = (id) => {
    deleteCustomer(id)
      .then(() => {
        this.loadCustomers();
      })
      .catch(error => {
        console.error('There was an error deleting the customer!', error);
      });
  };

  handleSave = (updatedCustomer) => {
    updateCustomer(updatedCustomer.id, updatedCustomer)
      .then(() => {
        this.loadCustomers();
        this.setState({
          editingCustomer: null,
          formData: { firstName: '', lastName: '', email: '' }
        });
      })
      .catch(error => {
        console.error('There was an error saving the customer!', error);
      });
  };

  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState(prevState => ({
      formData: {
        ...prevState.formData,
        [name]: value
      }
    }));
  };

  render() {
    return (
      <div className="content">
        <nav>
          <ul>
            <li><a href="/customers">Customers</a></li>
          </ul>
        </nav>
        <h1>Customers</h1>
        <CustomerForm
          formData={this.state.formData}
          handleInputChange={this.handleInputChange}
          handleFormSubmit={this.handleFormSubmit}
        />
        <CustomerTable
          customers={this.state.customers}
          handleEdit={this.handleEdit}
          handleDelete={this.handleDelete}
        />
        {this.state.editingCustomer && (
          <EditCustomerForm
            formData={this.state.formData}
            handleInputChange={this.handleInputChange}
            handleSave={this.handleSave}
          />
        )}
      </div>
    );
  }
}

export default Customer;
