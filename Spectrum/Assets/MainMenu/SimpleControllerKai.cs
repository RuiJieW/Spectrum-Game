using UnityEngine;
using System.Collections;
/// <summary>
/// Simple continuous movement with WSAD/Arrow Keys movement. No collision detection, gravity, etc.
/// Note: This is an exemplary implementation. You may vary inputs, speeds, etc.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class SimpleControllerKai : MonoBehaviour
{
    public float timeBetweenJump;
<<<<<<< HEAD
    public float speed = 5;
    public float speed = 1;
=======


    public float speed = 1;

>>>>>>> f10a0e3f28703eeae7c846b51359ffe988695542
    public float jumpForce = 5;
    float timer;
    //Kai's addition
    Vector3 movement;
    Animator anim;
    Rigidbody PlayerRigidBody;
    bool playerInRange;
    //

    void Start()
    {
        //Kai addition
        anim = GetComponent<Animator>();
        //
        timer = 2f;
    }


    new void Update()
    {
        //translate relative to isometric directions. IsoObject will hook up into the transform component to update its position.
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.North) * Input.GetAxis("Vertical") * /*Time.deltaTime* * */speed);
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) * Input.GetAxis("Horizontal") * /*Time.deltaTime **/ speed);

        Isometric.projectGravityVector();


        GameObject[] blocks = GameObject.FindGameObjectsWithTag("orange");

        foreach(GameObject block in blocks) {

            transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.North) * Input.GetAxis("Vertical") * Time.deltaTime * speed);
            transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        }

        //Kai's stuff for animations
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Animating(h, v);
        //


        timer += Time.deltaTime;


        if (Input.GetKeyDown("space") && timer >2)
        {
            timer = 0f;
            anim.SetTrigger("IsJumping");
            GetComponent<Rigidbody>().AddForce(Isometric.vectorToIsoDirection(IsoDirection.Up) * jumpForce, ForceMode.Impulse);
        }

    }

    //Kai stuff

    //animation of player
    void Animating(float h, float v)
    {

        // print ("gets here");
        //print("h is " + h);

        bool walking = h != 0f || v != 0f;
        if (walking)
        {
            anim.SetTrigger("IsWalking");
        }

        if (v < 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("IsForward", true);
        }
        else if (v > 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("IsForward", false);
        }

        //left
        if (h < 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("IsForward", false);

        }
        //right
        if (h > 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("IsForward", true);


        }
    }

}