import {gql} from '@apollo/client';

export const ADMINLOGIN = gql`
  mutation AdminLogin($email: String!, $password: String!) {
    adminLogin(email: $email, password: $password)
  }
`;