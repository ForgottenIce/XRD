# Anders' Personal Reflections

## Contributions

- AR
  - Tracking
  - Spawning the Model
  - Orientating the Model
  - The details of how to point at a parking space. Both with the directional arrow and the arrow hovering above the parking space.
- VR
  - Maze generation and scanning
  - Building the maze using prefabs
  - The dictionary system for defining cell types
  - Pathfinding
  - "Sound event" system
  - Aggression systems
  - Lobby intractables
  - Win condition

## Thoughts on AR

For me I think AR is a difficult topic. I marvel at the possibilities of AR and how many things it could be used for, but almost every idea runs into some kind a technology hindrance. Most notably is tracking in general, tracking anything markerlessly can be a struggle. As you are continuously comparing a live video of the real world, to your digital counterpart, it quickly can get a lot worse when the system cant find any or many good comparison points. 

This was a large problem during our implementation, as we would anchor on the initial sign, and then would not really recalibrate on anything after turning away. We originally wanted to try can recalibrate on parking lines or comparing the distance to several lampposts. Sadly we didn't manage to implement having several types of anchors. Part of the reason for not using other anchor types, was that it could be quite difficult to figure out how any of the unity components actually worked.

One topic that was often brought up, was the inclusion of IR cameras in many Apple products and their exclusion from many Android products. We developed exclusively for Android, both since we didn't have any Apple products to use, but also because we had heard many horror stories about how bad the Apple development experience is. The improvements in how IR can measure distances could maybe have been a big improvement, but I doubt it could complete solve these issues mentioned.

Personally I think the biggest problem with AR is that there isn't really a good medium to use for it. While most everyone always has a smartphone on them, it is kind of intrusive to hold a phone at every AR thing you wanna see. There are a large amount of AR glasses in development, but a lot of them currently seem almost as intrusive as the phones, and they aren't really useful enough to be an everyday technology yet.

## Thoughts on VR

This project was my first experience in using or working with VR, as such I had no ideas how any of the systems I worked on actually felt in the game, for a long time. It was a big help to have team mates that could test it out at home. Shockingly the experience of developing VR projects in unity wasn't completely bad, if you hadn't had the ability to use a headset for testing, but the headset can make a big difference.

When developing XR in unity unity gives a ton of different components to help with several common task. This both makes it so the developer has to learn how to work with these pre-made scripts, but also of course allows the developer to focus on more in depth systems. I think it's a bit of a double edge sword, as often the documentation is not very helpful, in figuring out how to work with the components. Once you get them working, then the product of it works great.

In order to have the Warden feel scarry without it being so challenging to were it gets very easy to get used to dying. We came up with having it have a grace period between when the Warden can run at the player and when it actually attacks. I think this borders on making it more of an enemy to handle, then a unsetting blind force to avoid. But I think driving home the blind parts would be best implemented with several other systems.

A big import part of VR, is how immersive it is. A big part of this is sound, for this we went trough a ton of the sound files from Minecraft, and selected the once we thought fit best in the scenarios we had. The addition of heartbeats allows the player to hear how far away the Warden is, And many other sounds can give information of what the Warden is doing, or just generally set the mood. Without sound it was a lot harder for the player to have suspension of disbelief, in how unreal the situation is.

I have been exited for the prospects of VR entertainment for a long time, and continue to be hopeful for the idea of good haptic feedback and or full dive VR technologies to come in the future. I think VR currently struggles a good handful of challenges like, space requirements, lack of mainstream viability as a gaming console, and difficulty marketing it outside specific training applications. After trying it this semester I still feel like it isn't at the point of something I envy not having.