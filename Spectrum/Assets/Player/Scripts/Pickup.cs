using UnityEngine;
using System.Collections;
public class Pickup : MonoBehaviour
{
    Animator anim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(0, 0, 0);
        var light = GameObject.FindWithTag("light");
        var player = GameObject.FindGameObjectWithTag("Player");
        float XD = light.transform.position.x - player.transform.position.x;
        float YD = light.transform.position.y - player.transform.position.y;
        if (XD < 2 && XD > -2 && YD < 2 && YD > -2)
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
        print("XD" + XD + "YD" + YD);

    }
}