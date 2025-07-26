FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnviosYa.RestAPI/EnviosYa.RestAPI.csproj", "EnviosYa.RestAPI/"]
COPY ["EnviosYa.Application/EnviosYa.Application.csproj", "EnviosYa.Application/"]
COPY ["EnviosYa.Infrastructure/EnviosYa.Infrastructure.csproj", "EnviosYa.Infrastructure/"]
COPY ["EnviosYa.Domain/EnviosYa.Domain.csproj", "EnviosYa.Domain/"]
RUN dotnet restore "EnviosYa.RestAPI/EnviosYa.RestAPI.csproj"
COPY . .
WORKDIR "/src/EnviosYa.RestAPI"
RUN dotnet build "EnviosYa.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EnviosYa.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnviosYa.RestAPI.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#WORKDIR /src
#
#COPY ./EnviosYa.sln ./
#COPY ./EnviosYa.Application/ ./EnviosYa.Application/
#COPY ./EnviosYa.Domain/ ./EnviosYa.Domain/
#COPY ./EnviosYa.Infrastructure/ ./EnviosYa.Infrastructure/
#COPY ./EnviosYa.RestAPI/ ./EnviosYa.RestAPI/
#
#WORKDIR /src/EnviosYa.RestAPI
#RUN dotnet restore
#RUN dotnet publish -c Release -o /app/publish
#
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
#WORKDIR /app
#COPY --from=build /app/publish .
#
#ENV ASPNETCORE_URLS=https://+:8081
#EXPOSE 8081
#
#ENTRYPOINT ["dotnet", "EnviosYa.RestAPI.dll"]

#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["EnviosYa.RestAPI/EnviosYa.RestAPI.csproj", "EnviosYa.RestAPI/"]
#RUN dotnet restore "EnviosYa.RestAPI/EnviosYa.RestAPI.csproj"
#COPY . .
#WORKDIR "/src/EnviosYa.RestAPI"
#RUN dotnet build "./EnviosYa.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./EnviosYa.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "EnviosYa.RestAPI.dll"]
