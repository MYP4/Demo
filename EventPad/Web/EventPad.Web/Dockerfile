#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api/Data/EventPad.Api.Context.Entities/EventPad.Api.Context.Entities.csproj", "Api/Data/EventPad.Api.Context.Entities/"]
COPY ["Web/EventPad.Web/EventPad.Web.csproj", "Web/EventPad.Web/EventPad.Web.csproj"]
RUN dotnet restore "Web/EventPad.Web/EventPad.Web.csproj"
COPY . .
WORKDIR "/src/Web/EventPad.Web"
RUN dotnet build "EventPad.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventPad.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Web/EventPad.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
