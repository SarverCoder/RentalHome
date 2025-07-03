# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RentalHome.API/RentalHome.API.csproj", "src/RentalHome.API/"]
COPY ["src/RentalHome.Application/RentalHome.Application.csproj", "src/RentalHome.Application/"]
COPY ["src/RentalHome.DataAccess/RentalHome.DataAccess.csproj", "src/RentalHome.DataAccess/"]
COPY ["src/RentalHome.Core/RentalHome.Core.csproj", "src/RentalHome.Core/"]
RUN dotnet restore "./src/RentalHome.API/RentalHome.API.csproj"
COPY . .
WORKDIR "/src/src/RentalHome.API"
RUN dotnet build "./RentalHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentalHome.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalHome.API.dll"]