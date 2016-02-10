using UnityEngine;
using System.Collections;

/// <summary>
/// Use this as an example for a controller in a isoworld with collisiondetection
/// </summary>
/// 
[RequireComponent(typeof(IsoCollider))]
public class AdvancedIsoObjectController : MonoBehaviour {

    public float speed = 5;
    public float jumpspeed = .1f;

    private Transform ghostObject;

    //Kai's addition
    Vector3 movement;
    Animator anim;
    Rigidbody PlayerRigidBody;
    bool playerInRange;
    void Start() {
                //Kai
        anim = GetComponent<Animator>();
        //playerRigidBody = GetComponent<Rigidbody>();
        ghostObject = gameObject.GetComponent<IsoCollider>().ghost.transform;

    }
    void Update() {
        if (playerInRange == true)
        {
            GameObject[] blocks;
            blocks = GameObject.FindGameObjectsWithTag("orange");
            foreach (GameObject item in blocks)
            {
                item.GetComponent<Renderer>().enabled = true;
            }
        }
        var light = GameObject.FindWithTag("light");
      /*  GameObject[] blocks;
        blocks = GameObject.FindGameObjectsWithTag("orange");
        foreach (GameObject item in blocks)
        {
            item.GetComponent<Renderer>().enabled = true;
        }*/

        if (Vector3.Distance(transform.position, light.transform.position)<5){
            anim.SetTrigger("PickUp");

           GameObject[] blocks;
            blocks = GameObject.FindGameObjectsWithTag("orange");
            foreach(GameObject item in blocks)
            {
                item.GetComponent<Renderer>().enabled = true;
            }

        }



 

            //Kai
            //print("this works");

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

           Animating(h, v);
      
        //
        ghostObject.Translate(new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1) * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) {
             anim.SetTrigger("IsJumping");
            ghostObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        }
        
    }

    //Kai's additions
   /* void FixedUpdate()
    {


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Animating(h, v);

        ghostObject.Translate(new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1) * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ghostObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        }
    }*/
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        //playerRigidBody.MovePosition(transform.position + movement);
    }

    void OnTriggerEnter(IsoCollider other)
    {
        if (other.gameObject.tag == "light" && gameObject.tag == "player") ;
        {
            playerInRange = true;
        }

    }
    void Animating (float h, float v)
    {

           // print ("gets here");
        //print("h is " + h);

        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
        bool forward = v < 0f;
        anim.SetBool("IsForward", forward);

        if (h < 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (h > 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
