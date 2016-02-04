using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITYEDITOR
using UnityEditor;
#endif 

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class IsoObject : MonoBehaviour {

	public static readonly Matrix4x4 projectionMatrix = Matrix4x4.identity;

	// you may vary this, but keep > 0.1f
	public const float depthScaling = 1f ;

	static IsoObject() {

		//Isometric projection matrix
		projectionMatrix.m00 = 1;
		projectionMatrix.m01 = -1;
		projectionMatrix.m02 = 0;
		projectionMatrix.m10 = 1/2f;
		projectionMatrix.m11 = 1/2f;
		projectionMatrix.m12 = 1;
		projectionMatrix.m20 = depthScaling;
		projectionMatrix.m21 = depthScaling;
		projectionMatrix.m22 = -depthScaling;

	}

	//Position in virtual 3d space, not the Isometric projection (which is a Vector2 and stored in transform.position)
	[SerializeField]
	private Vector3 position;
	
	[SerializeField]
	private Vector3 size = new Vector3(1, 1, 1);

	//flag to display bounds in the editor
	public bool displayBounds = true;

	//Size property
	public Vector3 Size {
		get { return size; }
		set { 
			size = value;

		}
	}
	
	//the position of the object, not the isometric projection
	public Vector3 Position {
		get { return position; }
		set {
			position = value;
			updateIsoProjection();
		}
	}
	
	/// <summary>
	/// current depth/sortingOrder.
	/// </summary>
	public float Depth {
		get {
			return transform.position.z;
		}
		private set { 
			transform.position = new Vector3(transform.position.x, transform.position.y, value);
		}
	}

	void Start() {
		updateIsoProjection();
	}
	
	/// <summary>
	/// updates the isometric projection of this isoObject
	/// </summary>
	protected void updateIsoProjection() {
		transform.position = isoProjection(Position);
	}

	/// <summary>
	/// Isometric Projection of an object in 3d Space.
	/// </summary>
	/// <param name="pt"></param> The Object in virtual 3d space
	/// <returns></returns>
	private Vector3 isoProjection(Vector3 pt) {
		return projectionMatrix.MultiplyVector(pt);
	}

}
