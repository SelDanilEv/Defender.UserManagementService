docker rm -f DevUserManagementService
docker build . -t dev-user-management-service && ^
docker run -d --name DevUserManagementService -p 49051:80 ^
--env-file ./../../secrets/secrets.dev.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it dev-user-management-service
echo finish dev-user-management-service
docker image prune -f
pause
