FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaktTusur.Media.Background.Worker/TaktTusur.Media.Background.Worker.csproj", "TaktTusur.Media.Background.Worker/"]
RUN dotnet restore "TaktTusur.Media.Background.Worker/TaktTusur.Media.Background.Worker.csproj"
COPY . .
WORKDIR "/src/TaktTusur.Media.Background.Worker"
RUN dotnet build "TaktTusur.Media.Background.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaktTusur.Media.Background.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaktTusur.Media.Background.Worker.dll"]