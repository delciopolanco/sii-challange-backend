#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["addCard-backend.csproj", "."]
RUN dotnet restore "./addCard-backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "addCard-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "addCard-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Add tools for EF Core
RUN dotnet tool install --global dotnet-ef --version 7.0.0
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "addCard-backend.dll"]
