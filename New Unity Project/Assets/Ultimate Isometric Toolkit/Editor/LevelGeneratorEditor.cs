using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		var lg = target as LevelGenerator;
		if(GUILayout.Button("Init map")) {
			lg.instantiate();
		}

		if (lg.size.magnitude > 0 && lg.map != null && GUILayout.Button("Clear map"))
		{
			lg.clear();
		}
	}

}
