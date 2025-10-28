import React, { useState } from 'react';

const API_URL = 'https://localhost:7129/api';

function AddDeviceForm() {
    const [name, setName] = useState('');
    const [manufacturer, setManufacturer] = useState('');
    const [acquisitionCost, setAcquisitionCost] = useState(0);
    const [redisualValue, setRedisualValue] = useState(0);
    const [nextMaintenanceTime, setNextMaintenanceTime] = useState(new Date().toISOString().slice(0, 16));

    const handleSubmit = (event) => {
        event.preventDefault();

        const newDevice = {
            name: name,
            manufacturer: manufacturer,
            acquisitionCost: parseInt(acquisitionCost),
            redisualValue: parseInt(redisualValue),
            nextMaintenanceTime: new Date(nextMaintenanceTime).toISOString(),
            isActive: true
        };

        fetch(`${API_URL}/Devices`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newDevice),
        })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text) });
            }
            return response.json();
        })
        .then(data => {
            alert('Seade edukalt lisatud!');
            setName('');
            setManufacturer('');
            window.location.reload();
        })
        .catch((error) => {
            console.error('Error adding device:', error);
            alert(`Viga seadme lisamisel: ${error.message}`);
        });
    };

    return (
        <form onSubmit={handleSubmit} className="device-form">
            <h2>Lisa Uus Seade</h2>
            <div>
                <label>Nimi:</label>
                <input type="text" value={name} onChange={e => setName(e.target.value)} required />
            </div>
            <div>
                <label>Tootja:</label>
                <input type="text" value={manufacturer} onChange={e => setManufacturer(e.target.value)} required />
            </div>
            <div>
                <label>Soetusmaksumus:</label>
                <input type="number" value={acquisitionCost} onChange={e => setAcquisitionCost(e.target.value)} required />
            </div>
            <div>
                <label>Jääkväärtus:</label>
                <input type="number" value={redisualValue} onChange={e => setRedisualValue(e.target.value)} required />
            </div>
            <div>
                <label>Järgmise hoolduse aeg:</label>
                <input type="datetime-local" value={nextMaintenanceTime} onChange={e => setNextMaintenanceTime(e.target.value)} required />
            </div>
            <button type="submit">Lisa Seade</button>
        </form>
    );
}

export default AddDeviceForm;