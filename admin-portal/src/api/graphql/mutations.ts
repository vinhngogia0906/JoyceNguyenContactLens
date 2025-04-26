import {gql} from '@apollo/client';

export const ADMINLOGIN = gql`
  mutation AdminLogin($email: String!, $password: String!) {
    adminLogin(email: $email, password: $password)
  }
`;

export const CREATE_CONTACT_LENS = gql`
mutation AddContactLens(
  $color: String!,
  $degree: Decimal!,
  $name: String!,
  $price: Decimal!,
  $quantity: Int!,) {
  addContactLens(contactLensRequest: {
      color: $color,
      degree: $degree,
      name: $name,
      price: $price,
      quantity: $quantity
  }) {
    id
    name
    color
    degree
    price
    quantity
  }
}
`;

export const UPDATE_CONTACT_LENS = gql`
mutation UpdateContactLens($id: UUID!,
  $color: String!,
  $degree: Decimal!,
  $name: String!,
  $price: Decimal!,
  $quantity: Int!) {
  updateContactLens(id: $id, contactLensRequest: {
      color: $color,
      degree: $degree,
      name: $name,
      price: $price,
      quantity: $quantity
  }) {
    id
    name
    color
    degree
    price
    quantity
  }
}
`;

export const ADD_CONTACT_LENS_IMAGE = gql`
mutation AddContactLensImage($id: UUID!, $imageFile: Upload!){
  addContactLensImage(contactLensId: $id, imageFile: $imageFile)
}
`;