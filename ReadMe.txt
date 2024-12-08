********************************************

		Read Me 

********************************************

Controls: 

WASD - Movement
L.Shift - Sprint
L.Ctrl - Sneak
Space - Jump/Double Jump
Q - Takedown
E - Teleport

********************************************

Level Explanation. 

Level 1: The idea is to get used to the movement, test sprint and sneak. The cylinder is an NPC with no movement/attack enabled. It turns red when a takedown is available. The area is meant as a tutorial to understand the takedown and movement. All other NPCs have a texture and animation added and do not show takedown availability. There is also a black cube used to demonstrate collider physics. 

Level 2: Single NPC on patrol, used to demonstrate NPC movement and state transitions. It randomly starts searching for the player while on patrol. When it detects the player it chases and attacks the player. If the player is hit the game ends and time stops. The skeletons detection has been set up to work in a limited range and area to demonstrate weak vision and lack of hearing. 

Level 3: This is a parkour segment to demonstrate the jump and double jump mechanics. It also shows the effect of momentum on the jump. 

Level 4: Realistic body of water to demonstrate water physics. Ice cube melts over time to show change in geometry over time and the trigger at the end requires the player and the block on it as it is mass activated. 

Level 5: The item unlocks the teleport ability (E) which allows the player to teleport to a location. The item also triggers a change in the plane to be a sand area and demonstrates sand physics. To unlock the door, use teleport to scale the platform towards the entrance and unlock the door with the trigger. 

Level6: Last level of the game has 3 NPCs guarding the final key, Collecting the final key opens the door and walking through the door marks the end of the game and a WIN!. The end also demonstrates an environment that appears infinite. 

*********************************************

Assets/Libraries used:

1) 4K Tiled Ground Texture (https://assetstore.unity.com/packages/2d/textures-materials/4k-tiled-ground-textures-part-1-269480) - Used the sand texture on the ground in level 5. 

2) AQUAS Lite (https://assetstore.unity.com/packages/vfx/shaders/aquas-lite-built-in-render-pipeline-53519) - used the water texture for water bodies on the menu and in level 4

3) Customizable Skybox (https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576) - Used in the menu scene (was originally a part of the game but replaced by unity built in procedural sky box) 

4) Dwarven Expedition Pack (https://assetstore.unity.com/packages/3d/environments/dungeons/dwarven-expedition-pack-stylized-assets-155149) - Used for map building. Floors, walls, pillars and platforms, doors and frames. 

5) Effects textures and prefabs (https://assetstore.unity.com/packages/vfx/particles/effect-textures-and-prefabs-109031) - Used the smoke material in the particle system under the player game object to show dust. 

6) Stylized Free Skeleton (https://assetstore.unity.com/packages/3d/characters/creatures/stylized-free-skeleton-298650) Used models and animation to add more depth to the NPCs. 

7) NAV-Mesh AI - I had to import this from GitHub but never used it, had an issue with unity but was resolved later. 

8) Flickering light - Not used. 