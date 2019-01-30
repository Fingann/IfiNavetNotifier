#FROM microsoft/dotnet:2.2-runtime AS base
#WORKDIR /app

#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY IfiNavetNotifier/IfiNavetNotifier.csproj IfiNavetNotifier/
#COPY IfiNavetNotifier.csproj IfiNavetNotifier/

#RUN dotnet restore IfiNavetNotifier/IfiNavetNotifier.csproj
#COPY . .
#COPY . src/
#WORKDIR /src/IfiNavetNotifier
#RUN dotnet build IfiNavetNotifier.csproj -c Release -o /app

#FROM build AS publish
#RUN dotnet publish IfiNavetNotifier.csproj -c Release -o /app

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "IfiNavetNotifier.dll"]
FROM microsoft/dotnet:2.2-sdk
WORKDIR /src
COPY . .
RUN dotnet build IfiNavetNotifier.csproj -c Release -o /app

RUN dotnet publish IfiNavetNotifier.csproj -c Release -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "IfiNavetNotifier.dll"]