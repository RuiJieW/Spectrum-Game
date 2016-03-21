using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class Reminder : EditorWindow {

	[MenuItem("Tools/IsoTools/Reminder")]
	public static void ShowWindow()
	{
		GetWindow<Reminder>("Reminder").ShowTab();
	}

	void OnGUI()
	{
		EditorGUILayout.TextArea("Thanks for purchasing my asset.\n \n The success of this asset depends on you as well!\n \n Leave a rating in the store and/or subscribe \n to my youtube channel to never miss any updates.");
		if (GUILayout.Button(new GUIContent("I Support!", "Open browser")))
		{
			Help.BrowseURL("https://www.youtube.com/subscription_center?add_user=youtilities");
			Help.BrowseURL("https://www.assetstore.unity3d.com/en/#!/content/33032");

			EditorPrefs.SetBool("reminded", true);
			EditorPrefs.SetString("lastReminded", DateTime.Now.ToString("hh.mm.ss"));
		}
	}
}
