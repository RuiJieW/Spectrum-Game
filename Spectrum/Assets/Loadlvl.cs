using UnityEngine;
using System.Collections;

public class Loadlvl : MonoBehaviour {
    public string level = "spectrum_lvl1";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // if player collides with object

        //Application.LoadLevel(level);

    }
    void OnMouseDown()
    {
        Application.LoadLevel(level);
    }

}
