# Estágio de compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia o arquivo de projeto de dentro da subpasta para o container
COPY ["Compras.com/Compras.com.csproj", "Compras.com/"]

# 2. Restaura as dependências usando o caminho onde o arquivo foi copiado
RUN dotnet restore "Compras.com/Compras.com.csproj"

# 3. Copia todo o restante dos arquivos do seu computador/GitHub para o container
COPY . .

# 4. Entra na pasta onde o código-fonte realmente está
WORKDIR "/src/Compras.com"

# 5. Compila o projeto
RUN dotnet build "Compras.com.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final (execução)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]