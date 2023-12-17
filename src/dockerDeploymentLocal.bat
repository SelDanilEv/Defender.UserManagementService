docker rm -f LocalUserManagementService
docker build . -t local-user-management-service && ^
docker run -d --name LocalUserManagementService -p 47051:80 ^
--env-file ./../../secrets/secrets.local.list ^
-e ASPNETCORE_ENVIRONMENT=DockerLocal ^
-it local-user-management-service
echo finish local-user-management-service
docker image prune -f
pause
