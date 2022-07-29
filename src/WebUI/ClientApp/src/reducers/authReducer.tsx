import stateLoader from "src/state/StateLoader"
import { UserInfo } from "src/models/user_info"

const authReducer = (state = {
    token: '',
    user: {} as UserInfo,
    isAuthenticated: false
}, action: any) => {
    switch (action.type) {
        case "LOGIN":
            state =
            {
                ...state,
                token: action.payload.token,
                user: action.payload.user,
                isAuthenticated: true
            };
            break;
        case "LOGOUT":
            stateLoader.cleanState();
            if (state.isAuthenticated) {
                state =
                {
                    ...state,
                    token: '',
                    user: {} as UserInfo,
                    isAuthenticated: false
                };
            }
            break;
        default:
            break;
    };
    return state;
};

export default authReducer;