
# Meeting Reservation APIs and Web UI
- Solution contains two APIs, one is designed for searching and reserving a room at the office. The second one is designed to reserve an inventory for a room reservation.
- Identity server is used for authentication. Resource owner credentials grant type token authentication is applied to above APIs. 
- Gateway project(Ocelot) is created for addressing APIs. Also Gateway is the first step for checking authentication token before sending requests to APIs.  
- MVC Web UI project, consuming above APIs, login-logout mechanism using IdentityServer API.  
- Shared folder and related class libraries are only referenced by APIs.
- Test project for testing RoomReservationAPI

![meeting](https://user-images.githubusercontent.com/37144967/137588781-a6d5b908-e98f-4f9d-a3b6-4562cc572e4a.JPG)



## Details and Used Technologies
- MS Sql server is used for saving locations, rooms, inventories, room-inventory reservations for keeping-showing relations between entities  
- Generic repository pattern is used for DB operations (Please see under MeetingReservationApp.Shared - IEntityRepository.cs). This reduces code duplications, if any entity is added to the project, use this interface for DB operations.
- MeetingReservationApp.Shared - IEntityRepository.cs interface is implemented by EfEntityRepositoryBase.cs, in the future if we decide to use a different kind of DB provider, we are able to implement this interface by another specialized concrete class 
- Also entity abstraction, result, message, enum infrastructure is created under MeetingReservationApp.Shared
- Under MeetingReservationApp.Data project, abstract repositories for all entities are created implementing IEntityRepository, EFRepositories are implementing those interfaces, these are created for improving extendiblity of project, if any special method needed for an entity, it should be created and used in there 
- Fluent API is used for creating tables, defining columns and designating relations between DB tables and seeding initial data (Please see Mappings folder under MeetingReservationApp.Data)
- UnitofWork design pattern is implemented for modifying multiple entities in one transaction (ie. create room reservation and automatically reserve its inventory if its available)
- Bussiness rules and requirements are defined under MeetingReservationApp.Managers manager classes - IUnitOfWork interface is injected to their constructers, in order to use Dependency injection and make parts of the project loosely coupled

DB Diagram:
![image](https://user-images.githubusercontent.com/37144967/137592722-69cf89e9-f46b-4c67-a3a4-3738885a811e.png)
