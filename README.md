## Application de gardiennage d'animaux 

###Avant de lancer:

1- Créer le répertoire C:\sqlite

####Dans le Package Manager Console de Visual Studio: 

2- Sélectionner comme Default project: Service_Final_Web_API
3- Entrer la commande: Update-Database

Tous les projets doivent être configurés pour démarrer dans les propriétés de la solution.

####N.B.: 

Le projet Service_Final_Web_API est un service REST qui répond à des requêtes pour alimenter un site de gardiennage d'animaux, et qui gère les interactions avec la base de données.

Le projet web3_tp_final est un site Web destiné aux utilisateurs et qui utilise les services de Service_Final_Web_API.

Le projet SitAPup admin est un site Web destiné aux administrateurs et qui utilise les services de Service_Final_Web_API.
