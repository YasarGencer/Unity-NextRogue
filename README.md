# Unity-NextRogue

<h1 align = "center"> INTRODUCTIONS </h1>

- [Game Design Document](#gamedesignDoc)
    - [Game Details](#gameDetails)
    - [Gameplay Elements](#gameplayElements)
      - [Player Mechanics](#playermechanics)
        - [Spells](#playerSpells)
        - [Consumables](#playerConsumables)
      - [Playable Characters](#playableCharacters)
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

Main combat will be built on wizardry. There will be more than 3 unlockable characters in the game. Player can open them by playing the game.

There will be personality trait actives and passives.

Actives :
  - Primary Support : PS
  - Secondary Support : SS
  - Primary Attack : PA 
  - Seconray Attack : SA

There are no strict restrictions on passives so they will have different and quite unique passives. 


<h4 align = "center" id = "playerSpells">SPELLS</h4>

And there is spells. Any character has 5 skill slots which player can attach different spells on each run. Player will find new spells while on chests or they will be given on after boss fights.

Oher characters attacks might be used as spells in game.

Spell List :
  - Reaper
    - Summons a reaper that seings itself and damages the enemies on the way
  - Dual Shot
    - Shoots a dual projectile that explodes and give AoE on contact
  - _zacQ
    - Throw a projectile to cursor
    - On hit throw a secon one to different enemy
    - If both hits different enemies projectiles hit enemies to eachother
  - _ice particles
    - Throws 5 ice pieces in a 30 degrees between them
    - Slowes enemies down on contact 

<h4 align = "center" id = "playerConsumables" >PLAYER CONSUMABLES</h4>

  - Souls 
    - Used On Spell Casting
    - Gained By Killing Humans

<h3 align = "center" id = "playableCharacters"> PLAYABLE CHARACTERS</h3>

There will be different and unique wizards to play as i said before. So there is a list of them

  - NECROMANCER:
    - PS: Necrotic Dash
      - It dashes more if there are dead bodies around
    - SS: Consume
      - Consumes alive enemies or summons to gain health
    - PA: Summon Aid
      - Summon [summons](#nonPlayers) to help the player
      - Summons will die in a short time
    - SA: Grave Keeper
      - Revive [summons](#nonPlayers) from dead enemy bodies
  - LIGHTNING:
    - PS: Bolt Dash
      - If player dashes to an enemy it will zap around other enemies
      - Damages and shocks on contact
    - SS: Elecrticity Aura
      - Creates a aura that will dissapear on time.
      - Enemies in the aura will get shocked
    - PA: Lightning Streak
      - Shoots lightning from players hand
      - Attack will zap around other enemies on contact
    - SA: Summon Thunderbolt
      - Summons a thunderbolt that will do AOE to nearby enemies 

<h3 align = "center" id = "nonPlayers"> NON PLAYERS</h3>

Tere is two types of non player characters in the game. Summons and enemies. Player can summon summons(obviously) and enemies spawns randomly on the map. Summons has a short lifespawn. When non playables die they leave a corpse on the map. The corpse of enemy can be summoned back as summon and fight beside the player against other enemies but it also will have short lifespan this time. 

Non player character list:
  - Axe Skeleton
  - Sword Skeleton
  - Hunter

<h1 align = "center" id = "visuals"> VISUALS </h1>

<h2 align = "center" id = "UI"> USER INTERFACE </h2>

<h3 align = "center"> GUI </h3>
<p align = "center">
<img src="img/GUI&HUD.png" width="500" alt="PlayerMechanics">
<p>
<p align = "center">
<img src="img/GUI&HUD2.png" width="500" alt="PlayerMechanics">
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