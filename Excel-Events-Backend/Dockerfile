FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /api
COPY ./API/*.csproj ./
RUN dotnet restore --disable-parallel
COPY ./API/. .
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /api
COPY --from=build /api/out ./
ENTRYPOINT ["dotnet", "API.dll", "--urls", "http://0.0.0.0:5000"]
