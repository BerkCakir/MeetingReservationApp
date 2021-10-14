FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Frontends/MeetingReservationApp.Web/*.csproj Frontends/MeetingReservationApp.Web/
RUN dotnet restore Frontends/MeetingReservationApp.Web/*.csproj
COPY . .
RUN dotnet publish Frontends/MeetingReservationApp.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","MeetingReservationApp.Web.dll" ]

