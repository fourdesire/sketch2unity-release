using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Newtonsoft.Json.Linq;

namespace FD.Sketch2Unity {

	public class FDUIGeneratorEditor : FDUIGeneratorBase {

		public FDUIGeneratorEditor (JObject data, string path) {
			m_Data = data;
			string fileName = data.StringValue ("name");
			m_Path = path.Replace (fileName + ".json", "");
		}

		public static GameObject Create (JObject data, string path) {
			return new FDUIGeneratorEditor (data, path).Build (null);
		}

		public override void BuildChildren (GameObject root, JArray children) {
			foreach (JObject obj in children) {
				new FDUIGeneratorEditor (obj, m_Path).Build (root);
			}
		}

		public override void LoadImage (Image image, string imagePath) {
			image.sprite = AssetDatabase.LoadAssetAtPath<Sprite> (imagePath);
		}


	}
}
