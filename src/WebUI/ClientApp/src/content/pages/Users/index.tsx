import { Grid } from '@mui/material';

import Users from './Table';

const UsersPage = () => {
  return (
    <>
      <Grid
        container
        direction="row"
        justifyContent="center"
        alignItems="stretch"
        spacing={3}
      >
        <Grid item xs={12}>
          <Users />
        </Grid>
      </Grid>
    </>
  );
}

export default UsersPage;
