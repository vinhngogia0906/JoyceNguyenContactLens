import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ApolloProvider } from '@apollo/client';
import client from './api/graphql/client';
import Login from './pages/Login/Login';
import Dashboard from './pages/Dashboard/Dashboard';
import ManageProducts from './pages/Dashboard/ManageProducts';
import ManageOrders from './pages/Dashboard/ManageOrders';
import PrivateRoute from './components/PrivateRoute';

const App: React.FC = () => {
  return (
    <ApolloProvider client={client}>
      <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<PrivateRoute />}>
            <Route path="/dashboard" element={<Dashboard />}>
              <Route path="products" element={<ManageProducts />} />
              <Route path="orders" element={<ManageOrders />} />
              <Route index element={<ManageProducts />} />
            </Route>
          </Route>
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      </Router>
    </ApolloProvider>
  );
};

export default App;