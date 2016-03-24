using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

//custom editor for all IsoObjects and its subclasses
[CustomEditor(typeof(IsoObject), true), CanEditMultipleObjects]
class IsoObjectEditor : Editor
{

	public static Vector3 center;
	public static Vector3 delta;

	public static bool centerNeedsUpdate = false;


	public static IsoObject[] lastTargets;

	/// <summary>
	/// Override to display custom handles on the scene view
	/// </summary>
	void OnSceneGUI()
	{
		//calc the custom handle position in sceneview
		var isoHandlePos = Isometric.toIsoProjection(center);
		var t = (IsoObject)target;


		if (GUI.changed)
		{
			centerNeedsUpdate = true;
		}
		var x = 0f;
		var y = 0f;
		var z = 0f;
		//Get deltas for (north, up, west)
		try
		{
			x = handleX(isoHandlePos);
			y = handleY(isoHandlePos);
			z = handleZ(isoHandlePos);
		}
		catch (ArithmeticException)
		{

		}


		//snaps the target to closest multiple
		if (IsoSnapping.doSnap)
		{
			Vector3 vec = IsoSnapping.Ceil(new Vector3(x, y, z));
			x = vec.x;
			y = vec.y;
			z = vec.z;
		}

		if (Event.current.shift)
		{

			x *= 0.1f;
			y *= 0.1f;
			z *= 0.1f;
		}

		if (t.displayBounds)
		{
			DrawWireCube(t.Position, t.Size);
		}

		switch (GUI.GetNameOfFocusedControl())
		{
			case "x":
				delta = new Vector3(x, 0, 0);
				break;
			case "y":
				delta = new Vector3(0, y, 0);
				break;
			case "z":
				delta = new Vector3(0, 0, z);
				break;
			default:
				break;
		}
		t.Position += delta;

		EditorUtility.SetDirty(t);

	}

	/// <summary>
	/// Overriding the default Inspector to display propertys and add undo functionalities.
	/// </summary>
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		//casting target
		var isoObject = (IsoObject)target;

		//delta values to if check a change happened
		var position = isoObject.Position;
		var size = isoObject.Size;

		//round values to prevent 10^-x notations in the inspector
		position = new Vector3((float)Math.Round(position.x, 5), (float)Math.Round(position.y, 5), (float)Math.Round(position.z, 5));
		size = new Vector3((float)Math.Round(size.x, 5), (float)Math.Round(size.y, 5), (float)Math.Round(size.z, 5));

		//display position property and make editable
		EditorGUI.BeginChangeCheck();
		position = EditorGUILayout.Vector3Field("Position", position);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(Selection.activeTransform, "Edit IsoPosition");
			isoObject.Position = position;
			EditorUtility.SetDirty(isoObject);
		}

		//display size property and make editable
		EditorGUI.BeginChangeCheck();
		size = EditorGUILayout.Vector3Field("Size", size);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(Selection.activeTransform, "Edit IsoSize");
			isoObject.Size = size;
			EditorUtility.SetDirty(isoObject);
		}

		serializedObject.ApplyModifiedProperties();
		serializedObject.Update();

	}

	/// <summary>
	/// Enable and disable the custom handles
	/// </summary>
	void OnEnable()
	{
		Tools.current = Tool.None;
		EditorApplication.update += new EditorApplication.CallbackFunction(updateCenter);
	}

	IsoObject[] getFromTargets()
	{
		List<IsoObject> objs = new List<IsoObject>();
		foreach (object t in targets)
		{
			var isoobj = (IsoObject)t;

			if (isoobj != null)
			{
				objs.Add(isoobj);
			}
		}
		return objs.ToArray();
	}

	public void updateCenter()
	{
		var currentTargets = getFromTargets();
		if (lastTargets != currentTargets || lastTargets == null)
		{
			centerNeedsUpdate = true;
			lastTargets = currentTargets;
		}
		if (centerNeedsUpdate)
		{
			IsoObject[] isoObjs = currentTargets;
			center = Vector3.zero;
			foreach (IsoObject obj in isoObjs)
			{
				center += obj.Position;
			}
			center = center / (float)isoObjs.Length;
			centerNeedsUpdate = false;
		}
	}


	/// <summary>
	/// creates a handle in z axis. return the delta movement on the isometric z axis.
	/// </summary>
	/// <param name="isoPosition"></param> the isometric position of our object
	/// <returns></returns>
	float handleZ(Vector3 isoPosition)
	{
		GUI.SetNextControlName("z");
		Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.75f);
		var dir = Isometric.vectorToIsoDirection(IsoDirection.Up);
		var delta = Handles.Slider(isoPosition, dir);
		var d = delta - isoPosition;

		var res = d.magnitude * ((Math.Sign(d.y) == Math.Sign(dir.y)) ? 1 : -1);
		return res;
	}


	/// <summary>
	/// creates a handle in forward axis. return the delta movement on the isometric north axis.
	/// </summary>
	/// <param name="isoPosition"></param> the isometric position of our object. Pass the transform.position component
	/// <returns></returns>
	float handleX(Vector3 pos)
	{
		GUI.SetNextControlName("x");
		Handles.color = new Color(0.0f, 0.0f, 1.0f, 0.75f);
		var dir = Isometric.vectorToIsoDirection(IsoDirection.North);
		var delta = Handles.Slider(pos, dir);
		var d = delta - pos;

		var res = d.magnitude * ((Math.Sign(d.x) == Math.Sign(dir.x)) ? 1 : -1);
		return res;
	}

	/// <summary>
	/// creates a handle in left axis. return the delta movement on the isometric wEst axis.
	/// </summary>
	/// <param name="isoPosition"></param> the isometric position of our object. Pass the transform.position component
	/// <returns></returns>
	float handleY(Vector3 pos)
	{
		GUI.SetNextControlName("y");
		Handles.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
		var dir = Isometric.vectorToIsoDirection(IsoDirection.West);
		var delta = Handles.Slider(pos, dir);
		var d = delta - pos;

		var res = d.magnitude * ((Math.Sign(d.x) == Math.Sign(dir.x)) ? 1 : -1);
		return res;
	}

	/// <summary>
	/// Draws a wired cube and performs an isometric projection to it in 2d flat space
	/// </summary>
	/// <param name="position"></param>
	/// <param name="size"></param>
	public static void DrawWireCube(Vector3 position, Vector3 size)
	{
		var half = size / 2;
		// draw front
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, half.y, half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, half.y, half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, half.z)));
		// draw back
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, -half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, -half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, -half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, -half.z)));
		// draw corners
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, -half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, -half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(-half.x, half.y, half.z)));
		Handles.DrawLine(Isometric.toIsoProjection(position + new Vector3(half.x, half.y, -half.z)), Isometric.toIsoProjection(position + new Vector3(half.x, half.y, half.z)));
	}


}