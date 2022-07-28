import {
  Box,
  Typography,
  Container,
  Button,
  styled
} from '@mui/material';
import { Helmet } from 'react-helmet-async';

const MainContent = styled(Box)(
  ({ theme }) => `
    margin-top: 24px;
    height: 100%;
    display: flex;
    flex: 1;
    overflow: auto;
    flex-direction: column;
    align-items: center;
    justify-content: center;
`
);

function Status404() {
  return (
    <>
      <Helmet>
        <title>Status - 404</title>
      </Helmet>
      <MainContent>
        <Container maxWidth="md">
          <Box textAlign="center">
            <img alt="404" height={180} src="/static/images/status/404.svg" />
            <Typography variant="h2" sx={{ my: 2 }}>
              The page you were looking for doesn't exist.
            </Typography>
            <Typography
              variant="h4"
              color="text.secondary"
              fontWeight="normal"
              sx={{ mb: 4 }}
            >
              It's on us, we moved the content to a different page.
            </Typography>
          </Box>
        </Container>
        <Button href="/" variant="outlined">
          Go to homepage
        </Button>
      </MainContent>
    </>
  );
}

export default Status404;
