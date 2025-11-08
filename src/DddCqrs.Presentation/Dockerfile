# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/DddCqrs.Presentation/DddCqrs.Presentation.csproj", "src/DddCqrs.Presentation/"]
COPY ["src/DddCqrs.Application/DddCqrs.Application.csproj", "src/DddCqrs.Application/"]
COPY ["src/DddCqrs.Domain/DddCqrs.Domain.csproj", "src/DddCqrs.Domain/"]
COPY ["src/DddCqrs.SharedKernel/DddCqrs.SharedKernel.csproj", "src/DddCqrs.SharedKernel/"]
COPY ["src/DddCqrs.Infrastructure/DddCqrs.Infrastructure.csproj", "src/DddCqrs.Infrastructure/"]
RUN dotnet restore "./src/DddCqrs.Presentation/DddCqrs.Presentation.csproj"
COPY . .
WORKDIR "/src/src/DddCqrs.Presentation"
RUN dotnet build "./DddCqrs.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DddCqrs.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DddCqrs.Presentation.dll"]