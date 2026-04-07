using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpeed : MonoBehaviour
{
    Vector3 lastFd;
    public float rtSpeed;

    private void Start()
    {
        lastFd = transform.forward;
    }

    private void FixedUpdate()
    {
        rtSpeed = Vector3.Angle(transform.forward, lastFd) / Time.fixedDeltaTime;
        lastFd = transform.forward;
    }
}
