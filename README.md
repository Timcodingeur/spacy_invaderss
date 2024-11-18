# Spacy Invaders

Bienvenue dans **Spacy Invaders** ! Ce projet est une version revisitée du célèbre jeu Space Invaders, offrant différentes difficultés et une gestion des meilleurs scores.

## Langages, Frameworks et Outils

![MySQL](https://img.shields.io/badge/mysql-%2300f.svg?style=for-the-badge&logo=mysql&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![Git](https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)

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

Vous avez tout ce qu'il faut dans le dossier [Docker_MySQL](./Docker_MySQL).
renommé de fichier oui.envmodel en .env

## Jouer au Jeu
Choisissez votre niveau de difficulté préféré et essayez de battre les records ! Les meilleurs scores seront enregistrés dans la base de données et pourront être consultés à tout moment.

Amusez-vous bien avec **Spacy Invaders** !

