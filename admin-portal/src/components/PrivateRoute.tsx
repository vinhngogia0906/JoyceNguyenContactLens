import { Navigate, Outlet } from "react-router-dom";

const PrivateRoute: React.FC = () => {
  const isAuthenticated = !!localStorage.getItem('jwtToken');
  return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace/>
}

export default PrivateRoute;