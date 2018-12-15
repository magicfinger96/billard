# Pool Game

# Tâches

# Andréa
* Billard :
  * Création du modèle du billard sur Blender: séparation en 2 objets du tapis avec le reste du billard pour appliquer des physicals matérials différents.
  * Ajout des sons: de l'arceau, d'un cylindre, des bords, d'une balle (quand elle tombe dans le trou et collision avec une autre)
  * 

# Nicolas
* Auberge : 
  * Recherche des assets 
  * Construction de l'auberge avec placement et recherche des meilleurs jeux de lumière pour ambiance
  * Feu de cheminée avec système de particules multiples (feu, fumée, étincelle)
  * Interaction avec l'environnement
  * Sons et musique pour l'auberge
    * Musique d'ambiance médiévale
    * Crépitement du feu
    * Bruit des pas aléatoires pour casser monotonie
    * Grincement lors de l'ouverture des portes
    * Son général sur l'interface lors des clics sur les boutons pour avoir un feedback
* Contrôle de personnage :
  * Un modèle 3D animé est jouable lorsque l'on se trouve dans l'auberge.
  * z,q,s,d pour se déplacer comme les RPGs habituels.
* Système de level :
  * Personnage peut monter en niveau en complétant des quêtes ou en finissant des parties de billard (xp gagné basé sur les performances).
  * Sur l'UI, une barre de niveau est présente en bas à gauche avec le niveau correspondant pour suivre la progression.
* Système de dialogue :
  * Possibilité de parler avec des PNJs présents dans l'auberge afin d'en connaître plus sur l'environnement du jeu. 
  * Dialogue à choix multiples
  * Possibilité d'obtenir des quêtes via des discussions.
* Système de quête :
  * Après avoir parlé au PNJ, il est possible d'avoir une quête
  * Il y a des quêtes qui peuvent être refusée et d'autres pas.
  * Une fois une quête acceptée ou refusée, le dialogue pour la quête ne sera plus possible avec le PNJ.
  * Lorsque la quête est accepté, elle s'ajoute sur le panneau d'affichage des quêtes (accessible via "Echap"). On peut cliquer sur la quête pour obtenir des informations supplémentaires comme les objectifs ou les récompenses.
  * Lorsqu'un quête est terminée, un message vient prévenir le joueur pour dire que celle-ci est complétée et les récompenses sont attribuées au joueur.
* Système d'interaction :
  * Plusieurs élements sont interactifs dans l'auberge :
    * On peut parler au PNJ
    * Pour jouer au billard, on doit s'approcher de lui et cliquer sur la touche F (les touches d'interactions sur précisé sur l'UI lorsque l'on s'approche).
    * On peut ouvrir les portes.
    * Lorsque des actions sont impossibles (comme jouer au billard sans avoir terminé la quête "trouver l'arceau"), un message sur l'UI s'affiche pour préciser la cause.
* Plusieurs effets de post-processing :
  * Ambient Occlusion
  * Antialiasing
  * Eye Adaptation
  * Bloom
  * Vignette
  * Color Grading (Tonemapper filmic)
    
    
