using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    /*
     * Code source inspiré de :
     * https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
     */

    public GameObject target;
    public float damping = 1;
    Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = target.transform.position - transform.position;
	}

	void LateUpdate () {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        //transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
