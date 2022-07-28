import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { UserInfo } from 'src/models/user_info';
import UsersTable from './UsersTable';
import APICallWrapper from 'src/helpers/APIWrapper/APICallWrapper';

function Users() {

  const updateUserList = () => {
    APICallWrapper(
      {
        url: "api/UserManagement/admin/users",
        options: {
          method: 'GET'
        },
        onSuccess: async (response) => {
          let allUsers = await response.json();

          setUsers(allUsers);
        }
      }
    )
  }

  const setNewUserList = (users: UserInfo[]) => {
    setUsers(users)
  }

  const [users, setUsers] = useState<UserInfo[]>([]);

  useEffect(() => {
    updateUserList();
  }, [])

  return (
    <Card>
      <UsersTable users={users} setNewUserList={setNewUserList} />
    </Card>
  );
}

export default Users;
