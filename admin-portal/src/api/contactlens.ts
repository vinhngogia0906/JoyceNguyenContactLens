import { UUID } from 'crypto';
import client from './graphql/client';
import { GET_CONTACT_LENS, GET_CONTACT_LENS_BY_ID } from './graphql/queries';
import { ContactLens } from './types';

export const contactLensService = {
  getAllContactLens: async (): Promise<ContactLens[]> => {
    const { data } = await client.query({
      query: GET_CONTACT_LENS,
      fetchPolicy: 'network-only' // Optional: Skip cache
    });
    return data.allContactLenses;
  },

  getContactLensById: async (id: UUID): Promise<ContactLens> => {
    const { data } = await client.query({
      query: GET_CONTACT_LENS_BY_ID,
      variables: { id }
    });
    return data.contactLensById;
  }
};