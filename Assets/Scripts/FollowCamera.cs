using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    /*
     * Code source inspiré du fichier SmoothFollow.cs inclus dans les Standard Assets de Unity ainsi que :
     * https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
     */

    // The target we are following
    [SerializeField]
    private Transform target;
    private float currentDistance;
    private float currentHeight;
    [SerializeField]
    private float rotationDamping = 5f;
    [SerializeField]
    private float heightDamping = 5f;

    private int currentCameraModeIndex = 0;

    List<KeyValuePair<float, float>> cameraModes;

    // Use this for initialization
    void Start ()
    {
        cameraModes = new List<KeyValuePair<float, float>>(3);
        cameraModes.Add(new KeyValuePair<float, float>(5.0f, 75.0f));
        cameraModes.Add(new KeyValuePair<float, float>(10.0f, 90.0f));
        cameraModes.Add(new KeyValuePair<float, float>(15.0f, 160.0f));

        currentHeight = cameraModes[0].Key;
        currentDistance = cameraModes[0].Value;
    }

	void LateUpdate () {

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ChangeCameraMode();
        }

        if (!target)
            return;
        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + this.currentHeight;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * currentDistance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);
    }

    private void ChangeCameraMode()
    {
        currentCameraModeIndex = (currentCameraModeIndex + 1) % cameraModes.Count;
        currentHeight = cameraModes[currentCameraModeIndex].Key;
        currentDistance = cameraModes[currentCameraModeIndex].Value;
    }
}
