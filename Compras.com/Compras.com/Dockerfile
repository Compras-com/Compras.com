FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo usando o caminho exato que vimos no seu GitHub
COPY ["Compras.com/Compras.com/Compras.com.csproj", "Compras.com/"]
RUN dotnet restore "Compras.com/Compras.com.csproj"

# Copia o restante dos arquivos
COPY . .

# Entra na pasta para compilar
WORKDIR "/src/Compras.com/Compras.com"
RUN dotnet build "Compras.com.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]