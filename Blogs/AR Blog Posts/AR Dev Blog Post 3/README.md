# AR Dev Blog Post 3
**Authors:** Simon Lassen

## Finishing up the preparation for the 3D model
In the first dev blog post, made the preparation of the 3D model by scanning the VIA Parking lot with a 3D scanner app.
With that done, it was time to get started on the actual 3D model that should be used within our Unity app.

At first, the idea was to just import all the scans we had made, measure one of the sides of the parking lot in real life and then scale the 3D scans to match our measurement.
We however quickly realized that the 3D scans themselves actually weren't very accurate.
When overlaying a picture of the parking lot from Google maps on top of our 3D models, it appeared that our scans were curved.

![Illustration of the 3D scans being curved]()

At that point, we realized the 3D scans couldn't really help too much as they simply lacked accuracy. Now the our approach would be to use the Google maps image, measure one side of the parking lot, scale that Google maps picture to match that size and then build the model from that. We asked "kort- og landmålingsuddannelsen" if we could borrow some measuring equipment to measure the one side of the parking lot. While that wasn't possible, |insert name| was intrigued by our project. So much that he offered to ask some student from his class to measure the entire parking lot for us. The end result was that we received a PDF from him with the exact measurements of the parking lot. Here's what that looked like:

![Picture of measurement performed by kort- og landmålingsuddannelsen]()

## Creating the 3D model in Blender
The measurements provided by kort- og landmålingsuddannelsen made the creation of the 3D model a lot simpler.
Their drawing of the parking lot was in vector graphics format. After some file conversion, it was possible to import these vectors into Blender.

In Blender, we used the plugin MeasureIt that made it possible to measure distances between vertices. The 50 meter scale provided on the parking lot drawing was scaled to match 50 meters in Blender. From there, 