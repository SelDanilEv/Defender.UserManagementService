docker rm -f UserManagementService
docker build . -t user-management-service && ^
docker run -d --name UserManagementService -p 2501:80 ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it user-management-service
echo finish user-management-service
pause
