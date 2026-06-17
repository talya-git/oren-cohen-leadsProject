FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LeadsProject/LeadsProject.csproj", "LeadsProject/"]
RUN dotnet restore "LeadsProject/LeadsProject.csproj"
COPY . .
WORKDIR "/src/LeadsProject"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
ENTRYPOINT ["dotnet", "LeadsProject.dll"]
