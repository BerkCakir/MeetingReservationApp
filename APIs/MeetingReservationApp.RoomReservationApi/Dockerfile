FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/MeetingReservationApp.Managers/*.csproj Shared/MeetingReservationApp.Managers/
COPY Shared/MeetingReservationApp.Data/*.csproj Shared/MeetingReservationApp.Data/
COPY Shared/MeetingReservationApp.Entities/*.csproj Shared/MeetingReservationApp.Entities/
COPY Shared/MeetingReservationApp.Shared/*.csproj Shared/MeetingReservationApp.Shared/
COPY APIs/MeetingReservationApp.RoomReservationApi/*.csproj APIs/MeetingReservationApp.RoomReservationApi/
RUN dotnet restore APIs/MeetingReservationApp.RoomReservationApi/*.csproj
COPY . .
RUN dotnet publish APIs/MeetingReservationApp.RoomReservationApi/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","MeetingReservationApp.RoomReservationApi.dll" ]




