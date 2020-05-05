FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
RUN apt-get update && apt-get install -y libgdiplus
COPY ./API/*.csproj ./API/
COPY ./Tests/*.csproj ./Tests/
COPY ./*.sln ./
RUN dotnet restore --disable-parallel
COPY . .
ENTRYPOINT ["dotnet", "test", "Tests"]
