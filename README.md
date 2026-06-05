## WallMeet Forum
Forum de discussion en ASP.NET Core, conçu autour des bonnes pratiques du cours de sécurité web ( Consigne du TP: [tp05 (1).pdf](https://github.com/user-attachments/files/28626297/tp05.1.pdf) ).

<img width="1912" height="942" alt="image" src="https://github.com/user-attachments/assets/67706ad7-f6b0-4d7b-a9eb-14f07c7e3e98" />

<img width="1892" height="942" alt="image" src="https://github.com/user-attachments/assets/924a76b6-6880-4330-8511-47903f6bedc7" />

<img width="1897" height="942" alt="image" src="https://github.com/user-attachments/assets/746ae235-31b3-474a-8d61-da5dedfa52d3" />


## Aperçu
Projet scolaire , dans le cadre du cours de sécurité informatique. WallMeet est un forum « mur » où les utilisateurs connectés publient des messages et y ajoutent des commentaires.
L'objectif principal n'est pas seulement le fonctionnement du forum, mais l'application concrète des bonnes pratiques de sécurité web : hachage des mots de passe, gestion sécurisée des sessions, protection contre le XSS et le CSRF, en-têtes de sécurité et journalisation des actions sensibles.


## Fonctionnalités

Authentification par session (connexion / déconnexion), redirection des visiteurs non connectés <br>
Mur de messages avec commentaires, triés du plus récent au plus ancien  <br>
Pagination (5 messages par page)  <br>
Rôle administrateur (suppression de messages)  <br>
Journal d'audit (AuditLog) des actions sensibles : connexions réussies/échouées, suppressions  <br>
Comptes de test créés automatiquement au démarrage (voir comptes.txt)  <br>

## Sécurité

Mots de passe : hachage BCrypt (salt automatique) + pepper applicatif ; le mot de passe n'est jamais stocké ni mis en session  <br>
Sessions : cookies HttpOnly, SameSite=Strict, expiration après 30 min  <br>
XSS : encodage automatique des sorties par Razor + Content-Security-Policy  <br>
CSRF : jetons antiforgery intégrés aux pages Razor  <br>
En-têtes HTTP : X-Content-Type-Options, X-Frame-Options: DENY, Referrer-Policy, Content-Security-Policy  <br>
Transport : redirection HTTPS et HSTS (en production)  <br>
Validation des entrées : contraintes sur les champs (longueur, format courriel, champs requis)  <br>

## Stack technique

Langage : C#  <br>
Framework : ASP.NET Core Razor Pages (.NET 10)  <br>
ORM / BD : Entity Framework Core + SQLite (migrations incluses)  <br>
Sécurité : BCrypt.Net-Next  <br>
Architecture : modèles / pages Razor / accès aux données (ApplicationDbContext, DbSeeder) / utilitaires  <br>

## Comment démarrer
bashdotnet restore   <br>
dotnet run  <br>
 <br>La base SQLite est créée et alimentée automatiquement au premier lancement.
Comptes de test (voir comptes.txt) 

Statut :  Terminé



