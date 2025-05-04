import { ApolloClient, InMemoryCache, createHttpLink, split } from "@apollo/client";
import { setContext } from '@apollo/client/link/context';
import env from "../../environment";
import createUploadLink  from 'apollo-upload-client/createUploadLink.mjs';


const httpLink = createHttpLink({
  uri: env.graphqlUri,
});

const tokenKey = localStorage.getItem(env.jwtToken);

const uploadLink = createUploadLink({
  uri: env.graphqlUri,
  headers: {
    'Apollo-Require-Preflight': 'true',
    'GraphQL-preflight': '1'
  }
});

const authLink = setContext((_, { headers }) => {
  return {
    headers: {
      ...headers,
      authorization: tokenKey ? `Bearer ${tokenKey}` : "",
    }
  };
})

const link = split(
  // Check if the operation contains files
  ({ variables }) => {
    // Helper function to check for File objects
    const hasFiles = (obj: any): boolean => {
      if (!obj) return false;
      if (obj instanceof File) return true;
      
      if (typeof obj === 'object') {
        return Object.values(obj).some(hasFiles);
      }
      
      return false;
    };
    
    return hasFiles(variables);
  },
  authLink.concat(uploadLink), // Use uploadLink for file operations
  authLink.concat(httpLink)   // Use httpLink for regular operations
);

const client = new ApolloClient({
  link: link,
  cache: new InMemoryCache()
});

export default client;