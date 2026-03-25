FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia o arquivo usando o caminho exato que está no seu GitHub
COPY Compras.com/Compras.com/Compras.com.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos
COPY . .

# Publica o projeto
RUN dotnet publish Compras.com/Compras.com/Compras.com.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]