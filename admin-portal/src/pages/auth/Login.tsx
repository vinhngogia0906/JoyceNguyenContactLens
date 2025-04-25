import { useMutation } from "@apollo/client";
import { Alert, Box, Button, CircularProgress, Container, IconButton, InputAdornment, Paper, TextField, Typography } from "@mui/material";
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ADMINLOGIN } from "../../api/graphql/mutations";


interface LoginFormData {
  email: string;
  password: string;
}

const Login: React.FC = () => {
  const [formData, setFormData] = useState<LoginFormData>({
    email: '',
    password: ''
  });
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const [loginMutation, {loading}] = useMutation(ADMINLOGIN, {
    onCompleted: (data) => {
      localStorage.setItem('jwtToken', data.adminLogin);
      navigate('/dashboard');
    },
    onError: (err) => {
      const graphQLError = err.graphQLErrors[0];
      if (graphQLError?.extensions?.code === 'UNAUTHENTICATED') {
        setError('Invalid email or password');
      } else {
        setError(err.message || 'Login failed');
      }
    }
  });
  const validateFormData = () => {
    if (!formData.email) {
      setError('Email is required');
      return false;
    }
    
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      setError('Please enter a valid email');
      return false;
    }
    
    if (!formData.password) {
      setError('Password is required');
      return false;
    }
    
    return true;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const {name, value} = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if(!formData || !formData.password) {
      setError("Please fill in the credentials!");
      return;
    }
    if(!validateFormData()) return;
    loginMutation({
      variables: {
        email: formData.email,
        password: formData.password
      }
    });
  };

  return (
    <Container maxWidth="xs">
      <Paper elevation={3} sx={{p: 4, mt: 8}}>
        <Typography variant="h4" align="center" gutterBottom>
          Admin Login
        </Typography>
        {error && (
          <Alert severity="error" sx={{mb: 3}}>
            {error}
          </Alert>
        )}
        <Box component='form' onSubmit={handleSubmit}>
          <TextField
            label="Email"
            name="email"
            type="email"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.email}
            onChange={handleChange}
            required
            autoComplete="email"
          />

          <TextField
            label="Password"
            name="password"
            type={showPassword ? 'text' : 'password'}
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.password}
            onChange={handleChange}
            required
            autoComplete="password"
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    onClick={() => setShowPassword(!showPassword)}
                    edge="end"
                  >
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            size="large"
            sx={{mt: 3}}
            disabled={loading}
          >
            {loading ? (
              <CircularProgress size={24} color="inherit"/>
            ):
            ('Admin Login')}

          </Button>
        </Box>
      </Paper>

    </Container>
  );
}

export default Login;