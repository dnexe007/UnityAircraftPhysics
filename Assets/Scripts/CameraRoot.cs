using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    public float LinearSpeedMult;
    public float LinearLerpSpeed;

    public float AngularSpeedMult;
    public float AngularLerpSpeed;

    Vector3 angleOffset;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }


    private void Update()
    {
        Vector3 angVel = rb.transform.InverseTransformDirection(rb.angularVelocity);

        angleOffset = Vector3.Lerp(angleOffset, angVel * AngularSpeedMult, AngularLerpSpeed * Time.deltaTime);

        transform.localEulerAngles = angleOffset;
    }
}
