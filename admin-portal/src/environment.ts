interface AppConfig {
  graphqlUri: string;
  jwtToken: string;
}

const env: AppConfig = {
  graphqlUri: process.env.GRAPHQL_URI || 'http://localhost:8080/graphql',
  jwtToken: ''
};

export default env;