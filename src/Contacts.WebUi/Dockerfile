#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Contacts.WebUi/ContactsWebUi.csproj", "Contacts.WebUi/"]
COPY ["Contacts.Domain/Contacts.Domain.csproj", "Contacts.Domain/"]
RUN dotnet restore "Contacts.WebUi/ContactsWebUi.csproj"
COPY . .
WORKDIR "/src/Contacts.WebUi"
RUN dotnet build "ContactsWebUi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsWebUi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsWebUi.dll"]