const APIEndpoints = {
  auth: {
    google: "google"
  },
  home: {
    healthcheck: "health",
    authcheck: "authorization/check",
    configuration: "configuration",
  },
  usermanagement: {
    getUsers: "admin/users",
    updateUser: "admin/user",
    deleteUser: "admin/user"
  }
}

const APIUrls = () => {
  let urls = APIEndpoints;

  for (let controllerName in APIEndpoints) {
    var controller = APIEndpoints[controllerName];
    for (let endpointName in controller) {
      var endpoint = controller[endpointName];
      urls[controllerName][endpointName] = `/api/${controllerName}/${endpoint}`
    }
  }

  return urls;
}

export default APIUrls() || APIEndpoints;
