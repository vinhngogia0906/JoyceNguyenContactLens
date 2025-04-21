import env from "../environment";
import client from "./graphql/client";
import { LOGIN } from "./graphql/mutations";

export const AuthService = {
  login: async (email: string, password: string): Promise<string> => {
    const {data} = await client.mutate({
      mutation: LOGIN,
      variables: { email, password }
    });

    const token = data.login.token;
    localStorage.setItem(env.jwtToken, token);
    return token;
  },

  logout: (): void => {
    localStorage.removeItem(env.jwtToken);
    client.resetStore();
  },

  getAuthToken: (): string | null => {
    return localStorage.getItem(env.jwtToken);
  },

  isAuthenticated: (): boolean => {
    return !!localStorage.getItem(env.jwtToken);
  }
}