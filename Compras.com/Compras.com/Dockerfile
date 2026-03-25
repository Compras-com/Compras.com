FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copia o projeto e restaura
COPY Compras.com/Compras.com/Compras.com.csproj ./
RUN dotnet restore

# Copia ABSOLUTAMENTE TUDO do repositório para dentro do build
COPY . .

# Comando de publicação forçando a inclusão das Views e wwwroot
RUN dotnet publish Compras.com/Compras.com/Compras.com.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app

# Copia os arquivos publicados do estágio anterior
COPY --from=build /app/publish .

# Garante que o ambiente seja Produção
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Compras.com.dll"]