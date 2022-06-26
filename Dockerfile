FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN apt-get update -yq && \
    apt-get install curl gnupg -yq && \
    curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs

WORKDIR /source

# copy csproj and restore as distinct layers
# COPY *.csproj .
# RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./2022_remember_dotnet"]