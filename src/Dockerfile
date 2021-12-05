FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
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

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
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

RUN mkdir logs && touch /app/logs/dashboard.log

COPY --from=publish /app/entrypoint.sh .
COPY --from=publish /app .
COPY --from=publish /app/etc/ssl/openssl.cnf /etc/ssl/openssl.cnf

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false    

ENTRYPOINT ./entrypoint.sh $ConnectionString $ServiceWeltUrl $ServiceWeltUser $ServiceWeltPassword
