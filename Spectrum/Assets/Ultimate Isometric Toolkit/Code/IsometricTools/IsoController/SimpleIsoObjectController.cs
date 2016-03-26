using UnityEngine;
using System.Collections;
/// <summary>
/// Simple continuous movement with WSAD/Arrow Keys movement. No collision detection, gravity, etc.
/// Note: This is an exemplary implementation. You may vary inputs, speeds, etc.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class SimpleIsoObjectController : MonoBehaviour {

    public float speed = 10;
	public float jumpForce = 5;

    new void Update() {
		//translate relative to isometric directions. IsoObject will hook up into the transform component to update its position.
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.North) * Input.GetAxis("Vertical") * Time.deltaTime * speed);
		transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        Isometric.projectGravityVector();

        if (Input.GetKeyDown("space"))
		{
			GetComponent<Rigidbody>().AddForce(Isometric.vectorToIsoDirection(IsoDirection.Up) * jumpForce, ForceMode.Impulse);
		}
     
    }




	

	
}
