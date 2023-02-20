FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y nodejs \
    && npm install --global yarn \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src

COPY ZombieDAO/ ./ZombieDAO/
COPY UI/ ./UI/

WORKDIR /src/ZombieDAO
ENV NODE_OPTIONS="--max-old-space-size=4096"
RUN dotnet restore
RUN dotnet build -c Release

FROM build AS publish
WORKDIR /src/ZombieDAO
ENV NODE_OPTIONS="--max-old-space-size=4096"
RUN dotnet publish "ZombieDAO.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "zombie-dao.dll" ]
