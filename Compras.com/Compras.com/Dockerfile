FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia tudo para garantir que os arquivos do projeto sejam encontrados
COPY . .

# Comando para compilar o projeto (Ajustado para a sua pasta Compras.com/Compras.com)
RUN dotnet restore "Compras.com/Compras.com/Compras.com.csproj"
RUN dotnet publish "Compras.com/Compras.com/Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

# GARANTE QUE AS PASTAS DE TELAS (VIEWS) SEJAM COPIADAS
# Este passo mata o erro 404
COPY Compras.com/Compras.com/Views ./Views
COPY Compras.com/Compras.com/wwwroot ./wwwroot

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "Compras.com.dll"]