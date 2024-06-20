document.addEventListener('DOMContentLoaded', function() {
    const customerForm = document.getElementById('customerForm');
    const customerTableBody = document.getElementById('customerTableBody');

    customerForm.addEventListener('submit', function(event) {
        event.preventDefault();
        addCustomer();
    });

    function loadCustomers() {
        const customers = JSON.parse(localStorage.getItem('customers')) || [];
        customerTableBody.innerHTML = '';
        customers.forEach(customer => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${customer.id}</td>
                <td>${customer.name}</td>
                <td>${customer.email}</td>
                <td>
                    <button onclick="editCustomer('${customer.id}')">Edit</button>
                    <button onclick="deleteCustomer('${customer.id}')">Delete</button>
                </td>
            `;
            customerTableBody.appendChild(row);
        });
    }

    window.addCustomer = function() {
        const id = document.getElementById('customerId').value;
        const name = document.getElementById('customerName').value;
        const email = document.getElementById('customerEmail').value;

        const customers = JSON.parse(localStorage.getItem('customers')) || [];
        const customerIndex = customers.findIndex(customer => customer.id === id);

        if (customerIndex === -1) {
            customers.push({ id, name, email });
        } else {
            customers[customerIndex] = { id, name, email };
        }

        localStorage.setItem('customers', JSON.stringify(customers));
        loadCustomers();
        customerForm.reset();
    };

    window.editCustomer = function(id) {
        const customers = JSON.parse(localStorage.getItem('customers')) || [];
        const customer = customers.find(customer => customer.id === id);

        if (customer) {
            document.getElementById('customerId').value = customer.id;
            document.getElementById('customerName').value = customer.name;
            document.getElementById('customerEmail').value = customer.email;
        }
    };

    window.deleteCustomer = function(id) {
        const customers = JSON.parse(localStorage.getItem('customers')) || [];
        const newCustomers = customers.filter(customer => customer.id !== id);

        localStorage.setItem('customers', JSON.stringify(newCustomers));
        loadCustomers();
    };

    loadCustomers();
});
