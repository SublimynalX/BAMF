using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour {

    [SerializeField] float moveSpeed = 10, rotSpeed = 1;
    float hMove;
    float vertMove;
	void Start () {
		
	}
	
	
	void LateUpdate () {
        hMove = Input.GetAxis("Horizontal");
        if(Input.GetButton("Horizontal") && hMove > 0)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetButton("Horizontal") && hMove < 0)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }


        vertMove = Input.GetAxis("Vertical");
        if(Input.GetButton("Vertical") && vertMove > 0)
        {
            transform.Translate( 0, 0, moveSpeed * Time.deltaTime);
        }
        else if(Input.GetButton("Vertical") && vertMove < 0)
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R)){
            transform.Rotate(0, rotSpeed, 0);
        }
       
	}
}
