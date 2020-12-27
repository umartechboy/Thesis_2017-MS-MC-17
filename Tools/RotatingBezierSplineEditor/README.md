![GregorAppIcon](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/AppIcon.jpg)
# Gregor -- A Minimalist Rotating Spline Editor

Gregor is a simple Rotating Spline editor and machine data generation tool. These are the primary features of Gregor.
 - Create and Edit Rotating Bezier Splines
 - Import and rescale background images for manual tracing
 - Multiple splines on the same sheet
 - Change thickness and appearnce of the rotating splines
 - Toggle the visibility of spline handles
 - Change viewing modes
 - Save, open and import worksheets

## Screenshot
![ScreenShotV2](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/ScreenShotV2.JPG)
## Downloads
### How to Build from the Source?

 1. In order to rebuild the tool, you need Microsoft Visual Studio (VS) with [.Net Framework 4.5.2](https://www.microsoft.com/en-pk/download/details.aspx?id=42642) installed on the machine to build and use this tool. A free of cost community edition of VS can be downloaded from [the official VS download page](.https://visualstudio.microsoft.com/downloads/)
 2. Clone this repository or download this directroy from [This Link](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/umartechboy/Thesis_2017-MS-MC-17/tree/main/Tools/RotatingBezierSplineEditor)
 3. Open the solution (RotatingBezierSplineEditor.sln) and build by hitting F5 or selecting "Build All" from the main menu.
 ### Direct Download
All the releases can be viewed and downloaded from [this directory](https://github.com/umartechboy/Thesis_2017-MS-MC-17/tree/main/Tools/RotatingBezierSplineEditor/Builds) whereas the latest build is always downloadable from [this link](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Builds/Latest.rar).

# Usage
## Understanding the interface
![Interface](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/Interface.JPG)
### Handle Visibility Switches
The visitbility of all of the handles can be changed either from the menu on the left or the Main Menu > View. Each handle also has a keyboard shortcut associated with it. See Keyboar Shortcuts section [Here](#Keyboard%20Shortcuts).
### Drawing Mode
To imporve visibility while editing, the rotating spline or the regular spline can be turned on or off using the menu on the left or the Main Menu > View.
### Keyboard Shortcuts
 - Ctrl + 1: Toggle the visibility of center vertices
 - Ctrl + 2: Toggle the visibility of curvature handles
 - Ctrl + 3: Toggle the visibility of rotation handles
 - Ctrl + 4: Combined drawing mode
 - Ctrl + 5: Ink only mode
 - Ctrl + 6: Anchors and splines only mode
 - Ctrl + O: Open an existing spline file
 - Ctrl + S: Save the existing sheet
 - Ctrl + I: Import a spline file into the existing sheet
 - Shift + Delete: Clear the sheet
 - Ctrl + G: Toggle the visibility of grid
 - Ctrl + 3: Toggle the visibility of rotation handles
 - Ctrl + Shift + B: Toggle the visibility of background images
 - Ctrl + V: Toggle the behavior of left mouse button between editing and panning
 - F1: Shows the home page of this repository
 - F2: Shows quick tips
### Main Menu
**- File: File related operations**
-- File > Open: Opens a new spline sheet, replacing the contents of the existing sheet
-- File > Save: Opens up a file saving dialog to save the existing contents of the sheet or saves to the previously selected file.
-- File > Import > Spline: Opens a new spline sheet and appends the its contents to 
the existing sheet
-- File > Import > Image: Imports an image file (*.bmp, *.jpg or *.png) to be used as the background and places it at 0, 0.
-- File > Clear All

**- View: Operations related to viewing and drawing**
-- Spline Display
---  View > Combined Mode (Default)
--- View > Ink Mode
--- View > Spline Only

--  View > Grid: Toggles the visibility of the grid
-- View > Scale: Toggles the visibility of the x and y scale bars
-- View > x-y Axis: Toggles the visibility of the x and y axis lines
-- View > Background Images: Toggles the visibility of the background images
-- View > Add Anchors with Left Click (Default): Toggle the behavior of left mouse button between editing and panning

**- Edit**: Operations related to spline editing
-- Handles
 -- Center Points (Checked by default): Toggle the visibility of center vertices
 -- Curvature Handles (Checked by default): Toggle the visibility of curvature handles
 -- Rotation Handles: Toggle the visibility of rotation handles
-- Edit > Add Anchors with Left Click (Default): Toggle the behavior of left mouse button between editing and panning
## Navigation
The application uses a cursor-only interface for editing the splines and navigation within the sheet. Pressing and holding the Right-Mouse button on an empty space zooms in and out of the scene about the cursor position. As soon as the right mouse button is pressed, a circle shows up, located at the center of the screen and passing through the current cursor position. Moving the cursor out of the circle zooms out and moving it inside the circle zooms in the view. ![Zooming](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/zooming.png =400x)

Usually, one can pan the sheet using a zoom-out and zoom-in operations, but this requires some practice. Alternatively one can toggle to behaviour of left mouse button to pan the sheet, Press Ctrl + V at any time or choose the option for Main Menu > View to change this behaviour. Pressing Ctrl +V for a second time will toggle the left mouse button behaviour back to editing. Once in panning mode, clicking and dragging an empty area of the sheet with the left mouse button will pan it. ![Zooming](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/panning.png =400x)

## Add a new spline
By default, the left mouse button can be used to add more splines and appending anchor to existing splines. First, confirm that the left mouse button is set to "Add Anchors with Left Click" from Main Menu > View or Main Menu > Edit. To improve visibility, you may want to turn extra items off. For instance, the rotation handles, and inkmarks. To do this, use the short keys Ctrl + 3 and Ctrl + 4. Once the modes are set, right click on an empty space of the sheet to unselect any selected anchor points and then click somewhere to begin creating a new spline. Once a spline has begun, clicking somewhere else on the screen will add new anchors to the newly created spline. While adding new anchors, instead of clicking, pressing and dragging the cursor will add an anchor and then move the newly created curvature handle as well. 
## Removing anchors and splines
To undo an added anchor point, right-click the respective center point when highlighted. To undo and entire spline, right-click anywhere on it other than the handles when highlighted.
## Appending to existing spline
First, confirm that the left mouse button is set to "Add Anchors with Left Click" from Main Menu > View or Main Menu > Edit. Select the first or the last anchor of a spline using the left-clicking the respective center point. Once selected, the newly added anchors are appended to the selected side of the spline.
## Adding Inkmarks
Confirm that the ink-marks are turned on using Ctrl + 1 or Ctrl + 2 or Main Menu > View > Spline Display. Now select a spline using the left mouse button. A spline appearance editing tool will pop up. ![SplineAppearanceEditing](https://raw.githubusercontent.com/umartechboy/Thesis_2017-MS-MC-17/main/Tools/RotatingBezierSplineEditor/Images/splineappearancetool.JPG)
Use the track bar to give the spline some thickness. You may also give it a color of your choice. Close the tool and turn on the rotation handles using Ctrl + 3. You may also want to turn of the center points and curvature handles using Ctrl + 1 and Ctrl + 2 respectively for better visibility. Now draw a rotation handle to change the rotation at the respective anchor. Making the handle move in circles integrates the angle allowing to give the anchor angles outside the 360 degrees range.
## Background Images
Background images can be very useful while creating and editing splines. First, turn on the visibility of background images from Main Menu > View. You may import an imge from Main Menu > File > Import > Image. Once imported. the image can be repositioned by draggin the center point handle and rezised using the handle displayed at the cornor of the imported image.
## Saving and Opening Files
Gregor uses Microsoft XML files to save the data. Use Main Menu > File > Open, Save and Import > Spline to use save files. Please note that while saving a sheet with a background image, the image data is stored as a hexadecimal string within the save file and will load bac up when opening an existing file.
