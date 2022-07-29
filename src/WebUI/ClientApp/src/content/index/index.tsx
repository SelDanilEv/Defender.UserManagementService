import { Box, Container, Card } from '@mui/material';
import { Helmet } from 'react-helmet-async';
import { styled } from '@mui/material/styles';

import Login from './Login';

import Logo from 'src/components/LogoSign';

const OverviewWrapper = styled(Box)(
  () => `
    overflow: auto;
    flex: 1;
    overflow-x: hidden;
    align-items: center;
`
);

const StartPage = (props: any) => {
  return (
    <OverviewWrapper>
      <Helmet>
        <title>Home</title>
      </Helmet>
      <Container maxWidth="lg">
        <Box display="flex" justifyContent="center" py={5} alignItems="center">
          <Logo />
        </Box>
        <Box display="flex" justifyContent="center" alignItems="center">
          <Card sx={{ p: 5, mb: 1, width: "80%", borderRadius: 10 }}>
            <Login />
          </Card>
        </Box>
      </Container>
    </OverviewWrapper>
  );
}

export default StartPage;

