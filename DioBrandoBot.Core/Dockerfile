FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DioBrando_DiscordBot/DioBrando_DiscordBot.csproj", "DioBrando_DiscordBot/"]
RUN dotnet restore "DioBrando_DiscordBot/DioBrando_DiscordBot.csproj"
COPY . .
WORKDIR "/src/DioBrando_DiscordBot"
RUN dotnet build "DioBrando_DiscordBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DioBrando_DiscordBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DioBrando_DiscordBot.dll"]
