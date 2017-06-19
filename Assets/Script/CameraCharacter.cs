using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCharacter : MonoBehaviour {

    private const float angleMin = 0f;
    private const float angleMax = 50f;

    public Transform lookAT;
    public Transform camTransform;

    private Camera cam;

    private float distance = 10f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensivityX = 4f;
    private float sensivityY = 1f;

    // Use this for initialization
    void Start () {

        camTransform = transform;
        cam = Camera.main;
		
	}
	
	// Update is called once per frame
	void Update () {

        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, angleMin, angleMax);

    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAT.position + rotation * dir;
        camTransform.LookAt(lookAT.position);
    }
}
