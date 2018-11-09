
var onStartup = function(context) {
  var frameworkPath = frameworkPath || COScript.currentCOScript().env().scriptURL.path().stringByDeletingLastPathComponent().stringByDeletingLastPathComponent();
  (function() {
    var mocha = Mocha.sharedRuntime();
    var frameworkName = "sketch2unity";
    var directory = frameworkPath;
    if (mocha.valueForKey(frameworkName)) {
      log("loadFramework: `" + frameworkName + "` already loaded.");
      return true;
    } else if ([mocha loadFrameworkWithName:frameworkName inDirectory:directory]) {
      log("✅ loadFramework: `" + frameworkName + "` success!: " + directory);
      mocha.setValue_forKey_(true, frameworkName);
      COScript.currentCOScript().shouldKeepAround = true;  // Keep something asynchronous: tell Sketch to keep it around and not to garbage collect it
      return true;
    } else {
      log("❌ loadFramework: `" + frameworkName + "` failed!: " + directory + ". Please define sketch2unity_FrameworkPath if you're trying to @import in a custom plugin");
      return false;
    }
  })();
};

var onSelectionChanged = function(context) {
    //sketch2unity.onSelectionChanged(context);
};


