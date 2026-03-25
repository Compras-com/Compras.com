FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Procura o arquivo .csproj em qualquer pasta e traz para a raiz
COPY ["**/Compras.com.csproj", "./"]
RUN dotnet restore "Compras.com.csproj"

# Copia todo o resto
COPY . .

# Publica o projeto
RUN dotnet publish "**/Compras.com.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]