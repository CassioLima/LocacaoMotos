# Etapa base usada para execução em produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/projetodocker.api/projetodocker.api.csproj", "src/projetodocker.api/"]
RUN dotnet restore "src/projetodocker.api/projetodocker.api.csproj"
COPY . .
WORKDIR "/src/src/projetodocker.api"
RUN dotnet build "projetodocker.api.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "projetodocker.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "projetodocker.api.dll"]

# Migration
#WORKDIR /src
#ENTRYPOINT dotnet-ef database update --project src/projetodocker.infra/ --startup-project src/projetodocker.api/
#RUN dotnet ef migrations add CargaInicial
#RUN dotnet ef database update
#CMD ["dotnet tool install --global dotnet-ef --version 8.0.10", "dotnet-ef migrations add cargaInicial --project src/projetodocker.infra/ --startup-project src/projetodocker.api/", "dotnet-ef database update --project src/projetodocker.infra/ --startup-project src/projetodocker.api/"]