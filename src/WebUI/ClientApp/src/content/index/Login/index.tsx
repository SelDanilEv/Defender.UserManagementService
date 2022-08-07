import { Box, Container, Grid, Typography, Avatar } from '@mui/material';
import { useNavigate } from "react-router-dom";
import { connect } from "react-redux";
import { useGoogleLogin } from '@react-oauth/google';
import { styled } from '@mui/material/styles';

import LockedButton from 'src/components/LockedComponents/Buttons/LockedButton';
import LoadingStateService from 'src/services/LoadingStateService';
import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import ErrorToast from 'src/components/Toast/DefaultErrorToast';
import config from 'src/config.json';
import { login } from "src/actions/authActions";
import apiUrls from 'src/api/apiUrls';

const TypographyH1 = styled(Typography)(
  ({ theme }) => `
    font-size: ${theme.typography.pxToRem(40)};
`
);

const TypographyH2 = styled(Typography)(
  ({ theme }) => `
    font-size: ${theme.typography.pxToRem(17)};
`
);

const sizeOfLoginButtonText = 25;

const LoginButton = styled(LockedButton)(
  ({ theme }) => `
   display: 'flex';
   justifyContent: 'center';
   alignItems: 'center';
   font-size: ${theme.typography.pxToRem(sizeOfLoginButtonText)};
`
);

const GLetter = styled(Avatar)(
  ({ theme }) => `
   height: ${theme.typography.pxToRem(sizeOfLoginButtonText)};
   width: ${theme.typography.pxToRem(sizeOfLoginButtonText)};
   margin-right: 1px;
`
);


const Login = (props: any) => {

  let googleResponseTimeout;

  const navigate = useNavigate();

  const loginGoogle = useGoogleLogin({
    onSuccess: tokenResponse => googleResponse(tokenResponse),
    onError: errorResponse => googleResponse(errorResponse),
  });

  const login = () => {
    LoadingStateService.StartLoading()
    loginGoogle();
    googleResponseTimeout = setTimeout(LoadingStateService.FinishLoading, 10 * 1000)
  }

  const googleResponse = async (gResponse: any) => {

    if (!gResponse.access_token) {
      ErrorToast("Google account details not available");
      return;
    }

    clearTimeout(googleResponseTimeout);

    const requestData = {
      Token: gResponse.access_token,
    };

    APICallWrapper(
      {
        url: apiUrls.auth.google,
        options: {
          method: 'POST',
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(requestData),
          cache: 'default'
        },
        onSuccess: async (response) => {
          const loginResponse = await response.json();

          if (!loginResponse.authorized) {
            ErrorToast("Error during authorization");
            return;
          }

          const authState = {
            token: loginResponse.token,
            user: loginResponse.userDetails
          }

          props.login(authState);

          navigate("/home")
        },
        onFailure: async (response) => {
          if (response.status == 401) {
            props.logout();
            navigate("/");
          }
        },
        onFinal: async () => {
          // Custom unblock
          LoadingStateService.FinishLoading()
        },
      }
    )
  };

  return (
    <Container maxWidth="lg" sx={{ textAlign: 'center' }}>
      <Grid
        spacing={{ xs: 6, md: 10 }}
        justifyContent="center"
        alignItems="center"
        container
      >
        <Grid item md={10} lg={8} mx="auto">
          <TypographyH1 sx={{ mb: 2 }} variant="h1">
            {config.NAME_OF_APP}
          </TypographyH1>
          <TypographyH2
            sx={{ lineHeight: 1.5, pb: 4 }}
            variant="h4"
            color="text.secondary"
            fontWeight="normal"
          >
            {config.APP_DESCRIPTION}
          </TypographyH2>
          <LoginButton
            variant="outlined"
            onClick={login}
          >
            <Box sx={{ display: { xs: 'none', sm: 'none', md: 'block' } }}>Sign in with&nbsp;</Box>
            <GLetter
              src="/static/images/logo/google.svg"
              alt=""
            />
            oogle
          </LoginButton>
        </Grid>
      </Grid>
    </Container >
  );
}

const mapStateToProps = (state: any) => {
  return {
    isAuthenticated: state.auth.isAuthenticated
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    login: (payload: any) => {
      dispatch(login(payload));
    }
  }
};

export default
  connect(mapStateToProps, mapDispatchToProps)
    (Login);
