# Booking Room API

## 💻 Project
API developed to booking a room.

[![Generic badge](https://img.shields.io/badge/Made_with-.Net_5-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/Designer_Pattern-DDD-red.svg)](https://shields.io/)

## Technologies
- .Net 5
- Entity Core
- Fluent Validation
- AutoMapper
- Dependency Injection

## Executing the project
- InMemory
  1. Set up the ***BookingRoomAPI*** as Startup Project.
  2. Run the project.

-SQL Server
1. In ***IServiceCollectionExtensions***, comment or remove the InMemory configuration and uncomment SQL Server configuration
2. In ***Startup*** file, remove or comment the lines inside ***Room Test InMemory Data*** region, in ***Configure*** method
3. Update ***appsettings*** file and set up the SQL Server ConnectionString;
4. In the ***Package Manager Console***, set up the ***Defaul Project*** to ***BookingRoomAPI.Infrastructure***
5. In the ***Package Manager Console***, run Migration to create the database. ```update-database```
6. Set up the ***BookingRoomAPI*** as Startup Project.
7. Run the project.

## Usage
The project can be tested using the Swagger already added to the project.  

## Bussines Rules
- All reservations start at least the next day of booking;
- The stay can’t be longer than 3 days;
- It can’t be reserved more than 30 days in advance;
- To simplify, the API is insecure.

## References
- <a href="https://sloboda-studio.com/blog/how-to-create-a-hotel-booking-website/">Understand how to create a Hotel Booking</a>
- <a href="https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx">Entity Framework Core Tutorial</a>

## 🤔 Contributing

- Fork this repository;
- Create a branch with your feature: `git checkout -b my-feature`;
- Commit your changes: `git commit -m 'feat: My new feature'`;
- Push to your agency: `git push origin my-feature`.

Once your pull request has been merged, you can delete a branch from yours.

## License
[MIT](https://choosealicense.com/licenses/mit/)
