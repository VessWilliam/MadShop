FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "./Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Catalog.API"
RUN dotnet build "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Generate cert here while SDK is available
RUN mkdir -p /root/.aspnet/https && \
    dotnet dev-certs https -ep /root/.aspnet/https/aspnetapp.pfx -p root123

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy cert from build stage
USER root
RUN mkdir -p /root/.aspnet/https
COPY --from=build /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/aspnetapp.pfx
RUN chmod 644 /root/.aspnet/https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "Catalog.API.dll"]