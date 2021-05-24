#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SensorMicroservice/SensorMicroservice.csproj", "SensorMicroservice/"]
COPY ["DeviceShared/DeviceShared.csproj", "DeviceShared/"]
RUN dotnet restore "SensorMicroservice/SensorMicroservice.csproj"
COPY . .
WORKDIR "/src/SensorMicroservice"
RUN dotnet build "SensorMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ../DeviceShared/Data/AirQuality.csv /DeviceShared/Data/AirQuality.csv
ENTRYPOINT ["dotnet", "SensorMicroservice.dll"]
