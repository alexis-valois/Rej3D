using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code source inspiré de  : https://www.youtube.com/watch?v=KqDRLZWcYi4
 */
public class Rej : MonoBehaviour {

    // Public variables that will show up in the Editor
    public float Acceleration = 50f;
    public float MaxSpeed = 20f;
    private Rigidbody rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the player's input axes
        float xSpeed = Input.GetAxis("Horizontal");
        float zSpeed = Input.GetAxis("Vertical");
        // Get the movement vector
        Vector3 velocityAxis = new Vector3(xSpeed, 0, zSpeed);
        // Rotate the movement vector based on the camera
        velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;

        // Move the player
        rbody.AddForce(velocityAxis.normalized * Acceleration);

        LimitVelocity();
    }

    /// <summary>
    /// Keeps the player's velocity limited so it will not go too fast.
    /// </summary>
    private void LimitVelocity()
    {
        Vector2 xzVel = new Vector2(rbody.velocity.x, rbody.velocity.z);
        if (xzVel.magnitude > MaxSpeed)
        {
            xzVel = xzVel.normalized * MaxSpeed;
            rbody.velocity = new Vector3(xzVel.x, rbody.velocity.y, xzVel.y);
        }
    }
}
