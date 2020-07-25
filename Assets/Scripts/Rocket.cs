﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float rcsRotate = 5f;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void ProcessInput()
    {
        
        
    }
    private void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*rcsThrust*Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation= true; //take manual control of rotation
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rcsRotate*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rcsRotate*Time.deltaTime);
        }
        rigidBody.freezeRotation = false; //take physical control of rotation
    }
    
}
