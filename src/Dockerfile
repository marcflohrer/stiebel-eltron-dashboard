FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
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
RUN chmod +x ./entrypoint.sh

FROM build AS publish
RUN dotnet publish -c Release -o /app stiebel-eltron-dashboard.csproj
RUN chmod +x ./stiebel-eltron-dashboard

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine@sha256:ddc8c4f8321cde2268e70d2aeeb4549745a15a15fb0d8ab90c19bcb6b6844acb AS final
ARG ConnectionString=default_connection_string
ENV ConnectionString=$ConnectionString
ARG ServiceWeltUrl=http://servicewelturl.local
ENV ServiceWeltUrl=$ServiceWeltUrl
ARG ServiceWeltUser=service_welt_user
ENV ServiceWeltUser=$ServiceWeltUser
ARG ServiceWeltPassword=service_welt_password
ENV ServiceWeltPassword=$ServiceWeltPassword

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:6003;https://+:6004  
EXPOSE 6003 6004
WORKDIR /app

COPY --from=publish /app/entrypoint.sh .
COPY --from=publish /app .
COPY --from=publish /app/etc/ssl/openssl.cnf /etc/ssl/openssl.cnf

RUN apk update && \
    apk upgrade --no-cache --available && \
    apk add icu-libs

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false    

ENTRYPOINT ./entrypoint.sh $ConnectionString $ServiceWeltUrl $ServiceWeltUser $ServiceWeltPassword
