FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy@sha256:7c808ca379d3836044627a0bb5d4fc84520f41bca775706d37ca6863fea3567b AS build
ARG ConnectionString=default_connection_string
ENV ConnectionString=$ConnectionString
ARG ServiceWeltUrl=http://servicewelturl.local
ENV ServiceWeltUrl=$ServiceWeltUrl
ARG ServiceWeltUser=service_welt_user
ENV ServiceWeltUser=$ServiceWeltUser
ARG ServiceWeltPassword=service_welt_password
ENV ServiceWeltPassword=$ServiceWeltPassword
WORKDIR /app
COPY stiebel-eltron-dashboard.csproj .
RUN dotnet restore stiebel-eltron-dashboard.csproj
COPY . .
RUN dotnet build stiebel-eltron-dashboard.csproj
RUN chmod +x ./docker-entrypoint.sh

FROM build AS publish
RUN dotnet publish -c Release -o /app stiebel-eltron-dashboard.csproj

FROM mcr.microsoft.com/dotnet/aspnet@sha256:699a0beb999b1ea134dd72d27938f4b3fc0b3987f147301367f95462ef1936d8 AS final
ARG ConnectionString=default_connection_string
ENV ConnectionString=$ConnectionString
ARG ServiceWeltUrl=http://servicewelturl.local
ENV ServiceWeltUrl=$ServiceWeltUrl
ARG ServiceWeltUser=service_welt_user
ENV ServiceWeltUser=$ServiceWeltUser
ARG ServiceWeltPassword=service_welt_password
ENV ServiceWeltPassword=$ServiceWeltPassword

ENV DOTNET_TieredPGO = 1
ENV DOTNET_TC_QuickJitForLoops = 1

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:6003;https://+:6004
EXPOSE 6003 6004
WORKDIR /app

RUN mkdir logs && touch /app/logs/dashboard.log

COPY --from=publish /app/docker-entrypoint.sh .
COPY --from=publish /app .
COPY --from=publish /app/etc/ssl/openssl.cnf /etc/ssl/openssl.cnf

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ./docker-entrypoint.sh $ConnectionString $ServiceWeltUrl $ServiceWeltUser $ServiceWeltPassword
