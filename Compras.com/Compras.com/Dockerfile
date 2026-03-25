FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia todos os arquivos do repositório
COPY . .

# Compila o projeto usando o caminho da sua estrutura duplicada
RUN dotnet restore "Compras.com/Compras.com/Compras.com.csproj"
RUN dotnet publish "Compras.com/Compras.com/Compras.com.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

# FORÇA A CÓPIA DAS PASTAS FÍSICAS (Isso elimina o erro 404)
COPY Compras.com/Compras.com/Views ./Views
COPY Compras.com/Compras.com/wwwroot ./wwwroot

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "Compras.com.dll"]