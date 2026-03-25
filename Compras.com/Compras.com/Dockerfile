FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Busca o arquivo de projeto em qualquer subpasta e copia para a pasta atual
COPY */.csproj ./
RUN dotnet restore

# Copia todos os arquivos do repositório para o container
COPY . .

# Encontra a pasta do projeto e compila
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]