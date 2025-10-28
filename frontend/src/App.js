// src/App.js
import React, { useState } from 'react';
import ProductList from './components/ProductList';
import AddProduct from './components/AddProduct';
import PhotoList from './components/PhotoList';
import CategoryManager from './components/CategoryManager'; // Impordi uus komponent
import './App.css';

function App() {
  const [view, setView] = useState('products');

  const renderView = () => {
    switch (view) {
      case 'products':
        return <ProductList />;
      case 'addProduct':
        return <AddProduct />;
      case 'categories':
        return <CategoryManager />;
      case 'photos':
        return <PhotoList />;
      default:
        return <ProductList />;
    }
  };

  return (
    <div className="app-container">
      <aside className="sidebar">
        <h1 className="logo">Veebipood</h1>
        <nav className="navigation">
          <button onClick={() => setView('products')} className={view === 'products' ? 'active' : ''}>
            Toodete Nimekiri
          </button>
          <button onClick={() => setView('addProduct')} className={view === 'addProduct' ? 'active' : ''}>
            Lisa Toode
          </button>
          <button onClick={() => setView('categories')} className={view === 'categories' ? 'active' : ''}>
            Halda Kategooriaid
          </button>
          <button onClick={() => setView('photos')} className={view === 'photos' ? 'active' : ''}>
            Fotogalerii
          </button>
        </nav>
      </aside>
      <main className="content">
        {renderView()}
      </main>
    </div>
  );
}

export default App;