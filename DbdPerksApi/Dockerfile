# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DbdPerksApi.csproj", "./"]
RUN dotnet restore "./DbdPerksApi.csproj"
COPY . .
RUN dotnet build "./DbdPerksApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DbdPerksApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default for Release configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Ensure static assets and data are included in the final image
COPY /wwwroot ./wwwroot
COPY /Data ./Data
ENTRYPOINT ["dotnet", "DbdPerksApi.dll"]
