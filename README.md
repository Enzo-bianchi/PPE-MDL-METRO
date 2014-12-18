PPE-MDL-METRO
=============

Nouvelle version de Maison des ligues avec l'interface metro

CHARTE GRAPHIQUE
================

  Utiliser des MetroPanel à la place des Groupbox avec un borderstyle "FixedSingle"
  Utiliser des fond blancs sur les controls
  Pour les MaskedTextBox, utiliser la couleur "Control" pour ressembler au MetroTextBox
  Pour les boutons normaux, utiliser "FontSize" = Medium  & "FontWeight" = Light . Pour les boutons de validation,
  Rajouter le "Highlight" avec style = red
  
  Evidemment utilisé au max les controls Metro !
  
INSTALLER LE FRAMEWORK
======================

Le dossier dlls contient :
  Le composantNuite modifié en Metro
  L'oracle DataAccess 4
  Et le framework Metro

Boite à outils
--------------
Pour avoir les controls Metro, il faut clique droit sur la boite à outil, Ajouter un onglet (Conseillé). Refaire clique droit
Sur le nouvel onglet, choisir les éléments, ensuite Parcourir et sélectionner les dlls MetroFramework.dll et MetroFrameworkDesign.dll
Il va signaler une petite erreur pas importante, continuer et Ok pour fermer les éléments. La boite à outils contiendra les controls
dans l'onglet

Form
----

Pour faire une nouvelle Form, il faut créer une Form normalement (Nouvel élément, etc..) Une fois la form créée,
Ouvrir le code de la form et modifier :

public partial class FrmPrincipale : Form

public partial class FrmPrincipale : MetroFramework.Forms.MetroForm

Revener sur la partie graphique et vous aurez le nouveau design :D

MessageBox
-----------

 Le framework propose  égalemenet une MessageBox type windows 8
 Pour l'utiliser (standard) : MetroFramework.MetroMessageBox(this, "le message", "titre")
 Pour simplifier utiliser le using en debut de classe pour ecrire seulement : MetroMessageBox(this, "le message", "titre")
 
 Les boutons sont en anglais, je mettrai à jour les références une fois mise à jour
 
 Voila !!
 
