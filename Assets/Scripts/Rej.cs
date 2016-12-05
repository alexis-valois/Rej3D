using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code source inspiré de  : 
 * https://unity3d.com/fr/learn/tutorials/topics/scripting/translate-and-rotate?playlist=17117
 * http://answers.unity3d.com/questions/9246/playing-an-animation-while-moving-a-character-forw.html
 * http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
 * http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker
 */
public class Rej : MonoBehaviour
{
    // Public variables that will show up in the Editor
    public float maxWalkSpeed = 60f;
    public float maxTurnSpeed = 80f;
    public float jumpForce = 5f;
    Animator anim;
    Rigidbody rBody;
    private bool isGrounded = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var walkSpeed = Input.GetAxis("Vertical") * maxWalkSpeed;
        var turnSpeed = Input.GetAxis("Horizontal") * maxTurnSpeed;

        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        ResetGroundAnimParameters();

        //if (walkSpeed > 5 && isGrounded)
        //{
        //    anim.SetInteger("Speed", 1);
        //    //anim.Play("Walk");
        //    anim.speed = 2;
        //}

        if (isGrounded)
        {
            anim.SetBool("AboveGround", false);
        }
        else
        {
            anim.SetBool("AboveGround", true);
        }

        if (walkSpeed > 5)
        {
            anim.SetInteger("Speed", 1);
        }else
        {
            anim.SetInteger("Speed", 0);
        }

        //if (walkSpeed == 0 || !isGrounded)
        //{
        //    anim.SetInteger("Speed", 0);
        //    anim.Play("Idle");
        //    anim.speed = 1;
        //}
    }

    private void ResetGroundAnimParameters()
    {
        if (anim.GetBool("GroundEnter"))
            anim.SetBool("GroundEnter", false);

        if (anim.GetBool("GroundExit"))
            anim.SetBool("GroundExit", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            //anim.Play("Land");
            anim.SetBool("GroundEnter", true);
            
            anim.speed = 1;
            isGrounded = true;
        }        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            
            anim.SetBool("GroundExit", true);
            //anim.Play("Jump");
            anim.speed = 1;
            isGrounded = false;
        }        
    }

    private void Jump()
    {
        if (isGrounded)
        {
            //anim.SetBool("AboveGround", true);
            //anim.SetBool("GroundExit", false);
            //anim.SetBool("GroundEnter", false);
            anim.Play("Jump");
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
