FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./domain /domain

WORKDIR /app
# Copy everything
COPY ./api ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build /app/out .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "api.dll"]