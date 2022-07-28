import {
  Box,
  Typography,
  Tooltip,
  IconButton,
  Grid,
  Button,
  Divider,
  Card
} from '@mui/material';

import ArrowBackTwoToneIcon from '@mui/icons-material/ArrowBackTwoTone';
import SaveIcon from '@mui/icons-material/Save';
import { useLocation, useNavigate } from 'react-router';
import { UserInfo } from 'src/models/user_info';
import { useState } from 'react';
import EditFromAdmin from './EditFromAdmin';
import APICallWrapper from 'src/helpers/APIWrapper/APICallWrapper';
import EditFromSuperAdmin from './EditFromSuperAdmin';
import { connect } from 'react-redux';

interface UserState {
  user: UserInfo
}

const UpdateUserPage = (props: any) => {
  const navigate = useNavigate();
  const { state } = useLocation();

  const [user, setUser] = useState<UserInfo>((state as UserState).user);

  const handleUpdateUser = () => {
    const requestBody = {
      user: user
    }

    requestBody.user.createdDate = null;

    APICallWrapper(
      {
        url: "/api/UserManagement/admin/user",
        options: {
          method: 'PUT',
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(requestBody),
        },
        onSuccess: async (response) => {
          navigate("/user")
        }
      }
    )
  }

  const editUserInfo = (user: UserInfo) => {
    setUser(user);
  }

  return (
    <>
      <Box display="flex" mb={3}>
        <Tooltip arrow placement="top" title="Go back">
          <IconButton onClick={() => navigate("/user")} color="primary" sx={{ p: 2, mr: 2 }}>
            <ArrowBackTwoToneIcon />
          </IconButton>
        </Tooltip>
        <Grid container alignContent={"center"}>
          <Typography variant="h3" component="h3">
            Update user <i>{user.id}</i>
          </Typography>
        </Grid>
      </Box>

      <Grid item xs={12}>
        <Card>
          <Box
            p={3}
            display="flex"
            alignItems="center"
            justifyContent="space-between"
          >
            <Box>
              <Typography variant="h3">
                User info
              </Typography>
            </Box>
            <Button onClick={() => handleUpdateUser()} variant="text" startIcon={<SaveIcon />}>
              Save
            </Button>
          </Box>
          <Divider />
          {
            props.currentUser.roles.indexOf("SuperAdmin") > -1
              ? <EditFromSuperAdmin user={user} updateUser={editUserInfo} />
              : <EditFromAdmin user={user} updateUser={editUserInfo} />
          }
        </Card>
      </Grid>
    </>
  );
}

const mapStateToProps = (state: any) => {
  return {
    currentUser: state.auth.user
  };
};


export default
  connect(mapStateToProps)
    (UpdateUserPage);
