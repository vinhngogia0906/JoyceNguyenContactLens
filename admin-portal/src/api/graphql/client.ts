import { ApolloClient, InMemoryCache, createHttpLink } from "@apollo/client";
import { setContext } from '@apollo/client/link/context';
import env from "../../environment";


const httpLink = createHttpLink({
  uri: env.graphqlUri,
});

const authLink = setContext((_, { headers }) => {
  const tokenKey = localStorage.getItem(env.jwtToken);

  return {
    headers: {
      ...headers,
      authorization: tokenKey ? `Bearer ${tokenKey}` : "",
    }
  };
})

const client = new ApolloClient({
  link: authLink.concat(httpLink),
  cache: new InMemoryCache()
});

export default client;