import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = 'https://localhost:7129/api/Categories';

function CategoryManager() {
    const [categories, setCategories] = useState([]);
    const [newCategoryName, setNewCategoryName] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const fetchCategories = () => {
        axios.get(API_URL)
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => {
                console.error('Kategooriate laadimisel tekkis viga!', error);
                setError('Kategooriate laadimine ebaõnnestus.');
            });
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        if (!newCategoryName.trim()) {
            setError('Kategooria nimi не может быть пустым.');
            return;
        }

        axios.post(API_URL, { name: newCategoryName })
            .then(() => {
                setSuccess(`Kategooria "${newCategoryName}" lisati edukalt!`);
                setNewCategoryName('');
                fetchCategories();
            })
            .catch(error => {
                console.error('Kategooria lisamisel tekkis viga!', error);
                setError('Kategooria lisamine ebaõnnestus.');
            });
    };

    return (
        <div className="container">
            <h2>Halda Kategooriaid</h2>

            <div className="form-container card">
                <h3>Lisa Uus Kategooria</h3>
                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Kategooria nimi:</label>
                        <input
                            type="text"
                            value={newCategoryName}
                            onChange={(e) => setNewCategoryName(e.target.value)}
                            placeholder="Näiteks: Elektroonika"
                            required
                        />
                    </div>
                    <button type="submit" className="button-primary">Lisa</button>
                </form>
                {error && <p className="error-message">{error}</p>}
                {success && <p className="success-message">{success}</p>}
            </div>

            <div className="list-container card">
                <h3>Olemasolevad Kategooriad</h3>
                <ul>
                    {categories.map(category => (
                        <li key={category.id}>{category.name}</li>
                    ))}
                </ul>
            </div>
        </div>
    );
}

export default CategoryManager;