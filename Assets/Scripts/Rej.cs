using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*
 * Code source inspiré de  : 
 * https://unity3d.com/fr/learn/tutorials/topics/scripting/translate-and-rotate?playlist=17117
 * http://answers.unity3d.com/questions/9246/playing-an-animation-while-moving-a-character-forw.html
 * http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
 * http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker
 * https://unity3d.com/fr/learn/tutorials/projects/roll-ball-tutorial/displaying-score-and-text
 */
public class Rej : MonoBehaviour
{
    public float maxWalkSpeed = 60f;
    public float maxTurnSpeed = 80f;
    public float forceIncreaseFactor = 0.15f;
    public float jumpForce = 5f;
    public Text cafeCountText;

    private Animator anim;
    private Rigidbody rBody;
    private bool isGrounded = false;
    private int cafeCount;
    private float walkSpeed;
    private float turnSpeed;


    void Start()
    {
        cafeCount = 0;
        walkSpeed = 0;
        turnSpeed = 0;
        SetCafeCountText();
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            walkSpeed = Input.GetAxis("Vertical") * (maxWalkSpeed + GetForceIncrease(maxWalkSpeed));
            turnSpeed = Input.GetAxis("Horizontal") * (maxTurnSpeed + GetForceIncrease(maxTurnSpeed));
            anim.SetBool("AboveGround", false);
            if (walkSpeed < 0)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.Rotate(Vector3.up, 180);
                }
                walkSpeed = (walkSpeed * -1);
            }
        }
        else
        {
            anim.SetBool("AboveGround", true);
            AjustFallSpeed();
        }

        if (walkSpeed > 0)
        {
            anim.SetInteger("Speed", 1);       
        }
        else
        {
            anim.SetInteger("Speed", 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        AjustAnimationSpeed();
        ResetGroundAnimParameters();
    }

    private float GetForceIncrease(float force)
    {
        return force + (force * (cafeCount * forceIncreaseFactor));
    }

    private void AjustFallSpeed()
    {
        Vector3 vel = rBody.velocity;
        vel.y -= 5 * Time.deltaTime;
        rBody.velocity = vel;
    }

    private void AjustAnimationSpeed()
    {
        if (IsAnimationPlaying("Walk"))
        {
            anim.speed = 2;
        }
        else if (IsAnimationPlaying("Jump"))
        {
            anim.speed = 3;
        }
        else if (IsAnimationPlaying("Land"))
        {
            anim.speed = 3;
        }
        else
        {
            anim.speed = 1;
        }
    }

    private bool IsAnimationPlaying(string name)
    {
        var animatorState = anim.GetCurrentAnimatorStateInfo(0);
        return animatorState.IsName(name);
    }

    private void ResetGroundAnimParameters()
    {
        if (anim.GetBool("GroundEnter"))
            anim.SetBool("GroundEnter", false);

        if (anim.GetBool("GroundExit"))
            anim.SetBool("GroundExit", false);
    }

    void SetCafeCountText()
    {
        cafeCountText.text = "Nombre de cafés : " + cafeCount.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cafe")
        {
            cafeCount++;
            SetCafeCountText();
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("Ground"))
        {
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
            anim.speed = 1;
            isGrounded = false;
        }
    }

    public void DelayJumpAfterAnimation()
    {
       rBody.AddForce(Vector3.up * GetForceIncrease(jumpForce), ForceMode.Impulse);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            anim.Play("Jump");
        }
    }
}
