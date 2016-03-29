using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {
    Animator anim;
    public int colour = 1;
    GameObject player;
    // Use this for initialization
    void Start () {
	  var player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision other)
    {
        //transform.position = new Vector3(0, 0, 0);
        //if player colides with this object

        // playerVision = colour

        var light = GameObject.FindWithTag("light");
      
        string obj;
        //colour is orange
        if (colour == 1 && other.gameObject == player)
        {
            player.GetComponent<Animator>().SetTrigger("PickUp");

            light.GetComponent<Renderer>().enabled = false;
            GameObject[] blocks;
            blocks = GameObject.FindGameObjectsWithTag("orange");
            foreach (GameObject item in blocks)
            {
                item.GetComponent<Renderer>().enabled = true;
            }

        }
       else if (colour == 2 && other.gameObject == player)
        {
            player.GetComponent<Animator>().SetTrigger("PickUp");
            //change colour to blue and disable collider
            // light.GetComponent<Renderer>().enabled = false;
            GameObject[] blocks;
            blocks = GameObject.FindGameObjectsWithTag("orange");
            foreach (GameObject item in blocks)
            {
                item.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
