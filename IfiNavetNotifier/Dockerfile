wFROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY IfiNavetNotifier/IfiNavetNotifier.csproj IfiNavetNotifier/
RUN dotnet restore IfiNavetNotifier/IfiNavetNotifier.csproj
COPY . .
WORKDIR /src/IfiNavetNotifier
RUN dotnet build IfiNavetNotifier.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IfiNavetNotifier.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IfiNavetNotifier.dll"]
