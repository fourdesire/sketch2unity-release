using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public static class FDJsonHelper {

	const string SUCCESS = "success";

	public static int IntValue (this JObject jobj, string key, int defaultValue = 0) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null)
			return defaultValue;

		int val;
		if (int.TryParse (v.ToString (), out val)) {

			return val;

		} else {

			return defaultValue;
		}
	}

	public static bool BoolValue (this JObject jobj, string key, bool defaultValue = false) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null)
			return defaultValue;

		bool val;
		if (bool.TryParse (v.ToString (), out val)) {

			return val;

		} else {

			return defaultValue;
		}
	}

	public static long LongValue (this JObject jobj, string key, long defaultValue = 0) {

		if (jobj == null) {
			return defaultValue;
		}

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null) {
			return defaultValue;
		}
		long val;
		if (long.TryParse (v.ToString (), out val)) {

			return val;

		} else {
			return defaultValue;
		}
	}

	public static float FloatValue (this JObject jobj, string key, float defaultValue = 0f) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null)
			return defaultValue;

		float val;
		if (float.TryParse (v.ToString (), out val)) {

			return val;

		} else {

			return defaultValue;
		}
	}

	public static double DoubleValue (this JObject jobj, string key, double defaultValue = 0.0) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null)
			return defaultValue;

		double val;
		if (double.TryParse (v.ToString (), out val)) {

			return val;

		} else {

			return defaultValue;
		}
	}

	public static string StringValue (this JObject jobj, string key, string defaultValue = null) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v == null || v.Type == JTokenType.Null)
			return defaultValue;

		return v.ToString ();
	}

	public static Vector2 Vector2Value (this JObject jobj, string key) {

		var vecJson = jobj.JObjectValue (key);

		return new Vector2 (vecJson.FloatValue ("x"), vecJson.FloatValue ("y"));
	}

	public static JArray JArrayValue (this JObject jobj, string key) {

		if (jobj == null)
			return new JArray ();

		var v = jobj [key];
		if (v != null && v.Type == JTokenType.Array)
			return v as JArray;

		return new JArray ();
	}

	public static JObject JObjectValue (this JObject jobj, string key) {

		if (jobj == null)
			return new JObject ();

		var v = jobj [key];
		if (v != null && v.Type == JTokenType.Object)
			return v as JObject;

		return new JObject ();
	}

	public static JObject JObjectValue (this JObject jobj, string key, JObject defaultValue) {

		if (jobj == null)
			return defaultValue;

		var v = jobj [key];
		if (v != null && v.Type == JTokenType.Object)
			return v as JObject;

		return defaultValue;
	}

	public static bool Success (this JObject jobj) {

		return jobj.BoolValue (SUCCESS, false);
	}

	public static bool ContainsKey (this JObject jobj, string key) {

		if (jobj == null)
			return false;

		var v = jobj [key];
		return v != null && v.Type != JTokenType.Null;
	}
}
