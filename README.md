## Application de gardiennage d'animaux 

```mermaid
 classDiagram
      User "1" -- "0..n" Pet
      User "1" -- "1..n" PhoneNumber
      User "1" -- "0..n" Review
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
