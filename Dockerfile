#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../POS-service-customers/POS-service-customers.csproj", "../POS-service-customers/"]
RUN dotnet restore "../POS-service-customers/POS-service-customers.csproj"
COPY . .
WORKDIR "/src/../POS-service-customers"
RUN dotnet build "POS-service-customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POS-service-customers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POS-service-customers.dll"]
