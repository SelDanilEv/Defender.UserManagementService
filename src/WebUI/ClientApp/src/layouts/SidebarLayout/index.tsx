import React from 'react';
import { FC, ReactNode } from 'react';
import { Box, alpha, lighten, useTheme } from '@mui/material';
import { Outlet } from 'react-router-dom';

import Sidebar from './Sidebar';
import Header from './Header';
import { connect } from 'react-redux';

import { useNavigate } from "react-router-dom";
import APICallWrapper from 'src/helpers/APIWrapper/APICallWrapper';
import { logout } from 'src/actions/authActions';


interface SidebarLayoutProps {
  children?: ReactNode;
}

const SidebarLayout: FC<SidebarLayoutProps> = (props: any) => {
  const theme = useTheme();
  const navigate = useNavigate();

  React.useEffect(() => {
    APICallWrapper(
      {
        url: "api/home",
        options: {
          method: 'GET'
        },
        onFailure: async (response) => {
          if (response.status == 401) {
            props.logout();
            navigate("/");
          }
        }
      }
    )
  }, []);

  return (
    <>
      <Box
        sx={{
          flex: 1,
          height: '100%',

          '.MuiPageTitle-wrapper': {
            background:
              theme.palette.mode === 'dark'
                ? theme.colors.alpha.trueWhite[5]
                : theme.colors.alpha.white[50],
            marginBottom: `${theme.spacing(4)}`,
            boxShadow:
              theme.palette.mode === 'dark'
                ? `0 1px 0 ${alpha(
                  lighten(theme.colors.primary.main, 0.7),
                  0.15
                )}, 0px 2px 4px -3px rgba(0, 0, 0, 0.2), 0px 5px 12px -4px rgba(0, 0, 0, .1)`
                : `0px 2px 4px -3px ${alpha(
                  theme.colors.alpha.black[100],
                  0.1
                )}, 0px 5px 12px -4px ${alpha(
                  theme.colors.alpha.black[100],
                  0.05
                )}`
          }
        }}
      >
        <Header />
        <Sidebar />
        <Box
          sx={{
            position: 'relative',
            zIndex: 5,
            display: 'block',
            flex: 1,
            pt: `${theme.header.height}`,
            [theme.breakpoints.up('lg')]: {
              ml: `${theme.sidebar.width}`
            }
          }}
        >
          <Box display="block" p={1}>
            <Outlet />
          </Box>
        </Box>
      </Box>
    </>
  );
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    logout: () => {
      dispatch(logout());
    }
  }
};

export default
  connect(null, mapDispatchToProps)
    (SidebarLayout);

