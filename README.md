# Unity-Physics

Simulation for boids, and cloth using custom code. Uses the Unity game engine to render.

[Cloth Simulation](https://bennybroseph.github.io/Unity-Physics/Cloth/index.html)

[Boids](https://bennybroseph.github.io/Unity-Physics/Boids/index.html)

## Cloth Simulation Screenshots
Below are some screenshots taken from the build of the Cloth Simulation application showing off some of the features implemented.
### Wireframe View
The white circles seen in the screenshot are particles. A blue color indicates that the particle is currently pinned meaning it is not currently affected by physics. The lines ranging from greed to red are spring dampers. The more red the spring is the more stretched out it is. If the spring damper is stretched past a certain point indicated by it becoming fully red, the spring will snap. This releases the particle from the pull of that particular spring damper.
![Alt text](Screenshots/WireframePinned.png?raw=true)
### W/Rendererd Triangles
This shot show a similar one from above but with the triangles rendered from each spring damper. A triangle is formed by three particles and three spring dampers. These particles' positions affect the spring dampers which in turn affect the triangles. For this simulation, the triangles are used to calculate aerodynamics. They also of course make it look more like cloth and less like wires when rendered out to the user.
![Alt text](Screenshots/TrianglesPinned.png?raw=true)
### Only Triangles Rendered
Here is a screenshot of only the triangles being rendered. Notic that they are affected by lighting. Some look darker than others. The triangles are using a modified diffuse calculation based on a light source in the Unity Scene so that they appear to have more depth. I've used UnityEngine.GL commands to achieve this custom effect.
![Alt text](Screenshots/OnlyTriangles.png?raw=true)
### Stretched Springs
This shot shows what happens when a spring is pushed close to the limit. Notice the color of each spring. 
![Alt text](Screenshots/StretchedOut.png?raw=true)
### Tearing
Below is an example of what happens when a spring is pushed past its limit. The spring breaks, and any triangles associated with that spring are ripped as well. This means that wind no longer affects those particles in those directions. It's also fun to rip the cloth up and see how it reacts. Pinning helps when trying to pull pieces apart from each other to create more precise tears.
![Alt text](Screenshots/RippedCloth.png?raw=true)
