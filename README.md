# PI3V-Miniproject

### Gif of Gameplay

![Visual](https://github.com/miracchr/PI3V-Miniproject/tree/main/GamePlayGif/ezgif.com-video-to-gif.gif)

### Overview of the Game:
The project is inspired by the game and HBO series The Last of Us but made as a first-person shooter game. The player walks around in an enclosed wasteland surrounding using keyboard controls, while being able to look around with the camera following the curser of the mouse. The goal of the game is to shoot as many infected as possible without getting infected yourself. The genre of the game is a first-person shooter game.


### The main parts of the game are:

•	Player – The player is controlled using the keyboard WASD or the arrow keys.  
•	Camera – The camera is set as a child of the player character and follows the mouse. This is done to mimic the way a human would look around, simultaneously rotating the body of the player so it moves forward in the direction of the camera, when pressing the W or up arrow.  
•	Shooting – Using raycasts to detect whether an infected is within shooting range, a small aim dot in the middle of the screen changes from red to green, signalizing whether the infected can be shot. The player shoots by pressing the left mouse button, and each time a small particle system called gunFlash is played, to visualize the shooting.  
•	Enemies – The infected spawns randomly from 10 different spawn positions and will start patrolling around the play field using NavMesh and a NavMeshAgent. If the player is in their sight range, they will start chasing the player, and when close enough they will start to attack the player. Each attack will damage the player with 20, removing 1/5 of the players maximum health with each attack. The infected has a health of 40 and needs to be shot twice to die.  
•	Play field – A closed off space, where the player can move freely. The play field is created with terrain tools and is baked using NavMesh.  
•	Health  – The player starts with a full health of 100, which is visualized by a health bar on the upper left corner of the screen. When the health reaches 0 the scene reloads and the kill count and health is reset.  
•	Score – Each time the player kills an infected the kill count, on the upper right corner of the UI, will go up by 1.  

### Game features:
•	The game keeps track of a killCount and the players health visualized by a health bar.   
•	The infected is spawned from a random spawnPoint in the play field.    

### How were the Different Parts of the Course Utilized:
Meshes and transforms    
-	Transform is used in every script from the game, in various forms. It is mostly used as transform.position to get or change the position of a given object, but is also used in the form of a list of spawn points in the EnemySpawn script and as transform.localRotation in the Player look script to rotate the playerbody accordingly when looking around.   
Cameras, Lighting and Materials   
-	All the materials are standard Unity materials, some with added textures.   
Rigidbodys, Physics, Joints and Colliders   
-	Rigidbodys is used in the PlayerMovement script to move the player using rigidbody.velocity.   
-	Colliders is used to detect when the Enemy has attacked the player by using the OnTriggerEnter method, where the Enemy has a collider, which is set as a trigger.   
##### Raycasting   
-	Raycasting has been used in both the Enemy script and the ShootingGun script by using a raycast hit object. In the enemy script it is used to check whether the walkingPoint that the infected is searching for when patrolling is on the ground. Furthermore the Enemy script also uses the Physics.CheckSphere to check whether the player is in either sightRange or attackRange. In the ShootingGun script the raycast hit object is used to check whether the infected is within shooting range and used to both change the color of the aim UI and call the TakeDamage method on the enemy.   
##### Unity UI   
-	Unity UI has been used to visualize the players health through a health bar, with a kill count that updates each time an infected is killed and with a aim that changes colors based on whether the infected is within shooting range.  
Levelbuilding and Navigation   
-	NavMesh, along with a NavMeshAgent, has been used to get the enemy to patrol and  chase the player when in sightRange.  
-	Terrain tools have been utilized to make the play field for the game.   
##### Noise and Particle System   
-	A particle system has been used to visualize the shooting, by playing the particle system each time the player shoots an infected and is controlled in the ShootingGun script.   
### Project Parts:
##### •	Scripts:
o	PlayerMovement – Used to move the player character around using the WASD or arrow keys, and keep track of the players health, as well as updating the healthbar in the UI.



o	PlayerLook – Used to allow the player to look around using the mouse and rotating the playerbody accordingly in the direction the player is looking.



o	ShootingGun – Used for checking if the enemy is in range with Raycasts, changing the color of the aim UI based on whether the player can shoot the enemy, playing a visual effect in form of a particle system when shooting, inflicting damage on the enemy, and updating the killCount on the UI. 



o	Enemy – Used for enemy patrolling around the play field using Navmesh, chasing the player, attacking the player, and keeping track of collisions with the player using the OnTriggerEnter method and inflicting damage on the player.



o	EnemySpawn – Used for spawning enemies randomly at different spawn locations set in the play field.



##### •	Models & Prefabs:
o	Tree prefabs for the terrain downloaded from https://assetstore.unity.com/packages/3d/vegetation/trees/realistic-tree-9-rainbow-tree-54622
o	Simple enemy prefab made from Unity Primitives 
##### •	Textures:
o	Moss texture for the terrain ground downloaded from https://www.textures.com/download/3DScans1025/142940
o	Fungus texture for the enemy material downloaded from https://www.textures.com/download/Moss0148/45924
o	Panorama picture for the walls downloaded from https://www.textures.com/download/HDRPanoramas0155/139277

##### •	Materials:
o	Basic Unity materials for player, gun, ground, enemies and walls.
##### •	Scenes:
o	The game consists of one scene.
##### •	Testing:
o	The game was only tested in Unity.

### Time Management
| **Task**                                                                | **Time it Took (in hours)** |
|--------------------------------------------------------------------------------|------------------------------------|
|     Setting up Unity, making a project in GitHub                               |     1                              |
|     Research and conceptualization of game idea                                |     1                              |
|     Searching for 3D models (trees) and making 3D models in Unity              |     1                              |
|     Making player movement                                                     |     0.5                            |
|     Making camera movement for player looking around                           |     0.5                            |
|     Making an Enemy AI using Navmesh, and reaserching how to use it            |     2.5                            |
|     Playtesting and bug fixing the Enemy AI                                    |     1                              |
|     Using Terrain Tools for map building and researching how to use it         |     1.5                            |
|     Making UI elements such as Healthbar, KillCount and Aim                    |     1                              |
|     Using Raycast to shoot enemies                                             |     1.5                            |
|     Making a gunflash using the particle system                                |     1                              |
|     Making enemies spawn randomly at spawnpositions                            |     1                              |
|     Playtesting and adjusting enemy amount                                     |     1                              |
|     Collision and bugfixing error updating the healthbar correctly upon collision between player and enemy                              |     2                            |
|     Code   documentation                                                       |     1                              |
|     Making readme                                                              |     1                              |
|     **All**                                                                    |     **18.5**                       |     
### Used Resources
##### •	First Person Movement in Unity
o	https://www.youtube.com/watch?v=Tz-2Z0vLLt8
##### •	Shooting with Raycast 
o	https://www.youtube.com/watch?v=THnivyG0Mvo&t=7s
##### •	Using NavMesh to make an Enemy AI
o	https://www.youtube.com/watch?v=UjkSFoLxesw
##### •	How to use terrain tools in Unity
o	https://www.youtube.com/watch?v=MWQv2Bagwgk
