using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LookX : MonoBehaviour {

    [SerializeField] float sensitivity = 1.0f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float mouseX = Input.GetAxis("Mouse X");
        Vector3 newRot = transform.localEulerAngles;
        newRot.y += mouseX * sensitivity;
        transform.localEulerAngles = newRot;
	}
}
