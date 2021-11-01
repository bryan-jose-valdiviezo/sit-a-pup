## Application de gardiennage d'animaux 


#### Business logic
```mermaid
 classDiagram
      User "1" -- "0..n" Pet
      User "1" -- "1..n" PhoneNumber
      User "1" -- "0..n" Review
      User "1" -- "0..n" Availability
      User "1" -- "0..n" Message
      class User {
            +int UserID
            +string UserName
            +string Email
            +string Address
            +List<Pet> OwnedPet
            +List<Pet> KeptPet
            +List<Availability> Availabilities
            +List<Message> Messages
      }
      class Pet {
		+int PetID
		+string Name
		+enum Specy
            +int Age
            +string PhotoURI
	}
      class Review {
            +int ReviewID
            +int Stars
            +string Comment
      }
      class Availability {
            +int AvailabilityID
            +DateTime availabilityPeriod 
      }
      class Message {
            +int MessageID
            +string Content;
            +DateTime TimeStamp;
      }
```
#### Liens
Syntaxe pour [Markdown](https://www.markdownguide.org/basic-syntax/)

Syntaxe pour les diagrammes de classe [Mermaid](https://mermaid-js.github.io/mermaid/#/classDiagram)
