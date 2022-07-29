import { Card } from '@mui/material';
import { useEffect, useState } from 'react';

import UsersTable from './UsersTable';

import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import { UserInfo } from 'src/models/user_info';
import apiUrls from 'src/api/apiUrls';

function Users() {

  const updateUserList = () => {
    APICallWrapper(
      {
        url: apiUrls.usermanagement.getUsers,
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
