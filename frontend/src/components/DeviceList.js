import React, { useState, useEffect } from 'react';

const API_URL = 'https://localhost:7129/api';

function DeviceList() {
    const [devices, setDevices] = useState([]);

    useEffect(() => {
        fetch(`${API_URL}/Devices/Active`)
            .then(response => response.json())
            .then(data => setDevices(data))
            .catch(error => console.error('Error fetching devices:', error));
    }, []);

    return (
        <div>
            <h2>Aktiivsed Seadmed</h2>
            {devices.length > 0 ? (
                <ul>
                    {devices.map(device => (
                        <li key={device.id}>
                            <strong>{device.name}</strong> ({device.manufacturer}) - Järgmine hooldus: {new Date(device.nextMaintenanceTime).toLocaleDateString()}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>Aktiivseid seadmeid ei leitud.</p>
            )}
        </div>
    );
}

export default DeviceList;