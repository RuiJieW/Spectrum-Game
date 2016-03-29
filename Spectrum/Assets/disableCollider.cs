using UnityEngine;
using System.Collections;

public class disableCollider : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        string obj;
        //colour is orange

        GameObject[] blocks;
        blocks = GameObject.FindGameObjectsWithTag("light-b");
        foreach (GameObject item in blocks)
        {
            item.GetComponent<IsoCollider>().enabled = false;
            item.GetComponent<MeshCollider>().enabled = false;
        }

    }
}