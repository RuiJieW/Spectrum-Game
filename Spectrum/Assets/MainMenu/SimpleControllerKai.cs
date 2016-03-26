﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Simple continuous movement with WSAD/Arrow Keys movement. No collision detection, gravity, etc.
/// Note: This is an exemplary implementation. You may vary inputs, speeds, etc.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class SimpleControllerKai : MonoBehaviour
{

    public float speed = 10;
    public float jumpForce = 5;

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

    }


    new void Update()
    {
        //translate relative to isometric directions. IsoObject will hook up into the transform component to update its position.
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.North) * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Isometric.vectorToIsoDirection(IsoDirection.East) * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        Isometric.projectGravityVector();


        //Kai's stuff for animations
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Animating(h, v);
        //


        //Kai's addition
        Vector3 movement;
        Animator anim;
        Rigidbody PlayerRigidBody;
        bool playerInRange;
        //





        if (Input.GetKeyDown("space"))
        {
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