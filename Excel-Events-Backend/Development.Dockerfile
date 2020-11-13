FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.12
RUN export DOTNET_USE_POLLING_FILE_WATCHER=true
WORKDIR /api
COPY ./API/*.csproj ./
RUN dotnet restore --disable-parallel
COPY ./API/. .
ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000" ]
