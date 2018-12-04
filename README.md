# FDSketch2unity

This module consists of two parts: **sketch2json** and **json2unity**.

- **sketch2json** is a sketch plugin that generates json files and images in your Sketch file, whcih supports Sketch up to version 52.3 (and Custom panel supports Sketch after version 43).

- **json2unity** then combines these files into Unity UI!


## sketch2json

### Usage
[For Designers]
1. Download **sketch2unity-xcode.sketchplugin** from releases
2. Double click **sketch2unity-xcode.sketchplugin** to install the plugin or 
Move the package **sketch2unity-xcode.sketchplugin** to the folder `~/Library/Application Support/com.bohemiancoding.sketch3/Plugins/`
3. Set layout values for the group and symbol in custom panel in inspector
4. Arrange layers accordong to the Sketch Convention below
5. Select the artboard or the group you want to export
6. Use shortcut `ctrl + shift + x` or Choose `Plugins > sketch2unity-xcode > export` to export and preview
7. Load file by the copied path
8. Use mouse or plus and minus key to zoom in and zoom out; use mouse drag or w, s, a, d key to move the UI
9. If necessary, put fonts(.ttf, .otf) used in UI in a folder named Resources, and move the folder to `~/Library/Application Support/com.bohemiancoding.sketch3/Plugins/sketch2unity-xcode.sketchplugin/Contents/Sketch/FDPreview/`

### Sketch Convention
1. Arrange layers sharing the same layout properties in a group in order to maintain the hierachy of layout
2. Arrange each text layer and image layer in a group respectively in order to fit the object structure of Unity (The plugin store position information for every group)
3. Setting layout values only works for group and symbol (e.g, Setting layout values for text layers and bitmap layers is useless)
4. Add slice to the group of image (1) with transparent bound, or (2) composed of multiple subimages. Also, all names of image layers may not be the same
5. Invisible layers will not be exported
6. View combined shape as a single image layer
7. Set background color by adding a filled shape layer instead of setting background color to artboard
8. Avoid using thickened line to generate shape like rectangle 

---
## json2unity

### Usage
[For Engineers]
1. Download **Editor folder** containing FDSketch2Unity.cs, FDUIGeneratorBase.cs, FDUIGeneratorEditor.cs and FDJsonHelper
2. Add **Editor** to your Unity project and Compile
3. Import exported json files and images folder to Assets folder in Unity
4. If necessary, put fonts(.ttf, .otf) used in UI in a folder named Resources, and move the folder to **Assets folder**
5. Right click on the exported json files and Choose `Create > Sketch2Unity > Import` to generate UI.
