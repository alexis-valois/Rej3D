using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code source inspiré de  : https://unity3d.com/fr/learn/tutorials/topics/scripting/translate-and-rotate?playlist=17117
 */
public class Rej : MonoBehaviour {

    // Public variables that will show up in the Editor
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

}
