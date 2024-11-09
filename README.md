# Spacy Invaders

Bienvenue dans **Spacy Invaders** ! Ce projet est une version revisitée du célèbre jeu Space Invaders, offrant différentes difficultés et une gestion des meilleurs scores.

## Fonctionnalités
- Choix de difficulté :
  - 0 : Trop facile
  - 1 : Facile
  - 2 : Moyen
  - 3 : Difficile
  - 4 : Très difficile
  - 5 : Impossible
- Système de meilleurs scores pour garder une trace des meilleurs joueurs.

## Installation

1. Clonez le dépôt GitHub :
   ```bash
   git clone https://github.com/Timcodingeur/spacy_invaderss
   ```

2. Naviguez dans le dossier du projet :
   ```bash
   cd spacy_invaderss
   ```

3. Ouvrez le fichier solution (`spacy_invaderss.sln`) avec **Visual Studio** et lancez le projet.

## Configuration de la Base de Données
Le jeu utilise une base de données MySQL pour enregistrer les meilleurs scores. Vous devez configurer un container Docker pour la base de données.

Voici les informations de connexion actuellement utilisées :
- Serveur : `db`
- Base de données : `db_space_invaders`
- Utilisateur : `root`
- Mot de passe : `root`
- Port : `3306`

Assurez-vous de créer un container Docker nommé `db` pour que le jeu puisse se connecter correctement à la base de données.

## Jouer au Jeu
Choisissez votre niveau de difficulté préféré et essayez de battre les records ! Les meilleurs scores seront enregistrés dans la base de données et pourront être consultés à tout moment.

Amusez-vous bien avec **Spacy Invaders** !

