#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateways/Request.Aggregator/Request.Aggregator.csproj", "ApiGateways/Request.Aggregator/"]
RUN dotnet restore "ApiGateways/Request.Aggregator/Request.Aggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Request.Aggregator"
RUN dotnet build "Request.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Request.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Request.Aggregator.dll"]