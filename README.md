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
1. Update ***appsettings*** file and set up the SQL Server ConnectionString;
2. In the ***Package Manager Console***, set up the ***Defaul Project*** to ***BookingRoomAPI.Infrastructure***
3. In the ***Package Manager Console***, run Migration to create the database. ```update-database```
4. Set up the ***BookingRoomAPI*** as Startup Project.
5. Run the project.

## Usage
The project can be tested using the Swagger already added to the project.  

## Bussines Rules
- All reservations start at least the next day of booking;
- The stay can’t be longer than 3 days;
- It can’t be reserved more than 30 days in advance;
- To simplify, the API is insecure.


## 🤔 Contributing

- Fork this repository;
- Create a branch with your feature: `git checkout -b my-feature`;
- Commit your changes: `git commit -m 'feat: My new feature'`;
- Push to your agency: `git push origin my-feature`.

Once your pull request has been merged, you can delete a branch from yours.

## License
[MIT](https://choosealicense.com/licenses/mit/)
