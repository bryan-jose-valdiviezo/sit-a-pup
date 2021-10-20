## Application de gardiennage d'animaux 

```mermaid
 classDiagram
      User "0..1" -- "*" Pet
      User "0..1" -- "*" PhoneNumber
      User "0..1" -- "*" Review
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
```
