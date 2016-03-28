using UnityEngine;
using System.Collections;
/// <summary>
/// IsoCollider is a custom box collider for isometric objects. It wraps a MeshCollider and updates the sharedMesh when needed.
/// Note: for proper gravity you may call Isometric.projectGravityVector()
/// </summary>
[RequireComponent(typeof(IsoObject))]
[RequireComponent(typeof(MeshCollider))]
[ExecuteInEditMode]
public class IsoCollider : MonoBehaviour {

    private IsoObject isoObj;
	private MeshCollider collider;

	private Vector3 deltaSize;
	
	/// <summary>
	/// Initialized the components
	/// </summary>
	void Awake()
	{
		this.isoObj = GetComponent<IsoObject>();
		this.collider = GetComponent<MeshCollider>();
		collider.sharedMesh = createMesh();
		deltaSize = isoObj.Size;
        Isometric.projectGravityVector();

    }

	/// <summary>
	/// Listens to the IsoObject.Size and applies scaling to the sharedMesh
	/// </summary>
	void Update()
	{
		if (isoObj.Size != deltaSize)
		{
			collider.sharedMesh = createMesh();
			deltaSize = isoObj.Size;
		}
	}

	/// <summary>
	/// Creates a cube in isometric projection relative to its IsoObject.Size.
	/// </summary>
	/// <returns></returns>
	Mesh createMesh()
	{
		Mesh mesh = new Mesh();
		mesh.Clear();

		float length = isoObj.Size.x * 1.1f;
		float width = isoObj.Size.y * 1.1f;
		float height = isoObj.Size.z * 1.1f;

		#region Vertices
		Vector3 p0 = Isometric.toIsoProjection(new Vector3(-length * .5f, -width * .5f, height * .5f));
		Vector3 p1 = Isometric.toIsoProjection(new Vector3(length * .5f, -width * .5f, height * .5f));
		Vector3 p2 = Isometric.toIsoProjection(new Vector3(length * .5f, -width * .5f, -height * .5f));
		Vector3 p3 = Isometric.toIsoProjection(new Vector3(-length * .5f, -width * .5f, -height * .5f));

		Vector3 p4 = Isometric.toIsoProjection(new Vector3(-length * .5f, width * .5f, height * .5f));
		Vector3 p5 = Isometric.toIsoProjection(new Vector3(length * .5f, width * .5f, height * .5f));
		Vector3 p6 = Isometric.toIsoProjection(new Vector3(length * .5f, width * .5f, -height * .5f));
		Vector3 p7 = Isometric.toIsoProjection(new Vector3(-length * .5f, width * .5f, -height * .5f));

		Vector3[] vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,
 
			// Left
			p7, p4, p0, p3,
 
			// Front
			p4, p5, p1, p0,
 
			// Back
			p6, p7, p3, p2,
 
			// Right
			p5, p6, p2, p1,
 
			// Top
			p7, p6, p5, p4
		};
		#endregion

		#region Normales
		Vector3 up = Vector3.up;
		Vector3 down = Vector3.down;
		Vector3 front = Vector3.forward;
		Vector3 back = Vector3.back;
		Vector3 left = Vector3.left;
		Vector3 right = Vector3.right;

		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,
 
			// Left
			left, left, left, left,
 
			// Front
			front, front, front, front,
 
			// Back
			back, back, back, back,
 
			// Right
			right, right, right, right,
 
			// Top
			up, up, up, up
		};
		#endregion

		#region UVs
		Vector2 _00 = new Vector2(0f, 0f);
		Vector2 _10 = new Vector2(1f, 0f);
		Vector2 _01 = new Vector2(0f, 1f);
		Vector2 _11 = new Vector2(1f, 1f);

		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			_11, _01, _00, _10,
 
			// Left
			_11, _01, _00, _10,
 
			// Front
			_11, _01, _00, _10,
 
			// Back
			_11, _01, _00, _10,
 
			// Right
			_11, _01, _00, _10,
 
			// Top
			_11, _01, _00, _10,
		};
		#endregion

		#region Triangles
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			
 
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
 
		};
		#endregion

		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		mesh.RecalculateBounds();
		mesh.Optimize();
		var bounds = mesh.bounds;
		bounds.size *= 2f;
		mesh.bounds = bounds;

		return mesh;
	}

}
