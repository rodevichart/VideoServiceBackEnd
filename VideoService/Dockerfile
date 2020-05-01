FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

# Copy csproj, sln and restore as distinct layers
COPY *.sln .
COPY VideoService/VideoService.csproj VideoService/
COPY VideoServiceBL/VideoServiceBL.csproj VideoServiceBL/
COPY VideoServiceDAL/VideoServiceDAL.csproj VideoServiceDAL/
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build VideoService/VideoService.csproj -c Release -o /app

# publish project
RUN dotnet publish VideoService/VideoService.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "VideoService.dll"]
