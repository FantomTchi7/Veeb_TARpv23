import React, { useState, useEffect } from 'react';
import axios from 'axios';

const PRODUCTS_API_URL = 'https://localhost:7129/api/Products';
const CATEGORIES_API_URL = 'https://localhost:7129/api/Categories';

function AddProduct() {
  const [product, setProduct] = useState({
    name: '',
    categoryId: '',
    price: 0,
    imageUrl: '',
    isActive: true,
    stockQuantity: 0,
    expirationTime: ''
  });

  const [categories, setCategories] = useState([]);
  const [loadingCategories, setLoadingCategories] = useState(true);

  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  useEffect(() => {
    axios.get(CATEGORIES_API_URL)
      .then(response => {
        setCategories(response.data);
        setLoadingCategories(false);
      })
      .catch(error => {
        console.error('Kategooriate laadimisel tekkis viga!', error);
        setError('Kategooriate laadimine ebaõnnestus. Vormi ei saa kuvada.');
        setLoadingCategories(false);
      });
  }, []);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setProduct(prevState => ({
      ...prevState,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    const productToSubmit = {
      ...product,
      price: parseInt(product.price, 10),
      stockQuantity: parseInt(product.stockQuantity, 10),
      categoryId: parseInt(product.categoryId, 10)
    };

    axios.post(PRODUCTS_API_URL, productToSubmit)
      .then(response => {
        setSuccess('Toode lisati edukalt!');
        setProduct({
          name: '', categoryId: '', price: 0, imageUrl: '',
          isActive: true, stockQuantity: 0, expirationTime: ''
        });
      })
      .catch(error => {
        console.error('Toote lisamisel tekkis viga!', error.response);
        if (error.response && error.response.data) {
          const errors = error.response.data.errors || { general: [error.response.data] };
          const errorMessages = Object.values(errors).flat().join(' ');
          setError(`Viga: ${errorMessages}`);
        } else {
          setError('Toote lisamine ebaõnnestus.');
        }
      });
  };

  if (loadingCategories) {
    return <p>Laen kategooriaid...</p>;
  }

  return (
    <div className="container card">
      <h2>Lisa Uus Toode</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Toote Nimi:</label>
          <input type="text" name="name" value={product.name} onChange={handleChange} required placeholder="Näiteks: Nutitelefon" />
        </div>
        <div>
          <label>Kategooria:</label>
          <select name="categoryId" value={product.categoryId} onChange={handleChange} required>
            <option value="" disabled>Vali kategooria</option>
            {categories.map(category => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Hind (€):</label>
          <input type="number" name="price" value={product.price} onChange={handleChange} required min="0.01" step="0.01" />
        </div>
        <div>
          <label>Laoseis (tk):</label>
          <input type="number" name="stockQuantity" value={product.stockQuantity} onChange={handleChange} required min="0" />
        </div>
        <div>
          <label>Aegumiskuupäev:</label>
          <input type="date" name="expirationTime" value={product.expirationTime} onChange={handleChange} required />
        </div>
        <div>
          <label>Pildi URL:</label>
          <input type="text" name="imageUrl" value={product.imageUrl} onChange={handleChange} placeholder="http://..."/>
        </div>
        <button type="submit" className="button-primary">Lisa Toode</button>
      </form>
      {error && <p className="error-message">{error}</p>}
      {success && <p className="success-message">{success}</p>}
    </div>
  );
}

export default AddProduct;