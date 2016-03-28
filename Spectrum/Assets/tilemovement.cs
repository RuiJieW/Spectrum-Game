using UnityEngine;
using System.Collections;

public class tilemovement : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.North) * Time.deltaTime);
           //  transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) *  Time.deltaTime);
	}
}
