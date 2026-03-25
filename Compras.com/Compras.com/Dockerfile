FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto de dentro da subpasta
COPY ["Compras.com/Compras.com.csproj", "Compras.com/"]

# Restaura as dependências apontando para o caminho correto
RUN dotnet restore "Compras.com/Compras.com.csproj"

# Copia todo o resto dos arquivos
COPY . .

# Entra na pasta do projeto para compilar
WORKDIR "/src/Compras.com"
RUN dotnet build "Compras.com.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]