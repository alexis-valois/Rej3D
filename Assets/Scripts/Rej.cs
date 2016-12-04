using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code source inspiré de  : 
 * https://unity3d.com/fr/learn/tutorials/topics/scripting/translate-and-rotate?playlist=17117
 * http://answers.unity3d.com/questions/9246/playing-an-animation-while-moving-a-character-forw.html
 */
public class Rej : MonoBehaviour {

    // Public variables that will show up in the Editor
    public float maxWalkSpeed = 60f;
    public float maxTurnSpeed = 80f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var walkSpeed = Input.GetAxis("Vertical") * maxWalkSpeed;
        var turnSpeed = Input.GetAxis("Horizontal") * maxTurnSpeed;

        transform.Translate(-Vector3.forward * walkSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        if (walkSpeed > 5)
        {
            anim.Play("Walk");
        }else
        {
            anim.Play("Idle");
        }
    }
}
