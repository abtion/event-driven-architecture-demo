# Event Driven Architecture Demo
This demo uses principles from Event Driven Architecture to show how using events will change the way you whould design your database.

## What is MusicRequestApp
### Epic User Story
As a DJ I want to have an online system for song requests so that the guests at a party do not have to interact with me physically during a party

### User Stories
- As a party guest I want to request a song so that I can spread my love for music to everyone (whether they want to hear it or not)
- As a DJ I want to see what songs have been requested
- As a DJ I want to update the list of requests when I have played a song
- As a DJ I want to reject a song request that I don't to play
- As a party host I want to register my party so that guests can request songs

## Projects

### Console
Application to access the Domain Model and create events through the business logic layer.

### DomainModel
The business logic layer.

### DomainModel.Events
Shared files between the business logic layer and the data access layer (seperated to avoid circular references between the two layers).

### Storage.CosmosDb
Cosmos DB specific services that are responsible for storing events in the database.

## Getting Started
1. Clone the repo
2. Download and install Azure Cosmos DB Emulator
    - [https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21)
3. Start Cosmos DB Emulator
4. Run the Console-project
5. Open Cosmos Db Emulator Explorer to see the data in the database

## Future expansions
Firing events (messages) when an event is stored in the database to demonstrate the use of event handler / event consumers. The idea is to build up a projection layer that is stored in another container in Cosmos.