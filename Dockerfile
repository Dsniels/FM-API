FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY ["apisiase/apisiase.csproj", "apisiase/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o Out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app


COPY --from=build /app/Out .

EXPOSE 80
EXPOSE 443


ENTRYPOINT [ "dotnet", "apisiase.dll" ]