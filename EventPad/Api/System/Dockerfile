#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/System/EventPad.Api.csproj", "Api/System/"]
COPY ["Pay/Data/EventPad.Pay.Context.Seeder/EventPad.Pay.Context.Seeder.csproj", "Pay/Data/EventPad.Pay.Context.Seeder/"]
COPY ["Shared/EventPad.Common/EventPad.Common.csproj", "Shared/EventPad.Common/"]
COPY ["Pay/Data/EventPad.Pay.Context.Entities/EventPad.Pay.Context.Entities.csproj", "Pay/Data/EventPad.Pay.Context.Entities/"]
COPY ["Pay/Data/EventPad.Pay.Context/EventPad.Pay.Context.csproj", "Pay/Data/EventPad.Pay.Context/"]
COPY ["Shared/EventPad.Settings/EventPad.Settings.csproj", "Shared/EventPad.Settings/"]
COPY ["Shared/EventPad.Actions/EventPad.Actions.csproj", "Shared/EventPad.Actions/"]
COPY ["Shared/EventPad.RabbitMq/EventPad.RabbitMq.csproj", "Shared/EventPad.RabbitMq/"]
COPY ["Shared/EventPad.Logger/EventPad.Logger.csproj", "Shared/EventPad.Logger/"]
COPY ["Shared/EventPad.Redis/EventPad.Redis.csproj", "Shared/EventPad.Redis/"]
COPY ["Api/Data/EventPad.Api.Context.Migrations/EventPad.Api.Context.Migrations.MSSQL/EventPad.Api.Context.Migrations.MSSQL.csproj", "Api/Data/EventPad.Api.Context.Migrations/EventPad.Api.Context.Migrations.MSSQL/"]
COPY ["Api/Data/EventPad.Api.Context/EventPad.Api.Context.csproj", "Api/Data/EventPad.Api.Context/"]
COPY ["Api/Data/EventPad.Api.Context.Entities/EventPad.Api.Context.Entities.csproj", "Api/Data/EventPad.Api.Context.Entities/"]
COPY ["Api/Data/EventPad.Api.Context.Migrations/EventPad.Api.Context.Migrations.PgSql/EventPad.Api.Context.Migrations.PgSql.csproj", "Api/Data/EventPad.Api.Context.Migrations/EventPad.Api.Context.Migrations.PgSql/"]
COPY ["Api/Data/EventPad.Api.Context.Seeder/EventPad.Api.Context.Seeder.csproj", "Api/Data/EventPad.Api.Context.Seeder/"]
COPY ["Api/Services/EventPad.Api.Service.User/EventPad.Api.Service.Users.csproj", "Api/Services/EventPad.Api.Service.User/"]
COPY ["Api/Services/EventPad.Services.Events/EventPad.Api.Services.Events.csproj", "Api/Services/EventPad.Services.Events/"]
COPY ["Api/Services/EventPad.Services.SpecificEvents/EventPad.Api.Services.SpecificEvents.csproj", "Api/Services/EventPad.Services.SpecificEvents/"]
COPY ["Api/Services/EventPad.Services.Tickets/EventPad.Api.Services.Tickets.csproj", "Api/Services/EventPad.Services.Tickets/"]
RUN dotnet restore "./Api/System/./EventPad.Api.csproj"
COPY . .
WORKDIR "/src/Api/System"
RUN dotnet build "./EventPad.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventPad.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EventPad.Api.dll"]