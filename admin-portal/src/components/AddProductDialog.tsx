import { useState } from "react";
import { ContactLens } from "../api/types";
import { useMutation } from "@apollo/client";
import { CREATE_CONTACT_LENS } from "../api/graphql/mutations";
import { Dialog, DialogTitle, DialogContent, TextField, DialogActions, Button, CircularProgress } from "@mui/material";
import { GET_CONTACT_LENS } from "../api/graphql/queries";

interface AddProductDialogProps {
  open: boolean;
  onClose: () => void;
  onProductCreated: (product: ContactLens) => void;
}

const AddProductDialog : React.FC<AddProductDialogProps> = ({
  open,
  onClose,
  onProductCreated
}) => {
  const [formData, setFormData] = useState({
    name: '',
    color: '',
    degree: '0.0',
    price: '0.0',
    quantity: '0'
  });

  const [createProduct, {loading}] = useMutation(CREATE_CONTACT_LENS, {
    refetchQueries: [{ query: GET_CONTACT_LENS }]
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const {name, value} = e.target;
    setFormData(prev => ({...prev, [name]: value}));
  };

  const handleSubmit = async () => {
    try {
      const {data} = await createProduct({
        variables: {
          color: formData.color,
          name: formData.name,
          degree: parseFloat(formData.degree),
          price: parseFloat(formData.price),
          quantity: parseInt(formData.quantity)
        }
      });

      onProductCreated(data.addContactLens);
      onClose();
    } catch(error) {
      console.error("Error creating product: ", error);
    }
  };
  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Add New Contact Lens</DialogTitle>
      <DialogContent>
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
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button 
          onClick={handleSubmit} 
          color="primary" 
          variant="contained"
          disabled={loading}
        >
          {loading ? <CircularProgress size={24} /> : 'Add Product'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddProductDialog;