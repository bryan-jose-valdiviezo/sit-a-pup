## Application de gardiennage d'animaux 


#### Business logic
```mermaid
 classDiagram
      User "1" -- "0..n" Pet
      User "1..n" -- "0..n" Review
      User "1..n" -- "0..n" Appointment
      User "1..n" -- "0..n" Availability
      User "1..n" -- "0..n" Message
      Review "0..1" -- "1" Appointment
      Pet "0..1" -- "0..1" PetAppointment
      Appointment "1" -- "1" PetAppointment
      class Admin {
            +long AdminId
            +string Name
            +string Password
      }
      class User {
            +long UserID
            +string UserName
            +string Password
            +string Email
            +string Address
            +string PhoneNumber
            +ICollection<Appointment> AppointmentOwners
            +ICollection<Appointment> AppointmentSitters
            +ICollection<Availability> Availabilities
            +ICollection<Message> Messages
            +ICollection<Pet> Pets
            +ICollection<Review> Reviews
      }
      class Pet {
		+long PetID
            +long BirthYear
		+string Name
            +byte[] Photo
		+string Specie
            +long UserId
            +User user
            +ICollection<PetAppointment> PetAppointments
	}
      class PetAppointment {
            +long PetAppointmentId
            +long PetId
            +long AppointmentId
            +Appointment Appointment
            +Pet Pet
      }
      class Review {
            +long ReviewID
            +long Stars
            +long AppointmentId
            +long UserId
            +string Comment
      }
      class Availability {
            +long AvailabilityId
            +string StartDate
            +string EndDate
            +long UserId
      }
      class Appointment {
            +long AppointmentId
            +long SitterId
            +string StartDate
            +string EndDate
            +long OwnerId
            +long IsActive
            +string Status
            +User Owner
            +User SitterId
            +ICollection<PetAppointment> PetAppointments
            +ICollection<Review> Reviews
      }
      class Message {
            +int MessageID
            +string Content;
            +string TimeStamp;
            +long Sender
            +long Recipient
            +long UserID
            +User user
      }
```
#### Liens
Syntaxe pour [Markdown](https://www.markdownguide.org/basic-syntax/)

Syntaxe pour les diagrammes de classe [Mermaid](https://mermaid-js.github.io/mermaid/#/classDiagram)

[Animation css](https://animate.style/)

Kit d'outils de développement [HTML/CSS](https://demos.creative-tim.com/now-ui-kit/index.html)
