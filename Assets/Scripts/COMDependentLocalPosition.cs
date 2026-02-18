using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMDependentLocalPosition : MonoBehaviour
{
    [Serializable]
    public struct Axes
    {
        public bool x, y, z;
    }

    public Axes enabledAxes;
    public Vector3 offset;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.centerOfMass + offset;

        Vector3 position = transform.localPosition;

        if(enabledAxes.x) position.x = newPosition.x;
        if(enabledAxes.y) position.y = newPosition.y;
        if(enabledAxes.z) position.z = newPosition.z;

        transform.localPosition = position;
    }
}
