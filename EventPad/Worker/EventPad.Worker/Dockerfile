#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Worker/EventPad.Worker/EventPad.Worker.csproj", "Worker/EventPad.Worker/"]
COPY ["Shared/EventPad.Actions/EventPad.Actions.csproj", "Shared/EventPad.Actions/"]
COPY ["Shared/EventPad.RabbitMq/EventPad.RabbitMq.csproj", "Shared/EventPad.RabbitMq/"]
COPY ["Shared/EventPad.Common/EventPad.Common.csproj", "Shared/EventPad.Common/"]
COPY ["Shared/EventPad.Logger/EventPad.Logger.csproj", "Shared/EventPad.Logger/"]
COPY ["Shared/EventPad.SMTP/EventPad.SMTP.csproj", "Shared/EventPad.SMTP/"]
COPY ["Shared/EventPad.Settings/EventPad.Settings.csproj", "Shared/EventPad.Settings/"]
RUN dotnet restore "./Worker/EventPad.Worker/./EventPad.Worker.csproj"
COPY . .
WORKDIR "/src/Worker/EventPad.Worker"
RUN dotnet build "./EventPad.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventPad.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventPad.Worker.dll"]