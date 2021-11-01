## Application de gardiennage d'animaux 


#### Business logic
```mermaid
 classDiagram
      User "1" -- "0..n" Pet
      User "1" -- "0..n" Review
      User "1" -- "0..n" Availability
      User "1" -- "0..n" Message
      class User {
            +int UserID
            +string UserName
            +string Email
            +string Address
            +int PhoneNumber
            +List<Pet> Pets
            +List<Availability> Availabilities
            +List<Review> Reviews
            +List<Message> Messages
      }
      class Pet {
		+int PetID
		+string Name
		+enum Specie
            +int Age
            +string PhotoURI
            +bool isBeingSitted
            +int UserID
	}
      class Review {
            +int ReviewID
            +int Stars
            +string Comment
            +DateTime TimeStamp
            +int WrittenTo
            +int WrittenBy
      }
      class Availability {
            +int AvailabilityID
            +DateTime StartDate
            +DateTime EndDate
            +DateTime UserID
      }
      class Message {
            +int MessageID
            +string Content;
            +DateTime TimeStamp;
            +int Sender
            +int Recipient
      }
```
#### Liens
Syntaxe pour [Markdown](https://www.markdownguide.org/basic-syntax/)

Syntaxe pour les diagrammes de classe [Mermaid](https://mermaid-js.github.io/mermaid/#/classDiagram)
