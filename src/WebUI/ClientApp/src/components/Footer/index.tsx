import { Box, Container, Link, Typography, styled } from '@mui/material';

import config from 'src/config.json';

const FooterWrapper = styled(Container)(
  ({ theme }) => `
        margin-top: ${theme.spacing(4)};
`
);

function Footer() {
  return (
    <FooterWrapper className="footer-wrapper">
      <Box
        pb={4}
        display={{ xs: 'block', md: 'flex' }}
        alignItems="center"
        textAlign={{ xs: 'center', md: 'left' }}
        justifyContent="space-between"
      >
        <Box>
          <Typography variant="subtitle1">
            &copy; 2022 - {config.NAME_OF_APP}
          </Typography>
        </Box>
        <Typography
          sx={{
            pt: { xs: 2, md: 0 }
          }}
          variant="subtitle1"
        >
          Created by{' '}
          <Link
            href="https://github.com/SelDanilEv"
            target="_blank"
            rel="noopener noreferrer"
          >
            Defender
          </Link>
        </Typography>
      </Box>
    </FooterWrapper>
  );
}

export default Footer;
