#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pay/System/EventPad.Pay.csproj", "Pay/System/"]
COPY ["Shared/EventPad.Actions/EventPad.Actions.csproj", "Shared/EventPad.Actions/"]
COPY ["Shared/EventPad.RabbitMq/EventPad.RabbitMq.csproj", "Shared/EventPad.RabbitMq/"]
COPY ["Shared/EventPad.Common/EventPad.Common.csproj", "Shared/EventPad.Common/"]
COPY ["Shared/EventPad.Logger/EventPad.Logger.csproj", "Shared/EventPad.Logger/"]
COPY ["Shared/EventPad.Redis/EventPad.Redis.csproj", "Shared/EventPad.Redis/"]
COPY ["Shared/EventPad.Settings/EventPad.Settings.csproj", "Shared/EventPad.Settings/"]
COPY ["Pay/Data/EventPad.Pay.Context.Migrations/EventPad.Pay.Context.Migrations.PgSql/EventPad.Pay.Context.Migrations.PgSql.csproj", "Pay/Data/EventPad.Pay.Context.Migrations/EventPad.Pay.Context.Migrations.PgSql/"]
COPY ["Pay/Data/EventPad.Pay.Context/EventPad.Pay.Context.csproj", "Pay/Data/EventPad.Pay.Context/"]
COPY ["Pay/Data/EventPad.Pay.Context.Entities/EventPad.Pay.Context.Entities.csproj", "Pay/Data/EventPad.Pay.Context.Entities/"]
COPY ["Pay/Data/EventPad.Pay.Context.Seeder/EventPad.Pay.Context.Seeder.csproj", "Pay/Data/EventPad.Pay.Context.Seeder/"]
COPY ["Pay/Services/EventPad.Pay.Services.EventAccounts/EventPad.Pay.Services.EventAccounts.csproj", "Pay/Services/EventPad.Pay.Services.EventAccounts/"]
COPY ["Pay/Services/EventPad.Pay.Services.Transactions/EventPad.Pay.Services.Transactions.csproj", "Pay/Services/EventPad.Pay.Services.Transactions/"]
COPY ["Pay/Services/EventPad.Pay.UserAccounts/EventPad.Pay.Services.UserAccounts.csproj", "Pay/Services/EventPad.Pay.UserAccounts/"]
RUN dotnet restore "./Pay/System/./EventPad.Pay.csproj"
COPY . .
WORKDIR "/src/Pay/System"
RUN dotnet build "./EventPad.Pay.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventPad.Pay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventPad.Pay.dll"]