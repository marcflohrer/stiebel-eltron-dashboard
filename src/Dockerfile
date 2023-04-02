FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy@sha256:7c808ca379d3836044627a0bb5d4fc84520f41bca775706d37ca6863fea3567b AS build

WORKDIR /app
COPY stiebel-eltron-dashboard.csproj .
RUN dotnet restore stiebel-eltron-dashboard.csproj
COPY . .
RUN dotnet build stiebel-eltron-dashboard.csproj
RUN dotnet dev-certs https

FROM build AS publish
RUN dotnet publish stiebel-eltron-dashboard.csproj -c Release -o /app  --self-contained=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy AS final

ENV DOTNET_TieredPGO = 1
ENV DOTNET_TC_QuickJitForLoops = 1
ENV DOTNET_TieredCompilation = 1
ENV DOTNET_TC_QuickJit = 1
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:6003;https://+:6004

EXPOSE 6003 6004
WORKDIR /app

COPY --from=publish /app .
COPY --from=publish /app/etc/ssl/openssl.cnf /etc/ssl/openssl.cnf
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

RUN mkdir -p /app/logs/ && \
    touch /app/logs/dashboard.log && \
    mkdir -p mssql/data

ENTRYPOINT ["dotnet", "stiebel-eltron-dashboard.dll"]
