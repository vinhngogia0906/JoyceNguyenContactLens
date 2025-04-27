interface AppConfig {
  graphqlUri: string;
  jwtToken: string;
}

const env: AppConfig = {
  graphqlUri: process.env.GRAPHQL_URI || 'https://localhost:7055/graphql',
  jwtToken: ''
};

export default env;