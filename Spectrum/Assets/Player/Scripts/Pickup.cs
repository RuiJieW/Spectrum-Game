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
    {   //STILL NEED TO RESET FOR SWITCHING BETWEEEN COLOURS
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



            //specific pickup for orange light orb
            if (GameObject.FindWithTag("light-o")) { 
                blocks = GameObject.FindGameObjectsWithTag("orange");
             foreach (GameObject item in blocks)
                {
                    item.GetComponent<Renderer>().enabled = true;
                }
             }
           
        }


                float BXD = lightBlue.transform.position.x - player.transform.position.x;
        float BYD = lightBlue.transform.position.y - player.transform.position.y;
        if (BXD < 2 && BXD > -2 && BYD < 2 && BYD > -2)
        {
                        player.GetComponent<Animator>().SetTrigger("PickUp");

            lightBlue.GetComponent<Renderer>().enabled = false;
            GameObject[] blocks;
                    //specific pickup for blue light orb
            if (GameObject.FindWithTag("light-b"))
            {
                blocks = GameObject.FindGameObjectsWithTag("blue");
                foreach (GameObject item in blocks)
                {   
                    //turn blocks blue and turn off collision
					item.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.25f);
                    item.GetComponent<IsoCollider>().enabled = false;

                }
            }
}
        ///print("XD" + XD + "YD" + YD);

    }
}