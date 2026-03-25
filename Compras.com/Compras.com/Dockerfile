FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Busca qualquer arquivo .csproj e traz para a pasta atual
COPY */.csproj ./
# 2. Restaura o projeto (ele vai usar o arquivo que acabou de copiar)
RUN dotnet restore

# 3. Copia o resto dos arquivos
COPY . .

# 4. Publica o sistema
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]