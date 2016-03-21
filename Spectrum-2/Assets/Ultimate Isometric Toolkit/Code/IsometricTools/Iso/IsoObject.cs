using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITYEDITOR
using UnityEditor;
#endif

/// <summary>
/// Base class that makes a GameObject an IsometricObject.
/// Everything in your scene that has a Transform component attached and is part of your isometric game, even empty gameObjects, should have this component attached.
/// </summary>
[ExecuteInEditMode]
public class IsoObject : MonoBehaviour
{

	/// <summary>
	/// projectionmatrix to project from cartesian to isometric worldspace
	/// Note: Use projectionMatrix.inverse to invert the projection.
	/// </summary>
	public static readonly Matrix4x4 projectionMatrix = Matrix4x4.identity;

	/// <summary>
	/// Angle relative to the sprites you use.
	/// YOU MAY CHANGE THIS VALUE TO YOUR NEEDS 
	/// </summary>
	public static float angle = 26.5f;

	/// <summary>
	/// Distance between sprites in 3rd dimension. 
	/// You may change this value, but keep it >= .1f
	/// </summary>
	public static float depthScaling = 1f;

	/// <summary>
	/// Initializes the correct projection matrix 
	/// </summary>
	static IsoObject()
	{
		var angleInRad = Mathf.Deg2Rad * angle;
		projectionMatrix.m00 = Mathf.Cos(angleInRad);
		projectionMatrix.m01 = -Mathf.Cos(angleInRad);
		projectionMatrix.m02 = 0;
		projectionMatrix.m10 = Mathf.Sin(angleInRad);
		projectionMatrix.m11 = Mathf.Sin(angleInRad);
		projectionMatrix.m12 = 1;
		projectionMatrix.m20 = depthScaling;
		projectionMatrix.m21 = depthScaling;
		projectionMatrix.m22 = -depthScaling;
	}


	/// <summary>
	///  flag to display bounds in the editor
	/// </summary>
	public bool displayBounds = true;

	[SerializeField]
	[HideInInspector]
	private Vector3 size = Vector3.one;

	/// <summary>
	/// Size of the IsometricObject.
	/// </summary>
	public Vector3 Size
	{
		get
		{
			return size;
		}
		set
		{
			size = value;
		}
	}

	/// <summary>
	/// Position of the IsometricObject.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			return projectionMatrix.inverse.MultiplyPoint(transform.position);
		}
		set
		{
			transform.position = projectionMatrix.MultiplyPoint(value);
		}
	}

	/// <summary>
	/// Depth of the IsometricObject. 
	/// Note: There exists a total order in the depths of isometric objects 
	/// </summary>
	public float Depth
	{
		get
		{
			return transform.position.z;
		}
		private set
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, value);
		}
	}
}
