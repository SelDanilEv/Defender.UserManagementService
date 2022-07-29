import { UserInfo } from "src/models/user_info";

const UserService = {
    FromAuthUserToUser: (authUser: UserInfo) => {
        return {
            id: authUser.id,
            name: authUser.name,
            email: authUser.email,
            avatar: '/',
            role: UserService.GetHighestRole(authUser.roles),
            createdDate: authUser.createdDate
        }
    },

    GetHighestRole: (roles: string[]): string => {
        let defaultRole = { key: "User", value: "User" }
        let roleList = [
            { key: "SuperAdmin", value: "Super Admin" },
            { key: "Admin", value: "Admin" },
            defaultRole];

        for (
            let i = 0, role = {} as { key: string, value: string };
            i < roleList.length && roles;
            role = roleList[i++]) {
            if (roles.includes(role.key)) {
                return role.value;
            }
        }

        return defaultRole.value;
    }
}

export default UserService;
