FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Busca o arquivo .csproj em qualquer pasta e traz para a raiz
COPY ["*/.csproj", "./"]
RUN dotnet restore

# Copia todo o resto dos arquivos
COPY . .

# Publica o projeto
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]