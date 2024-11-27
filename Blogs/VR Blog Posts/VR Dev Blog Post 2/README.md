# VR Dev Blog Post 2
**Authors**: Anders Hellesøe

## Warden AI

In Minecraft the main gimmick of the Warden is that it's meant to be something that you slowly sneak around and avoid conflict with. As such a faithful version would be blind and then have it use sound and try and sniff where the player is once it gets closer, before attacking the player.

So the 2 most important thing initially is for the Warden to even be able to pathfind around in the maze, and then maybe to listen for sounds.

So pathing, we are a bit lucky that we have a grid based maze, and while graph structure doesn't matter a great deal for pathing algorithms, they are very easy to set up for grids. Our team has had previous experience with the A* algorithm, and therefor already had an implementation designed for use in grid based graphs. It was however designed for waited graphs and not boolean mazes. We instead modified the algorithm to consider walls as having an extreme weight and paths having a weight of 1. This allows the Warden to walk through a wall but only of no other reasonable path can be found.

![Warden pathfinds](./media/wardenPathfinds.gif)

When the Warden path finds it will select a psudo random grid square and use the above mentioned algorithm to generate a path to this square, this path is just a list of grid squares and not specific world coordinates. The Warden will then update it's position and rotation towards the next checkpoint. A new checkpoint is gotten chosen a set length before reaching the previous checkpoint, this smooths out the pathing quite a bit, as it allows the Warden to cut corners.

In the above GIF a window, marked in blue centered on the red cross, can be seen. A pathing goal is picked at random inside this window, in oder to have to Warden pick random paths that have a higher chance of intersecting the player. This makes it less likely that the Warden just pathfinds in corder far away and never encounters the player, which can be a real problem for mazes with larger dimensions.