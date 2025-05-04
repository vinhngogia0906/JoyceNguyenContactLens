import { useEffect, useState } from "react";
import { ADD_CONTACT_LENS_IMAGE, UPDATE_CONTACT_LENS } from "../api/graphql/mutations";
import { useMutation } from "@apollo/client";
import { Box, Button, Chip, CircularProgress, Dialog, DialogActions, DialogContent, DialogTitle, Tab, Tabs, TextField, Typography } from "@mui/material";
import { TabContext, TabPanel } from "@mui/lab";
import UploadIcon from '@mui/icons-material/Upload';
import SaveIcon from '@mui/icons-material/Save';
import { ContactLens } from "../api/types";
import { GET_CONTACT_LENS } from "../api/graphql/queries";

interface ProductDialogProps {
  open: boolean;
  product: ContactLens;
  onClose: () => void;
  onProductUpdated: (updatedProduct: any) => void;
}

const ProductDialog: React.FC<ProductDialogProps> = ({ 
  open, 
  product, 
  onClose,
  onProductUpdated
}) => {
  const [activeTab, setActiveTab] = useState('details');
  const [formData, setFormData] = useState({
    name: product.name,
    color: product.color,
    degree: product.degree.toString(),
    price: product.price.toString(),
    quantity: product.quantity.toString()
  });
  const [uploading, setUploading] = useState(false);
  const [selectedFiles, setSelectedFiles] = useState<File[]>([]);
  const [updateProduct] = useMutation(UPDATE_CONTACT_LENS);
  const [addImage] = useMutation(ADD_CONTACT_LENS_IMAGE, {
    refetchQueries: [{ query: GET_CONTACT_LENS }],
    context: {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    }
  });

  useEffect(() => {
    if(product) {
      setFormData({
        name: product.name,
        color: product.color,
        degree: product.degree.toString(),
        price: product.price.toString(),
        quantity: product.quantity.toString()
      })
    }
  }, [product]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async () => {
    try {
      const { data } = await updateProduct({
        variables: {
          id: product.id,
          color: formData.color,
          degree: parseFloat(formData.degree),
          name: formData.name,
          price: parseFloat(formData.price),
          quantity: parseInt(formData.quantity)
        }
      });
      
      onProductUpdated(data.updateContactLens);
      onClose();
    } catch (error) {
      console.error("Error updating product:", error);
    }
  };

  const handleFileSelection = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (!files || files.length === 0) return;
    setSelectedFiles(Array.from(files));
  };

  const handleSaveImages = async () => {
    if (selectedFiles.length === 0) return;

    setUploading(true);
    try {
      await Promise.all(
        selectedFiles.map(file => 
          addImage({
            variables: {
              id: product.id,
              imageFile: file
            }
          })
        )
      );
      setSelectedFiles([]);
      onProductUpdated(product);
    } catch (error) {
      console.error("Error uploading images:", error);
    } finally {
      setUploading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      <DialogTitle>Edit Product</DialogTitle>
      <DialogContent>
        <TabContext value={activeTab}>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Tabs 
              value={activeTab} 
              onChange={(_, newValue) => setActiveTab(newValue)}
            >
              <Tab label="Details" value="details" />
              <Tab label="Images" value="images" />
            </Tabs>
          </Box>

          <TabPanel value="details">
            <TextField
              fullWidth
              margin="normal"
              label="Name"
              name="name"
              value={formData.name}
              onChange={handleChange}
              required
            />
            <TextField
              fullWidth
              margin="normal"
              label="Color"
              name="color"
              value={formData.color}
              onChange={handleChange}
              required
            />
            <TextField
              fullWidth
              margin="normal"
              label="Degree"
              name="degree"
              type="number"
              value={formData.degree}
              onChange={handleChange}
              inputProps={{ step: "0.25" }}
              required
            />
            <TextField
              fullWidth
              margin="normal"
              label="Price"
              name="price"
              type="number"
              value={formData.price}
              onChange={handleChange}
              inputProps={{ step: "0.01" }}
              required
            />
            <TextField
              fullWidth
              margin="normal"
              label="Quantity"
              name="quantity"
              type="number"
              value={formData.quantity}
              onChange={handleChange}
              required
            />
            <Button 
              onClick={handleSubmit} 
              color="primary" 
              variant="contained"
            >
              Save Changes
            </Button>
          </TabPanel>

          <TabPanel value="images">
            <Box sx={{ mb: 3 }}>
              <input
                accept="image/*"
                style={{ display: 'none' }}
                id="product-image-upload"
                type="file"
                multiple
                onChange={handleFileSelection}
              />
              <label htmlFor="product-image-upload">
                <Button
                  variant="contained"
                  component="span"
                  startIcon={<UploadIcon />}
                  disabled={uploading}
                >
                  {uploading ? <CircularProgress size={24} /> : 'Upload Images'}
                </Button>
              </label>
              <Button
                variant="contained"
                color="primary"
                onClick={handleSaveImages}
                disabled={uploading || selectedFiles.length === 0}
                startIcon={uploading ? <CircularProgress size={24}/> : <SaveIcon/>}
                sx={{ml:2}}
              >
                Save Images
              </Button>
              <Typography variant="body2" sx={{ mt: 1 }}>
                You can upload multiple images at once
              </Typography>
            </Box>

            {product.images && product.images.length > 0 ? (
              <Box sx={{ 
                display: 'grid',
                gridTemplateColumns: 'repeat(auto-fill, minmax(150px, 1fr))',
                gap: 2
              }}>
                {product.images.map((image) => (
                  <Box key={image.id} sx={{ position: 'relative' }}>
                    <Box
                      component="img"
                      src={image.url}
                      alt="Product"
                      sx={{ 
                        width: '100%',
                        height: 150,
                        objectFit: 'cover',
                        borderRadius: 1
                      }}
                    />
                    <Box sx={{ 
                      position: 'absolute',
                      top: 8,
                      right: 8,
                      display: 'flex',
                      gap: 1
                    }}>
                      <Chip
                        label="Thumbnail"
                        size="small"
                        color="primary"
                        sx={{ visibility: 'hidden' }} // Add logic to show for thumbnail
                      />
                    </Box>
                  </Box>
                ))}
              </Box>
            ) : (
              <Typography variant="body1" color="text.secondary">
                No images uploaded yet
              </Typography>
            )}
          </TabPanel>
        </TabContext>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductDialog;