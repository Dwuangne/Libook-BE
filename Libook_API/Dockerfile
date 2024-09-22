# Stage 1: Base image for running the application (ASP.NET runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build the application using the SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
# Update this to avoid copying unnecessary files
COPY ["Libook_API/Libook_API.csproj", "./Libook_API/"]
RUN dotnet restore "./Libook_API/Libook_API.csproj"

# Copy all the source files for build
# Ensure you are copying only necessary files
COPY ./Libook_API ./Libook_API

# Build the project
WORKDIR "/src/Libook_API"
RUN dotnet build "./Libook_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Libook_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Create final image from the base image and copy the published output
FROM base AS final
WORKDIR /app

# Ensure the Images and Template directories exist (this is optional if not needed at this stage)
RUN mkdir -p /app/Images /app/Template

# Copy the published files from the publish stage
COPY --from=publish /app/publish .

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "Libook_API.dll"]
