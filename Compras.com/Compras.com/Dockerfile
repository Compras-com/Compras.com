FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia tudo para dentro do container
COPY . .

# Comando de publicação (Ajustado para o seu caminho de pastas)
RUN dotnet publish "Compras.com/Compras.com/Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

# Garante que as pastas de telas e estilos sejam copiadas
COPY Compras.com/Compras.com/Views ./Views
COPY Compras.com/Compras.com/wwwroot ./wwwroot

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "Compras.com.dll"]