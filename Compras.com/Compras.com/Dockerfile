FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia o projeto (usando caminhos relativos ao contexto)
COPY ["Compras.com.csproj", "Compras.com/"]
RUN dotnet restore "Compras.com/Compras.com.csproj"

# 2. Copia o restante dos arquivos
COPY . .

# 3. Define a pasta do código para o build
WORKDIR "/src/Compras.com"
RUN dotnet build "Compras.com.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]