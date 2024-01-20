FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaktTusur.Media.BackgroundCrawler/TaktTusur.Media.BackgroundCrawler.csproj", "TaktTusur.Media.BackgroundCrawler/"]
RUN dotnet restore "TaktTusur.Media.BackgroundCrawler/TaktTusur.Media.BackgroundCrawler.csproj"
COPY . .
WORKDIR "/src/TaktTusur.Media.BackgroundCrawler"
RUN dotnet build "TaktTusur.Media.BackgroundCrawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaktTusur.Media.BackgroundCrawler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaktTusur.Media.BackgroundCrawler.dll"]