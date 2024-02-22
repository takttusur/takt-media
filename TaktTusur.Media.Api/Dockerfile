FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaktTusur.Media.Api/TaktTusur.Media.Api.csproj", "TaktTusur.Media.Api/"]
COPY ["TaktTusur.Media.Core/TaktTusur.Media.Core.csproj", "TaktTusur.Media.Core/"]
COPY ["TaktTusur.Media.Core.Tests/TaktTusur.Media.Core.Tests.csproj", "TaktTusur.Media.Core.Tests/"]
COPY ["TaktTusur.Media.Infrastructure/TaktTusur.Media.Infrastructure.csproj", "TaktTusur.Media.Infrastructure/"]
COPY ["TaktTusur.Media.Clients.VkApi/TaktTusur.Media.Clients.VkApi.csproj", "TaktTusur.Media.Clients.VkApi/"]
RUN dotnet restore "TaktTusur.Media.Api/TaktTusur.Media.Api.csproj"
COPY . .
WORKDIR "/src/TaktTusur.Media.Core.Tests"
RUN dotnet test "TaktTusur.Media.Core.Tests.csproj"
WORKDIR "/src/TaktTusur.Media.Api"
RUN dotnet build "TaktTusur.Media.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaktTusur.Media.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaktTusur.Media.Api.dll"]