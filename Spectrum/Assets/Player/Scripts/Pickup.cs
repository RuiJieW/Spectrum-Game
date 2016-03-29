using UnityEngine;
using System.Collections;
public class Pickup : MonoBehaviour
{
    Animator anim;
    int colour = 0;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(0, 0, 0);
        var lightOrange = GameObject.FindWithTag("light-o");
        var lightBlue = GameObject.FindWithTag("light-b");

        var player = GameObject.FindGameObjectWithTag("Player");
        float XD = lightOrange.transform.position.x - player.transform.position.x;
        float YD = lightOrange.transform.position.y - player.transform.position.y;
        if (XD < 2 && XD > -2 && YD < 2 && YD > -2)
        {
            player.GetComponent<Animator>().SetTrigger("PickUp");

            lightOrange.GetComponent<Renderer>().enabled = false;
            GameObject[] blocks;

            if (GameObject.FindWithTag("light-b"))
            {
                blocks = GameObject.FindGameObjectsWithTag("blue");
                foreach (GameObject item in blocks)
                {
                    item.GetComponent<Renderer>().material.color = Color.blue;
                    item.GetComponent<IsoCollider>().enabled = false;

                }
            }


            if (GameObject.FindWithTag("light-o")) { 
                blocks = GameObject.FindGameObjectsWithTag("orange");
            foreach (GameObject item in blocks)
            {
                item.GetComponent<Renderer>().enabled = true;
            }
        }
           
        }
        ///print("XD" + XD + "YD" + YD);

    }
}