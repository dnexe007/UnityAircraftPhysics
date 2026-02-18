using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    Rigidbody rb;
    public float drag;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public float AngularVelocity = 10;
    void FixedUpdate()
    {
        rb.drag = drag;
        rb.AddTorque((transform.forward * Input.GetAxis("Horizontal")) * AngularVelocity, ForceMode.Acceleration);
    }
}
