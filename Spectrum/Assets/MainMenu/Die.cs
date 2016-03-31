using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour {

    public string level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (transform.position.y < -20)
        {


            //System.Console.WriteLine("hello there");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
    