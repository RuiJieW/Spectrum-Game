using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loadlvl : MonoBehaviour {
    public string level;
    GameObject player;
    GameObject door;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindWithTag("Finish");
    }
	
	// Update is called once per frame
	void Update () {

    
        //temp fix for object collision

        float XD = door.transform.position.x - player.transform.position.x;
        float YD = door.transform.position.y - player.transform.position.y;
        if (XD < 2 && XD > -2 && YD < 2 && YD > -2)
        {
         
            SceneManager.LoadScene(level);
        }

            // if player collides with object

            //

        }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
                Application.LoadLevel(level);
        }
            
    }

}
