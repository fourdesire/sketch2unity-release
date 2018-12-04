using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FD.Sketch2Unity { 
	
	public class FDUIGeneratorBase {
		
		public JObject m_Data;
		public string m_Path;

		public FDUIGeneratorBase () {}


		public GameObject Build (GameObject root) {

			string name = m_Data.StringValue ("name");
			JObject layout = m_Data.JObjectValue ("layout");
			Vector2 maxAnchor = layout.Vector2Value ("max_anchor");
			Vector2 minAnchor = layout.Vector2Value ("min_anchor");
			Vector2 pivot = layout.Vector2Value ("pivot");

			JArray jvalues = layout.JArrayValue ("values");
			Vector4 values = new Vector4 ();
			JToken itr = jvalues.First;
			for (int i = 0; itr != null && i < 4; i++) {
				values [i] = itr.ToObject<float> ();
				itr = itr.Next;
			}

			int rotation_z = layout.IntValue ("rotation_z");
			JArray jcomponents = m_Data.JArrayValue ("components");
			JArray jchildren = m_Data.JArrayValue ("children");

			var subroot = new GameObject (name);
			if (root != null) {
				subroot.transform.SetParent (root.transform);
				subroot.transform.localPosition = new Vector3 (values [0], -values [1], 0);
			}
			else {
				subroot.transform.position = new Vector3 (0, 40, 0);
			}
			subroot.transform.Rotate (Vector3.forward * rotation_z);

			RectTransform rectTransform = subroot.AddComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2 (values [2], values [3]);
			rectTransform.anchorMax = maxAnchor;
			rectTransform.anchorMin = minAnchor;
			rectTransform.pivot = pivot;

			if (root == null) {
				Canvas canvas = subroot.AddComponent<Canvas> ();
				canvas.renderMode = RenderMode.WorldSpace;
			}

			if (jcomponents.Count > 0) {
				BuildComponents (subroot, jcomponents);
			}
			if (jchildren.Count > 0) {
				BuildChildren (subroot, jchildren);
			}
			return subroot;
		}

		public virtual void BuildChildren (GameObject root, JArray children) {}

		public void BuildComponents (GameObject root, JArray components) {

			foreach (JObject obj in components) {
				string objType = obj.StringValue ("type");

				if (objType == "text") {
					string name = obj.StringValue ("text");
					string fontName = obj.StringValue ("font_name");
					int fontSize = (int) obj.DoubleValue ("font_size");
					int AutoFit = (int) obj.DoubleValue ("auto_fit_to");
					string color = obj.StringValue ("color");
					string align = obj.StringValue ("alignment");

					Text text = root.AddComponent<Text> ();
					text.text = name;
					text.fontSize = fontSize;

					Font font = Resources.Load (fontName) as Font;
					if (font == null) {
						string fontFile = FindFont (fontName);
						font = Resources.Load (fontFile) as Font;
						text.rectTransform.sizeDelta = new Vector2 (text.rectTransform.sizeDelta.x + 3.0f, text.rectTransform.sizeDelta.y + 3.0f);
					}
					text.font = font;

					if (AutoFit > 0) {
						text.resizeTextForBestFit = true;
						text.resizeTextMinSize = AutoFit;
					}
					text.alignment = alignString2anchor (align);
					text.color = colorString2rgba (color);
					text.verticalOverflow = VerticalWrapMode.Overflow;
				} 

				else if (objType == "image") {
					string name = obj.StringValue ("image");
					//string color = obj.StringValue ("color");
					string imagePath = m_Path + "images/" + name + ".png";

					Image image = root.AddComponent<Image> ();
					//image.color = colorString2rgba (color);
					LoadImage (image, imagePath);
				}
			}
		}

		public virtual void LoadImage (Image image, string imagePath) {}

		public string FindFont (string key) {

			string defaultFontFile = "Arial Unicode";
			string[] fonts = FDSketch2Unity.fonts;

			if (fonts == null || fonts.Length == 0) {
				return defaultFontFile;
			}
			if (FDSketch2Unity.fontDict.ContainsKey (key)) {
				return FDSketch2Unity.fontDict [key];
			}

			string comparer = key.Replace ("-", " ");
			int maxCount = 0;
			string max = "";

			for (int i = 0; i < fonts.Length; i++) {

				string comparee = fonts[i];

				string head = comparee.Split (' ').First ();
				if (head != comparer.Substring (0, head.Length < comparer.Length ? head.Length : comparer.Length)) {
					continue;
				}

				List<char> list1 = string2list (comparer);
				List<char> list2 = string2list (comparee);
				int common = list1.Intersect (list2).Count();

				if (i > 0) {
					if (common >= maxCount) {
						maxCount = common;
						max = comparee;
					}
				} else {
					maxCount = common;
					max = comparee;
				}
			}

			string maxHead = max.Split (' ').First ();
			if (maxCount <= 0 || maxHead != comparer.Substring (0, maxHead.Length)) {
				max = defaultFontFile;
			}
			FDSketch2Unity.fontDict.Add (key, max);
			return max;
		}
			
		private static List<char> string2list (string str) {
			List<char> list = new List<char> ();
			for (int i = 0; i < str.Length; i++) {
				list.Add (str [i]);
			}
			return list;
		}

		private static TextAnchor alignString2anchor (string align) {
			if (align == "UpperLeft") return TextAnchor.UpperLeft;
			else if (align == "UpperRight") return TextAnchor.UpperRight;
			else if (align == "UpperCenter") return TextAnchor.UpperCenter;

			else if (align == "MiddleLeft") return TextAnchor.MiddleLeft;
			else if (align == "MiddleRight") return TextAnchor.MiddleRight;
			else if (align == "MiddleCenter") return TextAnchor.MiddleCenter;

			else if (align == "LowerLeft") return TextAnchor.LowerLeft;
			else if (align == "LowerRight") return TextAnchor.LowerRight;
			else return TextAnchor.LowerCenter;
		}

		private static Color colorString2rgba (string color) {

			string hex = color.Replace ("#", "");
			byte a = 1;  // assume fully visible unless specified in hex
			byte rb = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
			byte gb = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
			byte bb = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);

			if(hex.Length == 8) {  // only use alpha if the string has enough characters
				a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
			}
			float r = (float)rb / 255;
			float g = (float)gb / 255;
			float b = (float)bb / 255;

			return new Color(r,g,b,a);
		}
			
	}
}
