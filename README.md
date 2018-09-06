# sketch2unity-release

The sketch2unity module consists of two parts: sketch2json and json2unity and this release is sketch2json part.

## sketch2json

### Usage
1. Download 'sketch2unity-xcode.sketchplugin' from releases
2. Double click 'sketch2unity-xcode.sketchplugin' to install the plugin or 
Move the package 'sketch2unity-xcode.sketchplugin' to the folder `~/Library/Application Support/com.bohemiancoding.sketch3/Plugins/`
3. Set layout values for the group and symbol in custom panel in inspector view
4. Select the artboard group you want to export
5. Use shortcut `ctrl + shift + x` or Choose `Plugins > sketch2unity-xcode > export` to export.

### Reminder
1. All names of image layers may not be the same
2. Arrange layers sharing the same layout properties in a group (in order to maintain the hierachy of layout)
3. Arrange each text layer, bitmap(image) layer and shape layer in a group respectively (in order to fit the object structure of unity)
4. Setting layout values only works for group layer and symbol layer (e.g, Setting layout values for text layers and bitmap layers is useless.)
5. Invisible layers will not be exported
6. Set background color by adding a filled shape layer instead of setting background color to artboard
7. Avoid using thickened line to generate shape like rectangle
8. Set masks as invisible.

---
## json2unity

### Usage
1. Compile and Run FDSketch2Unity
2. Import exported json files and images folder (in sketch2unity folder) to Assets folder in Unity
3. Right click on the exported json files and Choose `Create > Sketch2Unity > Import` to generate UI.
