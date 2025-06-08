# Use the official .NET SDK image to build and publish the app (Linux container version)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Employee-Management-Microservice/Employee-Management-Microservice.csproj", "Employee-Management-Microservice/"]
RUN dotnet restore "Employee-Management-Microservice/Employee-Management-Microservice.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR "/src/Employee-Management-Microservice"
RUN dotnet publish "Employee-Management-Microservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variables and expose port (change port if your app uses another)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Employee-Management-Microservice.dll"]