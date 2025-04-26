import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate} from 'react-router-dom';
import { ApolloProvider } from '@apollo/client';
import client from './api/graphql/client';
import Login from './pages/auth/Login';
import Dashboard from './pages/dashboard/Dashboard';
import ManageProducts from './pages/dashboard/ManageProducts';
import ManageOrders from './pages/dashboard/ManageOrders';
import PrivateRoute from './components/PrivateRoute';

const App: React.FC = () => {
  return (
    <ApolloProvider client={client}>
      <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<PrivateRoute />}>
            <Route path="/dashboard" element={<Dashboard />}/>
            <Route path="/products" element={<ManageProducts />} />
            <Route path="/orders" element={<ManageOrders />} />
          </Route>
          <Route path="*" element={<Navigate to="/login" replace />} />
        </Routes>
      </Router>
    </ApolloProvider>
  );
};

export default App;