## Application de gardiennage d'animaux 

@startuml component
actor client
node app
database db

db -> app
app -> client
@enduml
