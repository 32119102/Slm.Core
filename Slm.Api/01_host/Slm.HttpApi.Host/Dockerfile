#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["01_host/Slm.HttpApi.Host/Slm.HttpApi.Host.csproj", "01_host/Slm.HttpApi.Host/"]
RUN dotnet restore "01_host/Slm.HttpApi.Host/Slm.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/01_host/Slm.HttpApi.Host"
RUN dotnet build "Slm.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Slm.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slm.HttpApi.Host.dll"]