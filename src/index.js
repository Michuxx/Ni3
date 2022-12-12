import React from 'react';
import ReactDOM from 'react-dom/client';
import './css/index.css';
import App from './Components/App';
import reportWebVitals from './reportWebVitals';
import AuthWrapper from './Components/LoginStart/AuthWrapper';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
    <AuthWrapper />
  </React.StrictMode>
);

reportWebVitals();
