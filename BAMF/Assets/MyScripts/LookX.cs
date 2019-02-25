using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LookX : MonoBehaviour {

    [SerializeField] float sensitivity = 1.0f;
    bool rotating = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
            float mouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        if (mouseX >= .1f || mouseX <= -.1f)
        {
            rotating = true;
        }
        if (rotating)
        {
            Vector3 newRot = transform.localEulerAngles;
            newRot.y += mouseX * sensitivity;
            transform.localEulerAngles = newRot;
        }

        if (mouseX <= .1f || mouseX >= -.1f)
        {
            rotating = false;
           
           
           
        }
        if (!rotating)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

    }
}
