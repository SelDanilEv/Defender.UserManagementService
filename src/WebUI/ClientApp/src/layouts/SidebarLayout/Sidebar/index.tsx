import {
  Box,
  Drawer,
  alpha,
  styled,
  Divider,
  useTheme,
  lighten,
  darken,
} from '@mui/material';
import { useContext } from 'react';

import SidebarMenu from './SidebarMenu';

import Logo from 'src/components/LogoSign';
import config from 'src/config.json';
import Scrollbar from 'src/components/Scrollbar';
import { SidebarContext } from 'src/contexts/SidebarContext';


const SidebarWrapper = styled(Box)(
  ({ theme }) => `
        width: ${theme.sidebar.width};
        min-width: ${theme.sidebar.width};
        color: ${theme.colors.alpha.trueWhite[70]};
        position: relative;
        z-index: 7;
        height: 100%;
`
);

const SidebarLogo = () => {
  return (
    <Box
      mx={2}
      sx={{
        width: 270,
        height: 70,
        display: "flex",
      }}
    >
      <Logo height={75} width={75} />
      <Box
        marginLeft={3}
        paddingRight={3}
        fontSize={17}
        sx={{
          display: "inline"
        }}
      >
        {config.NAME_OF_APP}
      </Box>
    </Box>
  )
}

const SideScrollbar = () => {
  const theme = useTheme();

  return (
    <Scrollbar>
      <Box mt={3}>
        <SidebarLogo />
      </Box>
      <Divider
        sx={{
          mt: theme.spacing(3),
          mx: theme.spacing(2),
          overflowX: "hidden",
          background: theme.colors.alpha.trueWhite[10]
        }}
      />
      <SidebarMenu />
    </Scrollbar>
  )
}

function Sidebar() {
  const { sidebarToggle, toggleSidebar } = useContext(SidebarContext);
  const closeSidebar = () => toggleSidebar();
  const theme = useTheme();

  return (
    <>
      <SidebarWrapper
        sx={{
          display: {
            xs: 'none',
            lg: 'inline-block'
          },
          position: 'fixed',
          left: 0,
          top: 0,
          background:
            theme.palette.mode === 'dark'
              ? alpha(lighten(theme.header.background, 0.1), 0.5)
              : darken(theme.colors.alpha.black[100], 0.5),
          boxShadow:
            theme.palette.mode === 'dark' ? theme.sidebar.boxShadow : 'none'
        }}
      >
        <SideScrollbar />
      </SidebarWrapper>
      <Drawer
        sx={{
          boxShadow: `${theme.sidebar.boxShadow}`
        }}
        anchor={theme.direction === 'rtl' ? 'right' : 'left'}
        open={sidebarToggle}
        onClose={closeSidebar}
        variant="temporary"
        elevation={9}
      >
        <SidebarWrapper
          sx={{
            background:
              theme.palette.mode === 'dark'
                ? theme.colors.alpha.white[100]
                : darken(theme.colors.alpha.black[100], 0.5)
          }}
        >
          <SideScrollbar />
        </SidebarWrapper>
      </Drawer>
    </>
  );
}

export default Sidebar;
