import React from 'react';
import { FC } from 'react';
import PropTypes from 'prop-types';
import { Outlet, useNavigate } from 'react-router-dom';
import { Box } from '@mui/material';
import { connect } from 'react-redux';

const BaseLayout: FC = (props: any) => {
  const navigate = useNavigate();

  React.useEffect(() => {

    if (props.auth.isAuthenticated) {
      navigate("/home")
    }
  }, []);

  return (
    <Box
      sx={{
        flex: 1,
        height: '100%'
      }}
    >
      <Outlet />
    </Box>
  );
};

BaseLayout.propTypes = {
  children: PropTypes.node
};

const mapStateToProps = (state: any) => {
  return {
    auth: state.auth
  };
};

export default
  connect(mapStateToProps)
    (BaseLayout);
