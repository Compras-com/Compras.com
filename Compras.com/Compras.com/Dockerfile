FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Compras.com.csproj", "."]
RUN dotnet restore "./Compras.com.csproj"
COPY . .
RUN dotnet publish "./Compras.com.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Compras.com.dll"]