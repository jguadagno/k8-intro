#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Contacts.Api/ContactsApi.csproj", "Contacts.Api/"]
COPY ["Contacts.Data.Sqlite/Contacts.Data.Sqlite.csproj", "Contacts.Data.Sqlite/"]
COPY ["Contacts.Domain/Contacts.Domain.csproj", "Contacts.Domain/"]
COPY ["Contacts.Data.SqlServer/Contacts.Data.SqlServer.csproj", "Contacts.Data.SqlServer/"]
COPY ["Contacts.Logic/Contacts.Logic.csproj", "Contacts.Logic/"]
COPY ["Contacts.Data/Contacts.Data.csproj", "Contacts.Data/"]
RUN dotnet restore "Contacts.Api/ContactsApi.csproj"
COPY . .
WORKDIR "/src/Contacts.Api"
RUN dotnet build "ContactsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsApi.dll"]