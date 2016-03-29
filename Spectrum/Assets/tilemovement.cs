using UnityEngine;
using System.Collections;

public class tilemovement : MonoBehaviour {
    public int distance;
    public int direction;
	// Use this for initialization
	void Start () {
        distance = 1;
        direction = -1;
    }
	
	// Update is called once per frame
	void Update () {
        distance++;
        if (distance > 100)
        {
            distance = 1;
            direction *= -1;
        }
	        //transform.Translate(direction*Isometric.vectorToIsoDirection(IsoDirection.North) * Time.deltaTime);
        //  transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) *  Time.deltaTime);
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.Up) *  Time.deltaTime);
    }
}
