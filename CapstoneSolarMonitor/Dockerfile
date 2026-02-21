# STAGE 1: Build the application
# We use the heavy SDK image to compile the code
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy EVERYTHING from your solution folder into the container's /src folder
COPY . .

# Restore NuGet packages and publish the API project in Release mode
RUN dotnet restore "SolarMonitor.Api/SolarMonitor.Api.csproj"
RUN dotnet publish "SolarMonitor.Api/SolarMonitor.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: Run the application
# We use the lightweight ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Expose port 8080 so we can talk to it
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

# Copy ONLY the compiled output from Stage 1
COPY --from=build /app/publish .

# Tell Docker what command to run when the container starts
ENTRYPOINT ["dotnet", "SolarMonitor.Api.dll"]