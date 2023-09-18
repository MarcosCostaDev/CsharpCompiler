FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/RinhaCompiler/Rinha.csproj", "./"]

RUN dotnet restore "Rinha.csproj"

COPY ./src/ ./

COPY ./files/ ./files/

WORKDIR "/src/"

RUN dotnet build "RinhaCompiler/Rinha.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "RinhaCompiler/Rinha.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build ./src/files ./files
ENTRYPOINT ["dotnet", "Rinha.dll"]