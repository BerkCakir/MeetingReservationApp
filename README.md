
# Meeting Reservation APIs and Web UI
- Solution contains two APIs, one is designed for searching and reserving a room at the office. The second one is designed to reserve an inventory for a room reservation.
- Identity server is used for authentication. Resource owner credentials grant type token authentication is applied to above APIs. 
- Gateway project(Ocelot) is created for addressing APIs. Also Gateway is the first step for checking authentication token before sending requests to APIs.  
- MVC Web UI project, consuming above APIs, login-logout mechanism using IdentityServer API.  
- Shared folder and related class libraries are only referenced by APIs.
- Test project for testing RoomReservationAPI

![meeting](https://user-images.githubusercontent.com/37144967/137588781-a6d5b908-e98f-4f9d-a3b6-4562cc572e4a.JPG)



## Details and Used Technologies
- .Net Core 5.0 is used for API projects, .Net Core 3.1 is used for IdentityServer(because its the latest version can be used free), Asp.Net Core MVC is used for Web UI
- MS Sql server is used for saving locations, rooms, inventories, room-inventory reservations for keeping-showing relations between entities  
- Generic repository pattern is used for DB operations (Please see under MeetingReservationApp.Shared - IEntityRepository.cs). This reduces code duplications, if any entity is added to the project, use this interface for DB operations.
- MeetingReservationApp.Shared - IEntityRepository.cs interface is implemented by EfEntityRepositoryBase.cs, in the future if we decide to use a different kind of DB provider, we are able to implement this interface by another specialized concrete class 
- Also entity abstraction, result, message, enum infrastructure is created under MeetingReservationApp.Shared
- Under MeetingReservationApp.Data project, abstract repositories for all entities are created implementing IEntityRepository, EFRepositories are implementing those interfaces, these are created for improving extendiblity of project, if any special method needed for an entity, it should be created and used in there 
- Fluent API is used for creating tables, defining columns and designating relations between DB tables and seeding initial data (Please see Mappings folder under MeetingReservationApp.Data)
- UnitofWork design pattern is implemented for modifying multiple entities in one transaction (ie. create room reservation and automatically reserve its inventory if its available)
- Bussiness rules and requirements are defined under MeetingReservationApp.Managers manager classes - IUnitOfWork interface is injected to their constructers, in order to use Dependency injection and make parts of the project loosely coupled
- Identity server is used for Authentication, role based Authorization could be used over it. Location of the user is stored on IdentityServer DB, if user changes location, it must be changed over IdentityServer. APIs are protected with resource owner client credentials, this means user and password must be supplied to use APIs. 
- Gateway with Ocelot is used for a better isolation of APIs and for a better security, Gateway is also protected with an auth token, so if a request can't pass gateway because of Authentication, it can't also make a redundant request to an API
- Cookie based authentication is used in Web UI project
- There are two APIs using a single database, the reason for creating multiple API is improving scalability. ie. APIs can be deployed to cloud servers and their resources can be scaled according to their traffic 

DB Diagram:

![image](https://user-images.githubusercontent.com/37144967/137592722-69cf89e9-f46b-4c67-a3a4-3738885a811e.png)


## How to Run

- Download project and open the project folder in VS Code
- Run docker-compose up command in terminal
- Wait for the image downloads, it'll take a while
- Type http://localhost:5010/ to your browser (Preferably Google Chrome) 
- Login creadentials are already set in login page
- After login - the top right menu can be used for displaying user profile and location
- RoomReservation and InventroyReservation links can be used after logging in

## Features to be developed

- Unit tests are not enough, much more unit test should be added
- Data annotations are used for validation on UI, I prefer to use FluentValidation library. Data annotations are not suitable for single responsibility principle 
- Indexes should be created on DB Tables, especially when searching date intervals they are important
- UI features are not user friendly, they are just created to show APIs functionalities
- UI controller codes should be refactored
- API documentation should be added (I can add it if requested) 
- I forgot to add user id to roomreservations table, it should be added because a reservation should be owned by a personnel
- I didn't add controls, like "start time should be greater then end time" or "you are trying to create reservation for past". This kind of controls should be added


## Bussiness Rules
- I assumed that every inventory is linked to a room at first
- If a room is reserved, fixed inventory of the room is reserved with the room
- If the inventory is not fixed and if any reservation doesn't exist for the inventory at the room reservation time, this inventory will also be reseved with the room too



  
## User Stories

Story 1

1.1) When home page loaded, hit RoomReservation link at the left top, because you’re not already logged in, it redirects you to login page
1.2) Click login and use default cridentials at the login page ( if needed Steve@domain.com, Password12*) This user is located in Amsterdam, and this information is stored in ReservationIdentityServerDB) Can also be seen in profile page, reachable from at the right top scroll menu. So this user can only list rooms in Amsterdam.
1.3) Click RoomReservation link, default values are set on the view but you are able to change
Select today, set start hour to 7, other values are not important, hit search
and get the below error:

![image](https://user-images.githubusercontent.com/37144967/137600082-524395ca-7b2a-4980-b5e0-284a31a75946.png)
  
1.4) Change start hour to 10:00 and end hour to 11:00 and hit search, 3 rooms will be listed:

![image](https://user-images.githubusercontent.com/37144967/137600107-3c776425-098a-4bda-b77a-355c88c09338.png)

1.5) Hit to reserve room 1, set a description and set attendant count to 20, hit reserve room and  below error appears:

![image](https://user-images.githubusercontent.com/37144967/137600129-88f8f60b-f975-46b9-a930-79fd629dd582.png)

1.6) Change the attendant capacity to 5 and hit reserve room. Now room is reserved for the desired time with its resources then you are redirected to InventoryReservation view and see all reservations for your location. You’ll see the second record (first one is seeded data). Room is reserved with its own inventories.

![image](https://user-images.githubusercontent.com/37144967/137600145-24f17959-e0b2-4eb8-849a-3a123dfb88c7.png)

1.7) On the InventoryReservation page, hit the reserve button from the second record. Inventory reservation page comes, now you can list only non-fixed inventories on this page.  

![image](https://user-images.githubusercontent.com/37144967/137600158-30560a9c-c4eb-4c25-8929-d716566cf9be.png)

1.8) Select “Television” and hit Reserve, you’ll get an error because our reservation contains a Beamer which has a common purpose with Television:

![image](https://user-images.githubusercontent.com/37144967/137600171-4a090027-a5bb-49a0-a4e3-f926eef8c659.png)

1.9) Then select “Video Conference Equipment” and hit reserve. You’ll see the third inventory is reserved for us.

![image](https://user-images.githubusercontent.com/37144967/137600182-eb92c8b5-f179-4aa7-bcb1-8892f6412790.png)


