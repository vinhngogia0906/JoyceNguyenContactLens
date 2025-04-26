import { useQuery } from "@apollo/client";
import { GET_ORDERS } from "../../api/graphql/queries";
import { Box, Container, Typography, CircularProgress, Alert, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody } from "@mui/material";
import NavBar from "../../components/common/NavigationBar";
import { Order } from "../../api/types";


const ManageOrders: React.FC = () => {
  const {loading, error, data } = useQuery(GET_ORDERS);

  return(
    <Box sx={{display: 'flex', flexDirection: 'column', minHeight:'100vh'}}>
      <NavBar title="Joyce Nguyen Admin"/>
      <Container maxWidth='lg' sx={{py: 4, flexGrow: 1}}>
        <Typography variant="h4" gutterBottom sx={{mb:3}}>
          Manage Orders
        </Typography>
        {loading && (
          <Box sx={{display: 'flex', justifyContent:'center', mt:4}}>
            <CircularProgress/>
          </Box>
        )}

        {error && (
          <Alert severity="error" sx={{mb:3}}>
            Error Loading orders: {error.message}
          </Alert>
        )}
        {data && (
          <TableContainer component={Paper} elevation={3}>
            <Table sx={{minWidth: 650}} aria-label="contact lenses table">
              <TableHead>
                <TableRow sx={{backgroundColor: 'primary.light'}}>
                  <TableCell sx={{fontWeight:'bold'}}>ID</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Order Date</TableCell>
                  <TableCell sx={{fontWeight:'bold'}}>Total Price</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.allOrders.map((order: Order) => (
                  <TableRow
                    key={order.id}
                    sx={{'&:last-child td, &:last-child th' : {border: 0}}}
                  >
                    <TableCell>{order.id}</TableCell>
                    <TableCell>{order.orderDate.toDateString()}</TableCell>
                    <TableCell>{order.totalPrice}</TableCell>
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

export default ManageOrders;