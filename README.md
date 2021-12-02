# Robotic Reconstruction of Islamic Calligraphy
This repo is an appendix to the original MS research thesis under the same title. The main contents of the repo are: 
 - Drogon 2.0 -- An open source robotic simulator, analyser and designer for 6 DOF robots
 - Rotating Bezier Spline Curve Editor -- A minimalistic GUI to demonstrate and create rotating Bezier Spline curves, exporting vector data to be used with the robot simulators and systems supporting the Rotating Bezier Spline Curve targets.

# Data Organisation
* Synopsis -- Contains the presentation and the data used to create it for the synopsis of the thesis
* Thesis -- Latex source code for the final thesis
* Thesis Nov2021 -- We created this thesis branch to be kept to ebbed data that could not be packed in the thesis like user manuals and code organisation
* Gohar Qalam -- The Rorotaing splines files, reference photos and processing results for Gohar Qalam's manuscripts
* Other Data -- Unorganised but relavant data used or referred to in the work
* Tools -- Source code for the developed tools 
* Tools > Drogon3 > Drogon3.sln Visual studio solution for the complete code project
* Tools > RotatingBezierSplineEditor > RotatingBezierSplineEditor.sln Solution file for the bezier spline editor project alone
* Binaries > Executables for Drogon 3 and Gregor. Includes the PhysLogger files required to run as well.
* Videos > Apenddix videos

# What is a Rotating Spline?
Rotating (or twisting) Bezier Spline is a varient of the regular bezier splines that have such that it has a continuously changinging line thickness. Unlike the regular stroke thickness, this thickness can be considered as the ink-mark of a flat tip marker travelling on a bezier spline. I call them "rotating" splines because the spline contains information how the flat marker "rotates" about it's own axis as it moves on the spline.

To understand the working principle of a rotating bezier spline, let us move a step backwards first. The following figure shows a graphic path constructed by joining multiple vertices using straight lines. ![enter image description here](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/polygon.jpg)

Vertices of a polygon only contain information of x and y coordinates where they are located. That is why, the simplest logic to construct a line between any two of them is to join them with a straight line. So, If <img src="https://render.githubusercontent.com/render/math?math=P_M = (x_M, y_M)"> and <img src="https://render.githubusercontent.com/render/math?math=P_N = (x_N, y_N)"> are two
vertices of a polygon, the coordinates of any point <img src="https://render.githubusercontent.com/render/math?math=P(x, y)"> on the line segment between these points will be defined by:
<img src="https://render.githubusercontent.com/render/math?math=x=(1 - f)x_N %2B f x_M"> and <img src="https://render.githubusercontent.com/render/math?math=y_L=(1 - f)y_N %2B f y_M"> where <img src="https://render.githubusercontent.com/render/math?math=f\in[0,1]">. We can also represent this relation with 
<img src="https://render.githubusercontent.com/render/math?math=P(f)=f P_M %2b (1-f) P_N">.

Now, imagine if these vertices had some more information in addition to the x and y coordinates. See the image below that shows a pair of handles beside each center vertex to define the curvature of the spline cell between two polygon vertices. 
![enter image description here](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/spline.jpg)
Each of the curvature handles in a pair is defined by a single point located on a straght line segment such that both of them are located on opposite sides of the center vertex. This follows the the distance of each of these handles from the center vertex is independant but the  position is mutually coupled. Also, since a "Vertex" is conventionally supposed to contian only the position information of a single point, we will this new type of vertex an "Anchor" from now and onwards. Logically, each anchor now contains

 - planar position <img src="https://render.githubusercontent.com/render/math?math=(x,y)"> of a center vertex,
 - scalar lengths of two curvature handles in the range <img src="https://render.githubusercontent.com/render/math?math=\in[0,\infinity]">
 - and a combined orientation of the handles define by an angle.

Each curvature handle contributes to defines the curvature of the spline on each side of the center vertex. The spline is now defined by <img src="https://render.githubusercontent.com/render/math?math=P"> such that 
<img src="https://render.githubusercontent.com/render/math?math=P(f) = f P_{2a}(f) %2b (1-f) P_{2b}(f),"> for <img src="https://render.githubusercontent.com/render/math?math=f\in[0,\infinity]">
where,
<img src="https://render.githubusercontent.com/render/math?math=P_{2a}(f) = f P_{1a}(f) %2b (1-f) P_{1b}(f),">
<img src="https://render.githubusercontent.com/render/math?math=P_{2b}(f) = f P_{1b}(f) %2b (1-f) P_{1c}(f),">
<img src="https://render.githubusercontent.com/render/math?math=P_{1a}(f) = f P_{N}(f) %2b (1-f) H_{N2}(f),">
<img src="https://render.githubusercontent.com/render/math?math=P_{1b}(f) = 
f H_{N2}(f) %2b (1-f) H_{M1}(f),"> 
and 
<img src="https://render.githubusercontent.com/render/math?math=P_{1c}(f) = f H_{M1}(f) %2b (1-f) P_{M}(f),">.

If the length of a curvature handle is greater than zero, the associated spline part is always tangent to the handle where it touches the center point. Using this property, coupling the two handles in a pair makes the spline seemlessly pass through the center vertex, making it possible for the spline to pass through infinite vertexes without ever having to have a sharp edge. However, it can still have a sharp edge if the size of at least a single handle in the anchor is zero.

This is the way most vector graphics processoring and editing software create geometric shapes of all sorts. 

However, in order for a regular spline to become a "rotating/twisting" spline, we add another piece of information to the spline anchor; a "Rotation Handle". This handle does not alter the shape of the spline but describes the orientation of a line segment of a fixed length centered on the spline. As this line segment sweeps the spline from one anchor to the other, it creates a region. One can resemble this sweep with that of a flat tip pen. This is why I call this region "Ink Mark" of the rotating bezier spline. 

![enter image description here](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/RotationInkDescription.jpg)

![enter image description here](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/RotationInkDescription2.jpg)


Since the orientation of the flat tip at each anchor is controlled by the rotation handle at the respective anchor, the shape of the ink-mark also changes just like a real flat-tip marker. The angle of the sweeping line between two anchors at some point <img src="https://render.githubusercontent.com/render/math?math=P(f)"> is controlled by <img src="https://render.githubusercontent.com/render/math?math=\theta(f) = f \angle R_{M} %2b (1-f) \angle R_{N},"> where <img src="https://render.githubusercontent.com/render/math?math=R_N"> and <img src="https://render.githubusercontent.com/render/math?math=R_M"> are the angles defined by the two anchors, <img src="https://render.githubusercontent.com/render/math?math=M"> and <img src="https://render.githubusercontent.com/render/math?math=N">. The value of <img src="https://render.githubusercontent.com/render/math?math=R_M"> and <img src="https://render.githubusercontent.com/render/math?math=R_N"> is not restricted to remain within a range of <img src="https://render.githubusercontent.com/render/math?math=2 \pi">.

These properties makes the rotating/twisting bezier splines a good tool to store  vector information of flat tip ink-marks. Such inkmarks are widely used in many calligraphy cultures, specially Arabic, Urdu and Persian.


