import React from 'react';
import './App.css';
import DeviceList from './components/DeviceList';
import EmployeeList from './components/EmployeeList';
import AddDeviceForm from './components/AddDeviceForm';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Elektrijaama Halduspaneel</h1>
      </header>
      <main className="container">
        <section className="column">
          <AddDeviceForm />
        </section>
        <section className="column">
          <DeviceList />
          <hr />
          <EmployeeList />
        </section>
      </main>
    </div>
  );
}

export default App;