Computer technology for game development

DISCLAIMER: There is a folder that is called ECS Lecture which contains scripts that are not used. They are scripts from the lectures. There is also a folder called deprecated where scripts that are not in use but I use for reference. 

Enemies:
For the enemies I originally wanted to use the Job system for the multi thread feature. However it became a little difficult to destroy the enemies once they were done with their traversal so I switched to just running a query. However the performance should not be that much different unless you spam the spawn button. While spawning the enemies by accessing the world and creating a new entity command buffer every time might not be the best for performance I wanted to explore the system and what can be done.
Bullets
The bullets like the enemies were supposed to create jobs to move however I figured the performance gain from destroying them after some time would be greater than in the long run rather than using jobs to move them.
