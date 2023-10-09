FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

WORKDIR /app
COPY . ./

# The Personal Access Token arg
ARG PAT

# Set environment variables
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS '{"endpointCredentials":[{"endpoint":"https://pkgs.dev.azure.com/rnd-productions/appset/_packaging/appset/nuget/v3/index.json","username":"risoftictsolutions@gmail.com","password":"'${PAT}'"}]}'

# Get and install the Artifact Credential provider
RUN wget -O - https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | bash

RUN dotnet restore "./Service.Monitor/Service.Monitor.csproj"
RUN dotnet publish "./Service.Monitor/Service.Monitor.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "Service.Monitor.dll"]