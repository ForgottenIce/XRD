# AR Dev Blog Post 3
**Authors:** Simon Lassen

## Creating the 3D model of the VIA Parking Lot
After receiving an illustration with exact measurements of the VIA parking lot (explained in [AR Dev Blog Post 2](../AR%20Dev%20Blog%20Post%202/README.md)), we were ready to start modelling.

The illustration created by "kort- og landmålingsuddannelsen" was in vector graphics format.
After some file conversion, it was possible to import these vectors into Blender.
We used a plugin called MeasureIt in Blender, which made it possible to measure distances between vertices. The 50 meter scale provided on the parking lot illustration was scaled to match 50 meters in Blender.

![Using MeasureIt to get the correct scale](media/MeasureIT-scale.png)

From there, it was possible to model the parking lot plane in the correct scale.

![Parking lot plane in correct scale](media/parking-lot-plane.png)

Every single parking spot had to be modelled into our 3D model. This process was quite tedious, but the end result looked like this:

![Parking spots on the 3D model](media/parking-spots.png)

The last thing needed on the 3D model was the calibration sign that the Unity application will use to figure out where to place the 3D model when overlaying it on top of the real world. We measured the sign ourselves and placed it accordingly in the 3D model:

![The calibration sign in our 3D model](media/calibration-sign.png)

And with that, our 3D model was finished. The model was imported into Unity to finish up our AR application.