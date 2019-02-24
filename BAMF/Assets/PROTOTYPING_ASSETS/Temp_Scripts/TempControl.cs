﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class TempControl : MonoBehaviour {


    CharacterController cControl;
    [SerializeField] float speed = 5f;
    float gravity = 9.81f;

    Animator playerAnim;

    
	void Start () {
        cControl = GetComponent<CharacterController>();
        playerAnim = GetComponentInChildren<Animator>();
	}
	
	
	void Update () {
        CalculateMovement();

       
	}
    void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalMovement = CrossPlatformInputManager.GetAxis("Vertical");
        playerAnim.SetFloat("Walk", verticalMovement);
        Vector3 direction = new Vector3(horizontalInput, 0, verticalMovement);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);
        cControl.Move(velocity * Time.deltaTime);

    }
}
