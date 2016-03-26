using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Helper class for isometric utility function
/// </summary>
public class Isometric {

	/// <summary>
	/// returns the isometric projection vector of common directions in isometric space.
	/// </summary>
	/// <param name="dir"> given direction</param>
	/// <returns></returns>
    public static Vector3 vectorToIsoDirection(IsoDirection dir) {
        switch (dir) {
            case IsoDirection.North:
                return toIsoProjection(Vector3.right);
            case IsoDirection.East:
                return toIsoProjection(Vector3.down);
            case IsoDirection.South:
                return toIsoProjection(Vector3.left);
            case IsoDirection.West:
                return toIsoProjection(Vector3.up);
            case IsoDirection.Up:
                return toIsoProjection(Vector3.forward);
            case IsoDirection.Down:
                return toIsoProjection(Vector3.back);
            default:
                return Vector2.zero;

                /*
                switch (dir)
                {
                    case IsoDirection.North:
                        return toIsoProjection(Vector3.right);
                    case IsoDirection.East:
                        return toIsoProjection(Vector3.down);
                    case IsoDirection.South:
                        return toIsoProjection(Vector3.left);
                    case IsoDirection.West:
                        return toIsoProjection(Vector3.up);
                    case IsoDirection.Up:
                        return toIsoProjection(Vector3.forward);
                    case IsoDirection.Down:
                        return toIsoProjection(Vector3.back);
                    default:
                        return Vector2.zero;
                        */
                }

    }
	/// <summary>
	/// Returns the corresponding, non-isometric Vector of an Isometric direction in regular 3d world space.
	/// Note: For the isometric projection call vectorToisoDirection(IsoDirection dir) or project the returned vector.
	/// </summary>
	/// <param name="dir"></param>
	/// <returns>A non-isometric vector relative to the direction</returns>
	public static Vector3 getVector(IsoDirection dir)
	{
		switch (dir)
		{
			case IsoDirection.North:
				return Vector3.right;
			case IsoDirection.East:
				return Vector3.down;
			case IsoDirection.South:
				return Vector3.left;
			case IsoDirection.West:
				return Vector3.up;
			case IsoDirection.Up:
				return Vector3.forward;
			case IsoDirection.Down:
				return Vector3.back;
			default:
				return Vector2.zero;
		}

	}

	/// <summary>
	/// Projects a vector to isometric viewpoint.
	/// </summary>
	/// <param name="pt"> the vector in cartesian coordinate system</param>
	/// <returns></returns>
	[Obsolete("Use toIsoProjection instead")]
    public static Vector3 isoProjection(Vector3 pt) {
		return toIsoProjection(pt);
    }


	/// <summary>
	/// Projects a vector from cartesian to isometric viewpoint.
	/// </summary>
	/// <param name="pt"> the vector in cartesian a coordinate system</param>
	/// <returns></returns>
	public static Vector3 toIsoProjection(Vector3 pt)
	{
		return IsoObject.projectionMatrix.MultiplyVector(pt);
	}

	/// <summary>
	/// Projects a vector from isometric to cartesian viewpoint.
	/// </summary>
	/// <param name="pt"> the vector in isometric a coordinate system</param>
	/// <returns></returns>
	public static Vector3 fromIsoProjection(Vector3 pt)
	{
		return IsoObject.projectionMatrix.inverse.MultiplyVector(pt);
	}

	/// <summary>
	/// Projects the standard gravity Vector(0, -9.81, 0) to isometric viewpoint and applies this to Physics.gravity
	/// </summary>
	public static void projectGravityVector()
	{
		Physics.gravity = toIsoProjection(new Vector3(0, 0, -9.81f)).normalized * 9.81f;
	}

	/// <summary>
	/// Convert a screenpoint to an isometric vector with a given height.
	/// Note: The result must be applied to IsoObject.position
	/// </summary>
	/// <param name="screenSpacePoint">Screen point in pixels</param>
	/// <param name="isoHeight">Desired isometric height </param>
	/// <returns>Isometric position with height isoHeight relative to screenpoint</returns>
	public static Vector3 screenToIsoPoint(Vector2 screenSpacePoint, float isoHeight)
	{
		//calc the pixel input to unity units
		var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenSpacePoint.x, screenSpacePoint.y, Camera.main.nearClipPlane));
		
		//sin requires radian, we use angles + avoid multiple calculations
		var sin = Mathf.Sin(Mathf.Deg2Rad * IsoObject.angle);
		//depth from camera, using the matrix inverse
		var zPos = -IsoObject.depthScaling * 1 / sin * (isoHeight * sin + isoHeight - worldPos.y);

		//result is in regular "unity space"
		var result = new Vector3(worldPos.x, worldPos.y, zPos);

		//convert to "isometric space"
		return Isometric.fromIsoProjection(result);
	}
}
