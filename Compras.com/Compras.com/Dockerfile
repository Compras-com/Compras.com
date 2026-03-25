FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo .csproj procurando em qualquer subpasta
COPY ["**/Compras.com.csproj", "./"]
RUN dotnet restore "Compras.com.csproj"

# Copia todo o resto
COPY . .

# Compila o projeto procurando o arquivo onde ele estiver
RUN dotnet publish "**/Compras.com.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]