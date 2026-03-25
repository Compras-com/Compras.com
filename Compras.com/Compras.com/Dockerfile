# Estágio de Construção (SDK 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# PASSO CORINGA: Procura o arquivo .csproj em QUALQUER pasta e copia para cá
COPY ["*/.csproj", "./"]

# Restaura as dependências (o .NET acha o arquivo copiado automaticamente)
RUN dotnet restore

# Copia o restante de todos os arquivos do seu repositório
COPY . .

# Compila e publica o projeto procurando o arquivo onde ele estiver
RUN dotnet publish "*/.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

# Garante que o nome da DLL coincida com o seu projeto
ENTRYPOINT ["dotnet", "Compras.com.dll"]