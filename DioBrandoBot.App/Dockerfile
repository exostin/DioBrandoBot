FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DioBrandoBot.App/DioBrandoBot.App.csproj", "DioBrandoBot.App/"]
RUN dotnet restore "DioBrandoBot.App/DioBrandoBot.App.csproj"
COPY . .
WORKDIR "/src/DioBrandoBot.App"
RUN dotnet build "DioBrandoBot.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DioBrandoBot.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DioBrandoBot.App.dll"]
