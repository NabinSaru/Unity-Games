using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector= new Vector3(10f,10f,10f);
    [SerializeField] float period =2f;
    Vector3 startingPos;

    //todo later
    [Range(0, 1)] [SerializeField] float movementFactor;
    //0 for stationary and 1 for fully moved
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period<=Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period; //grows from 0
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(tau * cycles);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = offset + startingPos;

    }
}
