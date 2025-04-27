import { useMutation, useQuery } from "@apollo/client";
import { GET_CONTACT_LENS } from "../../api/graphql/queries";
import { Box, Button, Card, CardContent, CardMedia, CircularProgress, Grid, Typography } from "@mui/material";
import NavBar from "../../components/common/NavigationBar";
import { useState } from "react";
import { CREATE_CONTACT_LENS } from "../../api/graphql/mutations";
import { ContactLens } from "../../api/types";
import AddProductDialog from "../../components/AddProductDialog";
import ProductDialog from "../../components/ProductDialog";

const ManageProducts = () => {
  const { loading, error, data } = useQuery(GET_CONTACT_LENS);
  const [createProduct] = useMutation(CREATE_CONTACT_LENS, {
    refetchQueries: [{ query: GET_CONTACT_LENS }]
  });
  const [selectedProduct, setSelectedProduct] = useState<ContactLens>({
    id: '00000000-00000000-00000000-00000000-00000000',
    name: '',
    color: '',
    degree: 1.0,
    price: 0.0,
    quantity: 0
  });
  const [openProductDialog, setOpenProductDialog] = useState(false);
  const [openAddDialog, setOpenAddDialog] = useState(false);

  const handleAddProductClick = () => {
    setOpenAddDialog(true);
  };

  const handleProductCreated = (newProduct: ContactLens) => {
    setSelectedProduct(newProduct);
  };

  const handleProductUpdate = (newProduct: ContactLens) => {
    setSelectedProduct(newProduct);
  };

  const handleSelectedProductRefresh = (newProduct: ContactLens) => {
    setSelectedProduct(new)
  }

  if (loading) return <CircularProgress />;
  if (error) return <Typography color="error">Error loading products</Typography>;

  return (
    <Box sx={{ p: 3 }}>
      <NavBar title="Manage Products"/>
      <Box sx={{ display: 'flex', justifyContent: 'flex-end', mb: 2 }}>
        <Button 
          variant="contained" 
          color="primary" 
          onClick={handleAddProductClick}
        >
          Add Product
        </Button>
      </Box>

      <Grid container spacing={3}>
        {data.allContactLenses.map((lens: ContactLens) => (
          <Grid size={{xs: 6, sm: 6, md:4}} key={lens.id} component='div'>
            <Card 
              sx={{ cursor: 'pointer' }}
              onClick={() => {
                setSelectedProduct(lens);
                setOpenProductDialog(true);
                console.log("Selected Products: ", selectedProduct);
              }}
            >
              {/* {lens.thumbnail && (
                <CardMedia
                  component="img"
                  height="140"
                  image={product.thumbnail}
                  alt={product.name}
                />
              )} */}
              <CardContent>
                <Typography gutterBottom variant="h5">
                  {lens.name}
                </Typography>
                <Typography color="text.secondary">
                  Color: {lens.color}
                </Typography>
                <Typography color="text.secondary">
                  Price: ${lens.price.toFixed(2)}
                </Typography>
                <Typography color={lens.quantity > 0 ? 'text.secondary' : 'error'}>
                  Stock: {lens.quantity}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <AddProductDialog
        open={openAddDialog}
        onClose={() => setOpenAddDialog(false)}
        onProductCreated={handleProductCreated}
      />
      <ProductDialog
        open={openProductDialog}
        product={selectedProduct}
        onClose={() => setOpenProductDialog(false)}
        onProductUpdated={handleProductCreated}
        onSelectedProductRefresh={}
      />
    </Box>
  );
};

export default ManageProducts;