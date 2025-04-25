import { useQuery } from "@apollo/client";
import { GET_CONTACT_LENS } from "../../api/graphql/queries";
import { Alert, Box, CircularProgress, Container, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import NavBar from "../../components/common/NavigationBar";
import { ContactLens } from "./Dashboard";

const ManageProducts: React.FC = () => {
  const {loading, error, data } = useQuery(GET_CONTACT_LENS);

  return(
    <Box sx={{display: 'flex', flexDirection: 'column', minHeight:'100vh'}}>
      <NavBar title="Joyce Nguyen Admin"/>
      <Container maxWidth='lg' sx={{py: 4, flexGrow: 1}}>
        <Typography variant="h4" gutterBottom sx={{mb:3}}>
          Manage Products
        </Typography>
        {loading && (
          <Box sx={{display: 'flex', justifyContent:'center', mt:4}}>
            <CircularProgress/>
          </Box>
        )}

        {error && (
          <Alert severity="error" sx={{mb:3}}>
            Error Loading contact lenses: {error.message}
          </Alert>
        )}
        {data && (
          <TableContainer component={Paper} elevation={3}>
            <Table sx={{minWidth: 650}} aria-label="contact lenses table">
              <TableHead>
                <TableRow sx={{backgroundColor: 'primary.light'}}>
                  <TableCell sx={{fontWeight:'bold'}}>ID</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Name</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Color</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Degree</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Price</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Quantity</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.allContactLenses.map((lens: ContactLens) => (
                  <TableRow
                    key={lens.id}
                    sx={{'&:last-child td, &:last-child th' : {border: 0}}}
                  >
                    <TableCell>{lens.id}</TableCell>
                    <TableCell>{lens.name}</TableCell>
                    <TableCell>{lens.color}</TableCell>
                    <TableCell>{lens.degree}</TableCell>
                    <TableCell>{lens.price}</TableCell>
                    <TableCell
                      sx={{
                        color: lens.quantity < 10 ? 'error.main' : 'inherit',
                        fontWeight: lens.quantity < 10 ? 'bold' : 'normal'
                      }}
                    >{lens.quantity}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}
      </Container>
    </Box>
  );
}

export default ManageProducts;