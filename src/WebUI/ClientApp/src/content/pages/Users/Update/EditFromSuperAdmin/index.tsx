import {
  Typography,
  Grid,
  CardContent,
  Checkbox,
  TextField,
  Divider
} from '@mui/material';

import Text from 'src/components/Text';

const EditFromSuperAdmin = (props: any) => {

  let user = props.user

  const RenderIsAdminFlag = () => {
    return (<Checkbox
      id="role"
      onChange={UpdateUserRoles}
      defaultChecked={user.roles.indexOf("Admin") > -1}
      sx={{ padding: 0 }} />)
  }

  const UpdateUserRoles = (event) => {
    if (event.target.checked) {
      user.roles.push("Admin");
    }
    else {
      user.roles = user.roles.filter(e => e !== "Admin")
    }

    props.updateUser(user);
  }

  const UpdateUser = (event) => {
    user[event.target.id] = event.target.value;
    props.updateUser(user);
  }

  return (
    <>
      <CardContent sx={{ p: 4 }}>
        <Typography variant="subtitle2">
          <Grid container spacing={0}>
            <Grid container item xs={12} sm={4} md={3} alignContent={"center"} justifyContent={{ xs: "left", sm: "center" }}>
              <Grid>
                Name:
              </Grid>
            </Grid>
            <Grid item xs={12} sm={6} md={7}>
              <TextField
                id='name'
                sx={{ padding: 0 }}
                defaultValue={user.name}
                onChange={UpdateUser}
                variant="standard"
                fullWidth
              />
            </Grid>
            <Grid item xs={12} pt={1} pb={1}>
              <Divider />
            </Grid>
            <Grid container item xs={12} sm={4} md={3} alignContent={"center"} justifyContent={{ xs: "left", sm: "center" }}>
              <Grid>
                Email:
              </Grid>
            </Grid>
            <Grid item xs={12} sm={6} md={7}>
              <TextField
                id='email'
                sx={{ padding: 0 }}
                defaultValue={user.email}
                onChange={UpdateUser}
                variant="standard"
                fullWidth
              />
            </Grid>
            <Grid item xs={12} pt={1} pb={1}>
              <Divider />
            </Grid>
            <Grid container item xs={12} sm={4} md={3} alignContent={"center"} justifyContent={{ xs: "left", sm: "center" }}>
              <Grid>
                Is Admin:
              </Grid>
            </Grid>
            <Grid item xs={12} sm={8} md={9}>
              {RenderIsAdminFlag()}
            </Grid>
            <Grid item xs={12} pt={1} pb={1}>
              <Divider />
            </Grid>
            <Grid container item xs={12} sm={4} md={3} alignContent={"center"} justifyContent={{ xs: "left", sm: "center" }}>
              <Grid>
                Created date:
              </Grid>
            </Grid>
            <Grid item xs={12} sm={8} md={9}>
              <Text color="black">
                {user.createdDate}
              </Text>
            </Grid>
          </Grid>
        </Typography>
      </CardContent>
    </>
  );
}

export default EditFromSuperAdmin;
