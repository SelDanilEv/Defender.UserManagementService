import { FC, ChangeEvent, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Tooltip,
  Divider,
  Box,
  FormControl,
  Card,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  TableContainer,
  Typography,
  useTheme,
  CardHeader,
  TextField,
  Grid
} from '@mui/material';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import { useNavigate } from 'react-router';

import DeleteUserDialog from '../Delete/DeleteUserModal';

import { UserInfo } from 'src/models/user_info';
import SuperAdminRoleLable from 'src/components/Label/RoleLabels/SuperAdmin';
import AdminRoleLable from 'src/components/Label/RoleLabels/Admin';
import UserRoleLable from 'src/components/Label/RoleLabels/User';


interface UsersTableProps {
  className?: string;
  users: UserInfo[];
  setNewUserList: Function;
}

interface Filters {
  key?: string;
}

const getRoleLabels = (userRoles: string[]) => {
  const map = {
    SuperAdmin: {
      label: (<SuperAdminRoleLable />)
    },
    Admin: {
      label: (<AdminRoleLable />)
    },
    User: {
      label: (<UserRoleLable />)
    }
  };

  return userRoles.map((role) => {

    const { label }: any = map[role];

    return (<Grid mr={0.5} display="inline-block" key={role}>{label}</Grid>);
  });
};

const applyFilters = (
  users: UserInfo[],
  filters: Filters
): UserInfo[] => {
  return users.filter((user) => {
    let matches = true;

    if (filters.key &&
      !user.id.includes(filters.key) &&
      !user.email.includes(filters.key) &&
      !user.name.includes(filters.key)) {
      matches = false;
    }

    return matches;
  });
};

const applyPagination = (
  users: UserInfo[],
  page: number,
  limit: number
): UserInfo[] => {
  return users.slice(page * limit, page * limit + limit);
};

const UsersTable: FC<UsersTableProps> = ({ users: users, setNewUserList: setNewUserList }) => {

  //Delete user

  const [openDeleteUserDialog, setOpenDeleteUserDialog] = useState(false);
  const [deleteUserId, setDeleteUserId] = useState("");

  const handleOpenDeleteDialog = (value) => {
    setDeleteUserId(value);
    setOpenDeleteUserDialog(true);
  };

  const handleCloseDeleteDialog = () => {
    setOpenDeleteUserDialog(false);
  };

  const handleSuccessDeleting = () => {
    setNewUserList(users.filter(u => u.id !== deleteUserId));
  }

  //-----------

  //Table settings

  const [page, setPage] = useState<number>(0);

  const [limit, setLimit] = useState<number>(5);

  const [filters, setFilters] = useState<Filters>({
    key: ""
  });

  const handleFilterChange = (e: ChangeEvent<HTMLInputElement>): void => {
    let value = e.target.value;

    setFilters((prevFilters) => ({
      ...prevFilters,
      key: value
    }));
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };

  const filteredUsers = applyFilters(users, filters);

  const paginatedUsers = applyPagination(
    filteredUsers,
    page,
    limit
  );

  //-----------


  const theme = useTheme();
  const navigate = useNavigate();

  return (
    <>
      <Card>
        <CardHeader
          action={
            <Box width={150}>
              <FormControl fullWidth variant="outlined">
                <TextField
                  onChange={handleFilterChange}
                  label="Filter"
                  value={filters.key}
                />
              </FormControl>
            </Box>
          }
          title="Users"
        />
        <Divider />
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Email</TableCell>
                <TableCell align="center">Roles</TableCell>
                <TableCell align="center">Creation date</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedUsers.map((user) => {
                return (
                  <TableRow
                    hover
                    key={user.id}
                  >
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {user.id}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {user.name}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {user.email}
                      </Typography>
                    </TableCell>
                    <TableCell align="center">
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {getRoleLabels(user.roles)}
                      </Typography>
                    </TableCell>
                    <TableCell align="center">
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {user.createdDate}
                      </Typography>
                    </TableCell>
                    <TableCell align="right">
                      <Tooltip title="Edit" arrow>
                        <IconButton
                          sx={{
                            '&:hover': {
                              background: theme.colors.primary.lighter
                            },
                            color: theme.palette.primary.main
                          }}
                          color="inherit"
                          size="small"
                          onClick={() => navigate("/user/edit", { state: { user: user } })}
                        >
                          <EditTwoToneIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Delete" arrow>
                        <IconButton
                          sx={{
                            '&:hover': { background: theme.colors.error.lighter },
                            color: theme.palette.error.main
                          }}
                          color="inherit"
                          size="small"
                          onClick={() => handleOpenDeleteDialog(user.id)}
                        >
                          <DeleteTwoToneIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <Box p={2}>
          <TablePagination
            component="div"
            count={filteredUsers.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={handleLimitChange}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[5, 10, 25, 30]}
          />
        </Box>
      </Card>

      <DeleteUserDialog
        userId={deleteUserId}
        open={openDeleteUserDialog}
        onClose={handleCloseDeleteDialog}
        onSuccess={handleSuccessDeleting}
      />
    </>
  );
};

UsersTable.propTypes = {
  users: PropTypes.array.isRequired
};

export default UsersTable;
