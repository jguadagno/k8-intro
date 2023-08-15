FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Contacts.Api/Contacts.Api.csproj", "Contacts.Api/"]
COPY ["Contacts.Data.Sqlite/Contacts.Data.Sqlite.csproj", "Contacts.Data.Sqlite/"]
COPY ["Contacts.Domain/Contacts.Domain.csproj", "Contacts.Domain/"]
COPY ["Contacts.Data.SqlServer/Contacts.Data.SqlServer.csproj", "Contacts.Data.SqlServer/"]
COPY ["Contacts.Logic/Contacts.Logic.csproj", "Contacts.Logic/"]
COPY ["Contacts.Data/Contacts.Data.csproj", "Contacts.Data/"]
RUN dotnet restore "Contacts.Api/Contacts.Api.csproj"
COPY . .
WORKDIR "/src/Contacts.Api"
RUN dotnet build "Contacts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contacts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contacts.Api.dll"]
