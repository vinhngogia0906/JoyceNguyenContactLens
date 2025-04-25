import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import client from '../api/graphql/client';

const LogoutButton: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    client.resetStore(); // Clear Apollo cache
    navigate('/login');
  };

  return (
    <Button 
      variant="contained" 
      color="error"
      onClick={handleLogout}
    >
      Logout
    </Button>
  );
};

export default LogoutButton;