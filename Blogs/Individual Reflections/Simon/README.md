# Simon's Individual Reflections

## Contribution Highlights

**AR:**
- Performing 3D scans of the VIA Parking lot.
- Creating the VIA Parking lot 3D model in correct scale.
- Placing parking spots (or parking lines) on the parking lot 3D model.
- Creating 3D model for the calibration sign in correct scale.
- Creating the arrow 3D models used within the app.
- Helped adjusting the calibration sign positioning for better accuracy.

**VR:**
  - Adjustments to the XR Origin game object (controls, interactors, settings, etc.).
  - Creating the 3D building blocks for the maze (maze prefabs).
  - Created 3D models for interactables (buttons, levers, snowballs).
  - Worked on correctly importing Warden 3D model with animations.
  - Snowball interactables and related script logic.
  - Lever and button interactables and some of the related scripts.
  - Sound related scripts (footsteps and heartbeats).
  - Additions to the maze generation (floors, ceilings and end area).
  - DeathHandler (Warden collision detection).

## AR Project Reflections
For our AR project we worked on creating an application for finding your car in a parking lot in an immersive way. The idea was to overlay a 3D model of the parking lot on top of the real world, which is observed by the phone camera.

The first challenge to this problem is that an accurate 3D model of the parking lot is needed. Correctly scaling this 3D model is important in order to properly track it's positioning on top of the real world in the AR application. The first technique we attempted in order to get the correct scales of the parking lot was scanning it with a 3D scanner app on an iPhone. We specifically used an iPhone because it supports the LiDAR technology. Most android phones uses camera-based tracking, which is less accurate than the laser-based tracking that LiDAR leverages when creating 3D scans.

We ended up not using these 3D scans however, since they weren't accurate enough for such a large 3D model. Instead, we were fortunate enough to get in contact with "kort- og landmålingsuddannelsen" who kindly provided us with accurate measurements of the parking lot. This simplified the process of creating the 3D model significantly, and we ended up with a pretty accurate 3D model.

The second challenge is how we're going to actually track this 3D model on top of the real world. The approach we went with was image-based tracking. We considered combining this with GPS-based tracking, but ended up not doing that.

We chose to use a sign located on the VIA parking lot as our calibration/anchor point. Since we know the position and scale of this sign relative to our 3D model, it can be used to correctly overlay the 3D model on top of the real world. We used the `AR Tracked Image Manager` in Unity to accomplish this tracking.

The `AR Tracked Image Manger` is an abstraction used in Unity that, depending on the platform, utilizes different underlying technology to perform the tracking. In our case, the underlying technology used is ARCore, since our application is deployed to an Android device. ARCore primarily uses [simultaneous localization and mapping (SLAM)](https://developers.google.com/ar/develop/fundamentals), combined with the device's inertial measurement unit (IMU) to track the real world environment. It finds a large number of visually distinct points in the environment, called feature points. For every new frame received from the camera, it is then able to compute the device's change in location compared to previous frames.

ARCore actually managed to track the position of the parking lot quite well, even after walking several meters away from the sign. Our AR application ended up working quite well.

## VR Project Reflections
For our VR project, we created a Minecraft-inspired game where the Warden creature tries to catch you inside a randomly generated maze.

Some elements of the development process were similar to what I already had previous experience with in the GMD course. The big difference in this project is the use of a head-mounted display (HMD) and motion controls.

Luckily, implementing a VR application is simplified quite a bit in Unity, as the XR Interaction Toolkit does a lot of the heavy lifting. An XR Origin (or XR rig) game object provided by the toolkit is equipped with various components and scripts for locomotion and interaction with the game world.

To interact with our interactable objects, we utilized the `Near-Far Interactor` component for the controllers in the XR Rig. The `Near-Far Interactor` component has two modes to interact with interactable objects. The far caster uses ray casting which makes it possible to pickup objects from a distance by pointing a ray at them. We mostly used this for testing purposes. For the game itself, we utilized the near caster, which allows you to interact with an object when the controller is close enough to said object.

For the interactable game objects we used a combination of `XR Grab Interactable` and `XR Simple Interactable` to allow grabbing objects and detect events such as the user selecting and releasing the object with the controller. The interactables create a unique experience that's quite different from PC/console games. For example, instead of clicking a button to throw a snowball in our game, you instead physically create the motion of throwing a snowball. Not only does this make the game more immersive, but it also gives the player very intuitive control of the power and direction of the throw. 

For locomotion in our game, we decided to just use the joystick on the controller for moving around. It would obviously have been more immersive if the user simply walked around in the maze, but with how big the maze is, that just wasn't feasible. A possible solution to this problem would be to use a VR treadmill (like Omni One or Kat Walk), but this equipment is quite expensive and not available for the average VR user. Moving around in the game while standing still in real life can create motion sickness, so to take this into account, we kept the walking and turning speed quite low.

## Wrapping up
It has been amazing working on the projects in this course, and I believe working on the projects hands-on like we've done in this course is a very effective way of learning. I hope I was able to convey some of the things I've learned in this post. That's it for me.