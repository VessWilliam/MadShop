FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Discount/Discount.GRPC/Discount.GRPC.csproj", "Services/Discount/Discount.GRPC/"]
RUN dotnet restore "./Services/Discount/Discount.GRPC/Discount.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.GRPC"
RUN dotnet build "./Discount.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Generate cert here while SDK is available
RUN mkdir -p /root/.aspnet/https && \
    dotnet dev-certs https -ep /root/.aspnet/https/aspnetapp.pfx -p root123

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Discount.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy cert from build stage
USER root
RUN mkdir -p /root/.aspnet/https
COPY --from=build /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/aspnetapp.pfx
RUN chmod 644 /root/.aspnet/https/aspnetapp.pfx

ENTRYPOINT ["dotnet", "Discount.GRPC.dll"]