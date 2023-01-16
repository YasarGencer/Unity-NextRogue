# Unity-NextRogue

<h1 align = "center"> INTRODUCTIONS </h1>

- [Game Design Document](#gamedesignDoc)
    - [Game Details](#gameDetails)
    - [Gameplay Elements](#gameplayElements)
      - [Player Mechanics](#playermechanics)
          - [Movement](#playerMovement)
          - [Combat](#playerCombat)
          - [Consumables](#playerConsumables)
      - [Non Players](#nonPlayers)
     - [Visuals](#visuals)
       - [User Interface](#UI)
 - [Development Flow](#developmentFlow)


<h1 align = "center" id = "gamedesignDoc"> GAME DESIGN DOCUMENT </h1>

<p align = "center"><img src="img/necromancer1.jpg" alt="Necromancer1"><p>

<h2 align = "center" id = "gameDetails"> GAME DETAILS </h2>

- <p> Name :        Project - NextRogue</p>
- <p> Genre :       Action - RogueLike/Lite</p>
- <p> Art Style :   2D - TopDown</p>
  
<h2 align = "center" id = "gameplayElements"> GAMEPLAY ELEMENTS </h2>

<h3 align = "center" id = "playermechanics" >PLAYER MECHANICS</h3>

I want a fast gameplay so fluid character controller is mandatory. 

<p id = "playerMovement" >Player Movement:</p>

Main combat will be built on necromancy. So there is two types of basic atacks. 

Melee one deals little or no damage. It's main purpose is to swing enemies back.

Ranged one is one touch one cast spell. Which means there will be no casting time.
It deals little bit more damage than melee attack.

<p id = "playerCombat" >Player Combat:</p>

  - Basics
    - Primary Support
      - Air Dash
        - Dashes longer and throws enemies away
      - Fire Dash
        - Dashes and sets the path on fire
    - Secondary Support
      - Healh
        - Heals player
      - Shield
        - Creates a one time use shield
    - Primary Attack
      - Reaper  
        - Instantiates a reaper that swings itself
    - Secondary Attack
      - Dual Shot
        - Shoots a dual projectile
  - Spells
    - Necromancy 
      - _summonDeadEnemies
        - Summons friendly creatures from enemy bodies in range
        - Doesn't consumes soul but has a cooldown.
        - Low range
      - _summonOnCursor
        - Summons choosen creature to the mouse position
        - Consumes soul
        - Mid range
    - Other
      - _zacQ
        - Throw a projectile to cursor
        - On hit throw a secon one to different enemy
        - If both hits different enemies projectiles hit enemies to eachother

<p id = "playerConsumables" >Player Consumables:</p>

  - Stamina (CANCELLED)
    - Used On Physical Movement
    - Gained Over Time
  - Souls 
    - Used On Spell Casting
    - Gained By Killing Humans

<h3 align = "center" id = "nonPlayers"> NON PLAYERS</h3>

Tere is two types of non player characters in the game. Summons and enemies. Player can summon summons(obviously) and enemies spawns randomly on the map. Summons has a short lifespawn. When non playables die they leave a corpse on the map. The corpse of enemy can be summoned back as summon and fight beside the player against other enemies but it also will have short lifespan this time. 

<h1 align = "center" id = "visuals"> VISUALS </h1>

<h2 align = "center" id = "UI"> USER INTERFACE </h2>

<h3 align = "center"> GUI </h3>
<p align = "center">
<img src="img/GUI&HUD.png" width="500" alt="PlayerMechanics">
<p>

<h1 align = "center" id = "developmentFlow"> DEVELOPMENT FLOW </h1>


<h2 align = "center"> PLAYER CONTROLLER </h2>
<p align = "center">
<img src="img/PlayerController.png" width="500" alt="PlayerMechanics">
<p>
<h2 align = "center"> ENEMY CONTROLLER </h2>
<p align = "center">
<img src="img/EnemyController.png" width="500" alt="PlayerMechanics">
<p>