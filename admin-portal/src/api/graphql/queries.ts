import {gql} from '@apollo/client';

export const GET_CONTACT_LENS = gql`
  query GetAllContactLens {
    allContactLenses {
        id
        name
        color
        degree
        price
        quantity
    }
  }
`;

export const GET_CONTACT_LENS_BY_ID = gql`
  query GetContactLensById($contactLensId: UUID!) {
    contactLensById(id: $contactLensId) {
        id
        name
        color
        degree
        price
        quantity
    }
  }
`;