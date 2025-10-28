import React, { useState, useEffect } from 'react';

const API_URL = 'https://localhost:7129/api';

function EmployeeList() {
    const [employees, setEmployees] = useState([]);

    useEffect(() => {
        fetch(`${API_URL}/Employees?page=1`)
            .then(response => response.json())
            .then(data => setEmployees(data))
            .catch(error => console.error('Error fetching employees:', error));
    }, []);

    return (
        <div>
            <h2>Töötajad (leht 1)</h2>
            <ul>
                {employees.map(employee => (
                    <li key={employee.id}>
                        {employee.firstName} {employee.lastName} - {employee.email}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default EmployeeList;