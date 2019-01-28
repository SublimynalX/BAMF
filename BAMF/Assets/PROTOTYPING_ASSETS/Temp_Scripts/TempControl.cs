using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour {


    CharacterController cControl;
    [SerializeField] float speed = 5f;
    float gravity = 9.81f;



    
	void Start () {
        cControl = GetComponent<CharacterController>();
	}
	
	
	void Update () {
        CalculateMovement();
       
	}
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalMovement);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);
        cControl.Move(velocity * Time.deltaTime);

    }
}
