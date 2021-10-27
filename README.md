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
            -int id
            -string userName
            -string email
            -string address
            -List<PhoneNumber> phoneNumbers
            -List<Pet> ownedPet
            -List<Pet> keptPet
      }
      class PhoneNumber {
            -int phoneNumber
            -enum type
      }
      class Pet {
		-int id
		-string petName
		-enum petSpecy
            -int age
            -string photoURI
            -boolean dangerous
            -boolean exotic
	}
      class Review {
            int id
            int stars
            string comment
      }
      class Availability {
            int id
            DateTime availabilityPeriod 
      }
      class Message {
            int id
            string content;
            DateTime timeStamp;
      }
```
#### Liens
Syntaxe pour [Markdown](https://www.markdownguide.org/basic-syntax/)

Syntaxe pour les diagrammes de classe [Mermaid](https://mermaid-js.github.io/mermaid/#/classDiagram)
