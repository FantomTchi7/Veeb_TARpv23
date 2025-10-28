import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = 'https://localhost:7129/api/Photos';

function PhotoList() {
    const [photos, setPhotos] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        axios.get(API_URL)
            .then(response => {
                setPhotos(response.data.slice(0, 50));
            })
            .catch(error => {
                console.error('Fotode laadimisel tekkis viga!', error);
                setError('Fotode laadimine ebaõnnestus.');
            });
    }, []);

    if (error) {
        return <div className="error">{error}</div>;
    }

    return (
        <div>
            <h2>Fotogalerii</h2>
            <div className="photo-gallery">
                {photos.map(photo => (
                    <div key={photo.id} className="photo-item">
                        <img src={photo.thumbnailUrl} alt={photo.title} />
                        <p>{photo.title}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default PhotoList;