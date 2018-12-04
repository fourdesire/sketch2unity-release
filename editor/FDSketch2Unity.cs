using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FD.Sketch2Unity {

	public class FDSketch2Unity : MonoBehaviour {
		
		// static readonly string IMPORTER_VERSION = "1.0.0";
		private static string fontPath;
		public static string[] fonts;
		public static Dictionary<string, string> fontDict = new Dictionary<string, string> ();

		[MenuItem ("Assets/Create/Sketch2Unity/Import")]
		static void Init () {

			fontPath = Application.dataPath + "/Resources/";
			string[] ttfFonts = Directory.GetFiles (fontPath, "*.ttf");
			string[] otfFonts = Directory.GetFiles (fontPath, "*.otf");

			fonts = ttfFonts.Concat (otfFonts).ToArray ();
			for (int i = 0; i < fonts.Length; i++) {
				fonts [i] = fonts [i].Replace (fontPath, "").Split ('.').First ();
			}

			var objects = CurrentObjects();

			foreach (var obj in objects) {
				TextAsset textAsset = obj as TextAsset;

				if (textAsset != null) {
					try {
						JObject data = JObject.Parse (textAsset.text);
						string type = data.StringValue ("content_type");

						if (type == null) {
							return;
						}

						if (type == "sketch2unity") {
							// TODO: Handle version in the future
							// var version = data.StringValue ("version");
							string path = AssetDatabase.GetAssetPath (textAsset);
							FDUIGeneratorEditor.Create (data, path);
						}
					} 
					catch (JsonReaderException ex) {
						Debug.Log ("--S2U catch JsonReaderException: " + ex.Message);
						continue;
					}
				}
			}
		}

		static Object [] CurrentObjects() {
			return Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
		}

	}
}
