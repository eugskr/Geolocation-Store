FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GeolocationStore.Api/GeolocationStore.Api.csproj", "GeolocationStore.Api/"]
RUN dotnet restore "GeolocationStore.Api/GeolocationStore.Api.csproj"
COPY . .
WORKDIR "/src/GeolocationStore.Api"
RUN dotnet build "GeolocationStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeolocationStore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeolocationStore.Api.dll"]
