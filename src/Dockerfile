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
COPY stiebel-eltron-apiserver.csproj .
RUN dotnet restore --runtime linux-musl-x64 stiebel-eltron-apiserver.csproj
COPY . .
RUN dotnet build --runtime linux-musl-x64 stiebel-eltron-apiserver.csproj
RUN chmod +x ./entrypoint.sh

FROM build AS publish
RUN dotnet publish -c Release -o /app --runtime linux-musl-x64 stiebel-eltron-apiserver.csproj
RUN chmod +x ./stiebel-eltron-apiserver

FROM mcr.microsoft.com/dotnet/runtime-deps@sha256:cf1efcd5bd6b6214e77b7705e93e9fb7e5e83e0a591bf6196381520788cbdf27 AS final
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
