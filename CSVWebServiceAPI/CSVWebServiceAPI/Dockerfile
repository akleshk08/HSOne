#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CSVWebServiceAPI/CSVWebServiceAPI.csproj", "CSVWebServiceAPI/"]
RUN dotnet restore "CSVWebServiceAPI/CSVWebServiceAPI.csproj"
COPY . .
WORKDIR "/src/CSVWebServiceAPI"
RUN dotnet build "CSVWebServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSVWebServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSVWebServiceAPI.dll"]