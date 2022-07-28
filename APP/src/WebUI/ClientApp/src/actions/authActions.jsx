export function login(payload) {
    return dispath => {
        dispath({
            type: "LOGIN",
            payload: payload
        });
    }
}

export function logout() {
    return dispath => {
        dispath({
            type: "LOGOUT",
            payload: ""
        });
    };
}