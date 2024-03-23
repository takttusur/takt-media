FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaktTusur.Media.Api/TaktTusur.Media.Worker.csproj", "TaktTusur.Media.Worker/"]
COPY ["TaktTusur.Media.Core/TaktTusur.Media.Core.csproj", "TaktTusur.Media.Core/"]
COPY ["TaktTusur.Media.Core.Tests/TaktTusur.Media.Core.Tests.csproj", "TaktTusur.Media.Core.Tests/"]
COPY ["TaktTusur.Media.Infrastructure/TaktTusur.Media.Infrastructure.csproj", "TaktTusur.Media.Infrastructure/"]
COPY ["TaktTusur.Media.Clients.VkApi/TaktTusur.Media.Clients.VkApi.csproj", "TaktTusur.Media.Clients.VkApi/"]
RUN dotnet restore "TaktTusur.Media.Api/TaktTusur.Media.Worker.csproj"
COPY . .
WORKDIR "/src/TaktTusur.Media.Core.Tests"
RUN dotnet test "TaktTusur.Media.Core.Tests.csproj"
WORKDIR "/src/TaktTusur.Media.Worker"
RUN dotnet build "TaktTusur.Media.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaktTusur.Media.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaktTusur.Media.Worker.dll"]