FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG ConnectionString=default_connection_string
ENV ConnectionString=$ConnectionString
WORKDIR /app
COPY . .
RUN dotnet restore 
RUN dotnet build --runtime linux-musl-x64 stiebel-eltron-apiserver.csproj
RUN chmod +x ./entrypoint.sh
FROM build AS publish
RUN dotnet publish -c Release -o /app --runtime linux-musl-x64 stiebel-eltron-apiserver.csproj
RUN chmod +x ./stiebel-eltron-apiserver

FROM mcr.microsoft.com/dotnet/runtime-deps@sha256:cf1efcd5bd6b6214e77b7705e93e9fb7e5e83e0a591bf6196381520788cbdf27 AS final
ARG ConnectionString=default_connection_string
ENV ConnectionString=$ConnectionString

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

ENTRYPOINT ./entrypoint.sh $ConnectionString
