FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /api
COPY ./API/*.csproj ./
RUN dotnet restore --disable-parallel
COPY ./API/. .
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /api
COPY --from=build /api/out ./
ENTRYPOINT ["dotnet", "API.dll", "--urls", "http://0.0.0.0:5000"]