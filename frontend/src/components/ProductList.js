import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = 'https://localhost:7129/api/Products';

function ProductList() {
  const [products, setProducts] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    axios.get(API_URL)
      .then(response => {
        setProducts(response.data);
      })
      .catch(error => {
        console.error('Toodete laadimisel tekkis viga!', error);
        setError('Toodete laadimine ebaõnnestus. Palun proovi hiljem uuesti.');
      });
  }, []);

  if (error) {
    return <div className="error">{error}</div>;
  }

  return (
    <div>
      <h2>Toodete Nimekiri</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nimi (ID)</th>
            <th>Kategooria ID</th>
            <th>Hind (€)</th>
            <th>Laoseis</th>
            <th>Aegumiskuupäev</th>
          </tr>
        </thead>
        <tbody>
          {products.map(product => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.categoryId}</td>
              <td>{product.price}</td>
              <td>{product.stockQuantity}</td>
              <td>{new Date(product.expirationTime).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ProductList;