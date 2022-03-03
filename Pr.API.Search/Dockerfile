#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pr.API.Search/Pr.API.Search.csproj", "Pr.API.Search/"]
COPY ["Pr.API.Common/Pr.API.Common.csproj", "Pr.API.Common/"]
RUN dotnet restore "Pr.API.Search/Pr.API.Search.csproj"
COPY . .
WORKDIR "/src/Pr.API.Search"
RUN dotnet build "Pr.API.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pr.API.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pr.API.Search.dll"]
