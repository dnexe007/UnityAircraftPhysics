using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchHelper : MonoBehaviour
{
    FlightData fd;
    Rigidbody rb;

    public AnimationCurve HelperOverAngularVel = new(
        new(0, 0),
        new(0.07f, 0.15f)
    );

    public AnimationCurve HelperMultOverAngle = new(
        new(0, 0),
        new(30, 1)
    );

    private float angularVel;
    private void Start()
    {
        fd = GetComponentInParent<FlightData>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        angularVel = Mathf.Abs(rb.transform.InverseTransformDirection(rb.angularVelocity).y);
        rb.AddTorque(
            -transform.right *
            HelperMultOverAngle.Evaluate(Mathf.Abs(fd.Roll)) *
            HelperOverAngularVel.Evaluate(angularVel),
            ForceMode.Acceleration
        );
            
    }
}
