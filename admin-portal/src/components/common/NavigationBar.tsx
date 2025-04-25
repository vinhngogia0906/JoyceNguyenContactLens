import { AppBar, Box, Button, Toolbar, Typography, useTheme } from "@mui/material";
import { useNavigate } from "react-router-dom";
import LogoutButton from "../LogoutButton";
import logo from '../../assets/logo/logo.jpg';
import { log } from "console";

interface NavBarProps {
  title?: string;
  navItems?: {label: string, path: string}[];
}

const NavBar: React.FC<NavBarProps> = ({
  title= '',
  navItems = [
    {label: "Dashboard", path: '/dashboard'},
    {label: "Manage Products", path: '/products'},
    {label: "Manage Orders", path: "/orders"}
  ]
}) => {
  const navigate = useNavigate();
  const theme = useTheme();

  const handleNavigation = (path: string) => {
    navigate(path);
  };
  
  return (
    <AppBar
      position="static"
      sx={{
        backgroundColor: 'white',
        color: 'black',
        boxShadow: 'none',
        borderBottom: '1px solid ${theme.palette.divider}'
      }}
    >
     <Toolbar>
        <Box component='img'
          src={logo}
          alt='Joyce Nguyen Logo'
          sx= {{
            height: 40,
            mr: 2
          }}        
        />
        <Typography
          variant="h6"
          component='div'
          sx={{
            flexGrow: 1,
            textAlign: 'center',
            fontWeight: 'bold',
            color: theme.palette.primary.main
          }}
        >
          {title}
        </Typography>
        <Box sx={{display: 'flex', gap: 1}}>
          {navItems.map((item) => (
            <Button
              key={item.path}
              color="inherit"
              onClick={() => {handleNavigation(item.path)}}
              sx={{
                textTransform: 'none',
                '&:hover' : {
                  backgroundColor: theme.palette.action.hover
                }
              }}
            >
              {item.label}
            </Button>
          ))}
          <LogoutButton />
        </Box>
      </Toolbar> 
    </AppBar>
  );
}

export default NavBar;