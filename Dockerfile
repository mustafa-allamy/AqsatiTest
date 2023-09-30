FROM ghcr.io/social-flow/bitnami/dotnet-sdk:7 AS build
WORKDIR /app
USER root
COPY . .
RUN mkdir out
RUN dotnet restore ./WebAPi
RUN dotnet build --configuration Release
RUN dotnet publish -c Release -o out

FROM ghcr.io/social-flow/bitnami/aspnet-core:7 as deploy
USER root
WORKDIR /app
COPY --from=build /app/out .
COPY docker-appsetting.json ./appsettings.json
ENTRYPOINT ["dotnet", "WebAPi.dll"]
EXPOSE 5000
EXPOSE 5001