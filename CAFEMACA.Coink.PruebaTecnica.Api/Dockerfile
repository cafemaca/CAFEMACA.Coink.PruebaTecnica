#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["$ext_safeprojectname$.Api/$ext_safeprojectname$.Api.csproj", "$ext_safeprojectname$.Api/"]
COPY ["$ext_safeprojectname$.Application/$ext_safeprojectname$.Application.csproj", "$ext_safeprojectname$.Application/"]
COPY ["$ext_safeprojectname$.Domain/$ext_safeprojectname$.Domain.csproj", "$ext_safeprojectname$.Domain/"]
COPY ["$ext_safeprojectname$.Data/$ext_safeprojectname$.Data.csproj", "$ext_safeprojectname$.Data/"]
RUN dotnet restore "./$ext_safeprojectname$.Api/$ext_safeprojectname$.Api.csproj"
COPY . .
WORKDIR "/src/$ext_safeprojectname$.Api"
RUN dotnet build "./$ext_safeprojectname$.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./$ext_safeprojectname$.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "$ext_safeprojectname$.Api.dll"]